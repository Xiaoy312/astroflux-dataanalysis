<Query Kind="Program">
  <NuGetReference>Pcap.Net.x86</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>PcapDotNet.Base</Namespace>
  <Namespace>PcapDotNet.Core</Namespace>
  <Namespace>PcapDotNet.Core.Extensions</Namespace>
  <Namespace>PcapDotNet.Packets</Namespace>
  <Namespace>PcapDotNet.Packets.IpV4</Namespace>
  <Namespace>PcapDotNet.Packets.Transport</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
    var playerioGS24 = new IpV4Address("69.175.58.50");

    var packets = SniffDevice("{AA527AE1-4315-437C-9FBB-4290D7A14107}", "tcp")
        .Where(p => p.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
        .Where(p => p.Ethernet.IpV4.Destination == playerioGS24 || p.Ethernet.IpV4.Source == playerioGS24)
        .Publish().RefCount();
    var inboundPackets = packets.Where(p => p.Ethernet.IpV4.Source == playerioGS24);
    var outboundPackets = packets.Where(p => p.Ethernet.IpV4.Destination == playerioGS24);

    var serializer = new BinarySerializer(inboundPackets);
    serializer.ObserveMessages()
        //.IgnorePlayerMessages()
        //.Where(x => x.Type == "tryBeamPickup")
        .Select(x => new { x.Type, x.Contents.Count })
        .Dump();
}

// Define other methods and classes here
IObservable<Packet> SniffDevice(string guid, string filterValue)
{
    var device = LivePacketDevice.AllLocalMachine.FirstOrDefault(x => x.GetGuid() == guid);
    if (device == null)
        throw new ArgumentException("No device found with guid: " + guid);

    return Observable.Create<Packet>(observer =>
    {
        Task.Run(() =>
        {
            using (var communicator = device.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                    throw new NotSupportedException();

                using (var filter = communicator.CreateFilter(filterValue))
                    communicator.SetFilter(filter);

                communicator.ReceivePackets(0, observer.OnNext);
            }
        });

        return Disposable.Empty;
    });
}
byte[] GetData()
{
    return @"
    
96 ca 73 70 61 77 6e 45 6e 65 6d 79 d6 43 31 56 70 47 5a 65 46 70 6b 65 79 50 35 53 33 47 48 31 43 54 51 06 09 2b fb 80 d6 44 4b 62 6c 33 6a 37 49 5f 55 53 70 68 77 41 71 37 78 67 67 61 67 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 00 00 03 42 cc f2 4c ec 9b 0d 00 07 ff df fb 00 07 ff fe 98 3b 80 80 80 00 00 00 00

    "
        .Trim()
        .Split(' ')
        .Select(c => byte.Parse(c, System.Globalization.NumberStyles.HexNumber))
        .ToArray();
}

public class BinarySerializer
{
    private const byte ShortStringMask = 0b11000000;
    private const byte ShortUnsignedIntMask = 0b10000000;
    private const byte ShortByteArrayMask = 0b01000000;
    private const byte UnsignedLongMask = 0b00111000;
    private const byte LongMask = 0b00110000;
    private const byte ByteArrayMask = 0b00010000;
    private const byte StringMask = 0b00001100;
    private const byte UnsignedIntMask = 0b00001000;
    private const byte IntMask = 0b00000100;
    private const byte DoubleMask = 0b00000011;
    private const byte FloatMask = 0b00000010;
    private const byte BooleanTrueMask = 0b00000001;
    private const byte BooleanFalseMask = 0b00000000;

    private Subject<object> _values = new Subject<object>();
    private Subject<Message> _messages = new Subject<Message>();
    
    private List<Token> _tokens = new List<Token>();
    private MessageBuilder _messageBuilder = null;
    private TokenBuilder _tokenBuilder = null;

    public BinarySerializer(IObservable<Packet> feed)
    {
        _tokens.Add(new Token(ShortStringMask, ReadUnsignInt, (Stream stream) => Encoding.UTF8.GetString(stream.ReadToEnd())));
        _tokens.Add(new Token(ShortUnsignedIntMask, ReadUnsignInt));
        _tokens.Add(new Token(ShortByteArrayMask, stream => throw new NotImplementedException("ShortByteArray")));
        _tokens.Add(new Token(UnsignedLongMask, stream => throw new NotImplementedException("UnsignedLong")));
        _tokens.Add(new Token(LongMask, stream => throw new NotImplementedException("Long")));
        _tokens.Add(new Token(ByteArrayMask, stream => throw new NotImplementedException("ByteArray")));
        _tokens.Add(new Token(StringMask, stream => throw new NotImplementedException("String")));
        _tokens.Add(new Token(UnsignedIntMask, stream => throw new NotImplementedException("UnsignedInt")));
        _tokens.Add(new Token(IntMask, ReadLength, (Stream steam) => unchecked((int)(uint)ReadUnsignInt(steam))));
        _tokens.Add(new Token(DoubleMask, _ => 8, stream => BitConverter.ToDouble(stream.ReadToEnd(), 0)));
        _tokens.Add(new Token(FloatMask, _ => 4, stream => BitConverter.ToSingle(stream.ReadToEnd(), 0)));
        _tokens.Add(new Token(BooleanTrueMask, _ => true));
        _tokens.Add(new Token(BooleanFalseMask, _ => false));

        feed
            .Select(p => p.Ethernet.IpV4.Tcp.Payload)
            .Subscribe(d => AddBytes(d));
        _messages.HandleMessagePack(_messages);
    }

    private void AddByte(byte value)
    {
        if (_tokenBuilder == null)
        {
            var token = _tokens.FirstOrDefault(x => (x.Type & value) == x.Type);
            if (token != null)
            {
                _tokenBuilder = new TokenBuilder(token);
                _tokenBuilder.OnValue.Subscribe(OnValue);
                _tokenBuilder.AddByte((byte)(value & ~token.Type));
            }
        }
        else
        {
            _tokenBuilder.AddByte(value);
        }
    }
    public void AddBytes(IEnumerable<byte> bytes)
    {
        foreach (var item in bytes)
        {
            try
            {
                AddByte(item);
            }
            catch (NotImplementedException)
            {
                throw;
            }
            catch (Exception)
            {
                // swallow exception
            }
        }
    }
    
    public IObservable<object> ObserveValues() => _values.AsObservable();
    public IObservable<Message> ObserveMessages() => _messages.AsObservable();
    
    private void OnValue(object value)
    {
        _values.OnNext(value);
        
        if (_messageBuilder == null)
        {
            _messageBuilder = new MessageBuilder();
            _messageBuilder.OnMessage.Subscribe(OnMessage);
        }

        _tokenBuilder.Dispose();
        _tokenBuilder = null;
        _messageBuilder.AddValue(value);
    }
    private void OnMessage(Message message)
    {
        _messages.OnNext(message);
        
        _messageBuilder.Dispose();
        _messageBuilder = null;
    }

    private object ReadLength(Stream stream) => (uint)ReadUnsignInt(stream) + 1;
    private object ReadUnsignInt(Stream stream) => Enumerable.Range(0, (int)stream.Length).Aggregate(0U, (acc, x) => acc << 8 | (uint)stream.ReadByte());
}
public class MessageBuilder : IDisposable
{
    public IObservable<Message> OnMessage => _messages.AsObservable();

    private Subject<Message> _messages = new Subject<Message>();
    private Message _message = new Message();
    private int _length = -1;
    
    public void Dispose()
    {
        _messages.Dispose();
    }

    public void AddValue(object value)
    {
        if (_length == -1)
        {
            switch (value)
            {
                case byte x: _length = (int)x; break;
                case int x: _length = (int)x; break;
                case uint x: _length = (int)x; break;

                default:
                    throw new ArgumentException($"First message value should be a valid integer value indicating its length. Type `{value.GetType()}` is not a valid length.");
            }
        }
        else
        {
            if (_message.Type == null)
            {
                if (!(value is string type))
                    throw new ArgumentException("Second message value should be a string indicating its type");
                
                _message.Type = type;
            }
            else
            {
                _message.Contents.Add(value);
            }
            
            if (_message.Contents.Count == _length)
                _messages.OnNext(_message);
        }
    }
}
public class TokenBuilder : IDisposable
{
    public IObservable<object> OnValue => _values.AsObservable();

    private Subject<object> _values = new Subject<object>();
    private Token _token;
    private int offset = 0;
    private uint length = 1;
    private Stream stream = new MemoryStream();

    public TokenBuilder(Token token)
    {
        this._token = token;
    }
    public void Dispose()
    {
        _values.Dispose();
        stream.Dispose();
    }

    public void AddByte(byte value)
    {
        stream.WriteByte(value);

        if (stream.Length == length)
        {
            stream.Position = 0;

            if (offset == _token.Length - 1)
            {
                _values.OnNext(_token.Handlers[offset](stream));
            }
            else
            {
                var result = _token.Handlers[offset](stream);
                switch (result)
                {
                    case byte x: length = (uint)x; break;
                    case int x: length = (uint)x; break;
                    case uint x: length = (uint)x; break;

                    default:
                        throw new InvalidOperationException($"Intermediate handler should return an integer value as length. Type `{result.GetType()}` is not a valid length.");
                }
                
                stream = new MemoryStream();
                offset++;

                if (length == 0)
                {
                    // skip to last
                    _values.OnNext(_token.Handlers.Last()(stream));
                }
            }
        }
    }
}
public class Token
{
    public IReadOnlyList<Func<Stream, object>> Handlers { get; }
    public byte Type { get; }
    public int Length => Handlers.Count;

    public Token(byte type, params Func<Stream, object>[] handlers)
    {
        this.Type = type;
        this.Handlers = handlers.AsReadOnly();
    }
}

public class Message
{
    public string Type { get; set; }
    public List<object> Contents { get; set; } = new List<object>();

    public Message() { }
    protected Message(Message message)
    {
        this.Type = message.Type;
        this.Contents = message.Contents;
    }
    
    public T Get<T>(int index) => (T)Contents[index];
    public int GetLength(int index)
    {
        var value = Contents[index];
        switch (value)
        {
            case byte x: return (int)x;
            case int x: return (int)x;
            case uint x: return (int)x;
            
            default:
                throw new ArgumentException($"Expecting value to be a valid interger. Type `{value.GetType()}` is not a valid length.");
        }
    }

    public static Message FromMessagePack(Message pack, string type, int index, int length)
    {
        var message = new Message
        {
            Type = type,
            Contents = pack.Contents.Skip(index).Take(length).ToList()
        };

        if (message.Contents.Count != length)
            throw new DataException("message length mismatch");

        return message;
    }
    
    public override string ToString()
    {
        return $"Message[{Type}]: length={Contents.Count}";
    }
}
#region Specialized Message
public class SpawnDropMessage : Message
{
    public List<object> Drops { get; set; }

    public SpawnDropMessage(Message message) : base(message)
    {
        for (int i = 0; i < message.Contents.Count; i += 9)
        {
            var key = Get<string>(i);
            var id = Get<string>(i);
            var isEjectedFromCargo = Get<string>(i);

            Drops.Add(key);
        }
    }
}
#endregion
public static class MessageHelper
{
    public static IObservable<Message> IgnorePlayerMessages(this IObservable<Message> source)
    {
        var ignoreTypeList = @"
            playerUpdate, playerLanded, playerKilled, playerEnterWarpJump, userLeft
            FriendManager: friendRequest, addFriend, removeFriend
            GroupManager: addToGroup, removeFromGroup, addGroupInvite, cancelGroupInvite
        ".Trim().Split('\n', ',', ':').Select(x => x.Trim()).ToList();
        
        return source
            .Where(x => !ignoreTypeList.Contains(x.Type));
    }
    public static void HandleMessagePack(this IObservable<Message> source, IObserver<Message> messageObserver)
    {
        source
            .Where(x => x.Type == "msgPack")
            .Subscribe(pack =>
            {
                var type = default(string);
                var length = default(int);

                try
                {
                    for (int i = 0; i < pack.Contents.Count - 2; i += length)
                    {
                        type = pack.Get<string>(i);
                        length = pack.GetLength(i + 1);

                        switch (type)
                        {
                            case "missionUpdate": messageObserver.OnNext(Message.FromMessagePack(pack, "updateMission", i + 2, length - 2)); break;
                            case "statsChanged": messageObserver.OnNext(Message.FromMessagePack(pack, "updatePlayerStats", i + 2, length - 2)); break;
                            case "bossComponentDamaged": messageObserver.OnNext(Message.FromMessagePack(pack, "bossComponentDamaged", i + 2, length - 2)); break;
                            case "bossComponentKilled": messageObserver.OnNext(Message.FromMessagePack(pack, "bossComponentKilled", i + 2, length - 2)); break;
                            case "aiBoucning": messageObserver.OnNext(Message.FromMessagePack(pack, "projectileBouncing", i + 2, length - 2)); break;
                            case "t": messageObserver.OnNext(Message.FromMessagePack(pack, "spawnerKilled", i + 2, length - 2)); break; // yes, they are inverted...
                            case "s": messageObserver.OnNext(Message.FromMessagePack(pack, "turretKilled", i + 2, length - 2)); break;
                            case "r": messageObserver.OnNext(Message.FromMessagePack(pack, "enemyKilled", i + 2, length - 2)); break;
                            case "h": messageObserver.OnNext(Message.FromMessagePack(pack, "xpGain", i + 2, length - 2)); break;
                            case "i": messageObserver.OnNext(Message.FromMessagePack(pack, "xpLoss", i + 2, length - 2)); break;
                            case "j": messageObserver.OnNext(Message.FromMessagePack(pack, "playerDamaged", i + 2, length - 2)); break;
                            case "k": messageObserver.OnNext(Message.FromMessagePack(pack, "enemyDamaged", i + 2, length - 2)); break;
                            case "m": messageObserver.OnNext(Message.FromMessagePack(pack, "spawnerDamaged", i + 2, length - 2)); break;
                            case "l": messageObserver.OnNext(Message.FromMessagePack(pack, "turretDamaged", i + 2, length - 2)); break;
                            case "b": messageObserver.OnNext(Message.FromMessagePack(pack, "playerFire", i + 2, length - 2)); break;
                            case "a": messageObserver.OnNext(Message.FromMessagePack(pack, "playerCourse", i + 2, length - 2)); break;
                            case "p": messageObserver.OnNext(Message.FromMessagePack(pack, "enemyFire", i + 2, length - 2)); break;
                            case "q": messageObserver.OnNext(Message.FromMessagePack(pack, "turretFire", i + 2, length - 2)); break;
                            case "c": messageObserver.OnNext(Message.FromMessagePack(pack, "aiStateChanged", i + 2, length - 2)); break;
                            case "n": messageObserver.OnNext(Message.FromMessagePack(pack, "tryBeamPickup", i + 2, length - 2)); break;
                            case "o": messageObserver.OnNext(Message.FromMessagePack(pack, "tryPickup", i + 2, length - 2)); break;
                            case "playerWeaponChanged": messageObserver.OnNext(Message.FromMessagePack(pack, "playerWeaponChanged", i + 2, length - 2)); break;
                            case "AICharge": messageObserver.OnNext(Message.FromMessagePack(pack, "aiCharge", i + 2, length - 2)); break;
                            case "g": messageObserver.OnNext(Message.FromMessagePack(pack, "hardenShield", i + 2, length - 2)); break;
                            case "d": messageObserver.OnNext(Message.FromMessagePack(pack, "playerUsedBoost", i + 2, length - 2)); break;
                            case "e": messageObserver.OnNext(Message.FromMessagePack(pack, "convShield", i + 2, length - 2)); break;
                            case "f": messageObserver.OnNext(Message.FromMessagePack(pack, "dmgBoost", i + 2, length - 2)); break;
                            case "u": messageObserver.OnNext(Message.FromMessagePack(pack, "spawnDrops", i + 2, length - 2)); break;
                            case "v": messageObserver.OnNext(Message.FromMessagePack(pack, "powerUpHeal", i + 2, length - 2)); break;
                            case "x": messageObserver.OnNext(Message.FromMessagePack(pack, "troonGain", i + 2, length - 2)); break;
                            case "z": messageObserver.OnNext(Message.FromMessagePack(pack, "aiTeleport", i + 2, length - 2)); break;

                            default:
                                Console.WriteLine($"Invalid message type: {type}");
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to parse `msgPack`: {e}");
                    Console.WriteLine($"Type: {type}");
                }
            });
    }
}

public static class StreamExtensions
{
    public static byte[] ReadToEnd(this Stream stream)
    {
        var length = stream.Length - stream.Position;
        var buffer = new byte[length];
        stream.Read(buffer, 0, buffer.Length);
        
        return buffer;
    }
}
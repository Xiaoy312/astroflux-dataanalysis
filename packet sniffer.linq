<Query Kind="Program">
  <NuGetReference>Pcap.Net.x86</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>PcapDotNet.Base</Namespace>
  <Namespace>PcapDotNet.Core</Namespace>
  <Namespace>PcapDotNet.Core.Extensions</Namespace>
  <Namespace>PcapDotNet.Packets</Namespace>
  <Namespace>PcapDotNet.Packets.IpV4</Namespace>
  <Namespace>PcapDotNet.Packets.Transport</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
</Query>

void Main()
{
    var playerioGS24 = new IpV4Address("69.175.58.50");

    SniffDevice("{AA527AE1-4315-437C-9FBB-4290D7A14107}", "tcp")
        .Where(p => p.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp)
        .Where(p => p.Ethernet.IpV4.Destination == playerioGS24 || p.Ethernet.IpV4.Source == playerioGS24)
        .Select(p => p.Ethernet.IpV4.Tcp.Payload.ToArray())
        //        .Select(d => new
        //        {
        //            Text = Encoding.ASCII.GetString(d, 0, Math.Min(d.Length, 20)),
        //            Raw = d.BytesSequenceToHexadecimalString(" "),
        //        })
        //        .Where(x => !x.Text.Contains("msgPack"))
        //.Select(d => Tuple.Create(d, MessageParser.Parse(d)))
        .Select(d => MessageParser.Parse(d))
        //.GroupBy(x => x.Type, x => x.Contents.Count)
        .Select(x => x.Type).Distinct()
        //.Where(x => x.Item2.Type == "spawnEnemy")
//        .Select(x => new
//        {
//            x.Item2.Type, x.Item2.Contents, Raw = x.Item1.BytesSequenceToHexadecimalString(" ")
//        })
        //.Take(2)
        .Dump(1);
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

public class Message
{
    public string Type { get; set; }
    public List<object> Contents { get; set; }

    public override string ToString()
    {
        return $"Message[{Type}]: length={Contents.Count}";
    }
}
public class MessageParser
{
    private static readonly new Dictionary<byte, Func<Stream, byte, object>> tokenParsers = new Dictionary<byte, Func<Stream, byte, object>>
    {
        [192] = ReadShortString,
        [128] = ReadShortUnsignedInt,
        [64] = ReadShortByteArray,
        [56] = ReadUnsignedLong,
        [48] = ReadLong,
        [16] = ReadByteArray,
        [12] = ReadString,
        [8] = ReadUnsignedInt,
        [4] = ReadInt,
        [3] = ReadDouble,
        [2] = ReadFloat,
        [1] = ReadBooleanTrue,
        [0] = ReadBooleanFalse,
    };

    public static Message Parse(byte[] data)
    {
        var contents = new List<object>();
        using (var stream = new MemoryStream(data))
        {
            while (stream.Position < stream.Length)
            {
                var position = stream.Position;
                try
                {
                    //Console.Write($"position={stream.Position,3}");
                    var token = (byte)stream.ReadByte();
                    //Console.Write($", token={token:X2} ({Convert.ToString(token, 2).PadLeft(8, '0')})");

                    var parser = tokenParsers.FirstOrDefault(x => (x.Key & token) == x.Key);
                    //Console.Write(", parser=" + Convert.ToString(parser.Key, 2).PadLeft(8, '0'));

                    var result = parser.Value.Invoke(stream, (byte)(token & ~parser.Key));
                    //Console.WriteLine($", result=({result.GetType().Name}){result}");

                    contents.Add(result);


                    var buffer = new byte[stream.Position - position];
                    stream.Position = position;
                    stream.Read(buffer, 0, buffer.Length);

                    //string.Join(" ", buffer.Select(x => x.ToString("X2"))).Dump();
                    //Console.WriteLine();
                }
                catch (NotImplementedException e)
                {
                    stream.Position = position + 1;
                    var buffer = new byte[stream.Length - stream.Position];
                    stream.Read(buffer, 0, buffer.Length);

                    data.BytesSequenceToHexadecimalString(" ").Dump("data");
                    buffer.BytesSequenceToHexadecimalString(" ").Dump("rest of stream");

                    throw e;
                }
                catch (Exception e)
                {
                    stream.Position = position + 1;
                    var buffer = new byte[stream.Length - stream.Position];
                    stream.Read(buffer, 0, buffer.Length);

                    return new Message()
                    {
                        Type = "ParsingError",
                        Contents = new List<object>
                            {
                                data,
                                buffer,
                                e,
                            }
                    };
                }
            }
        }
        
        try
        {

            var message = new Message();
            message.Type = (string)contents[1];
            message.Contents = contents.Skip(2).ToList();

            if (message.Contents.Count != Convert.ToInt32(contents[0]))
            {
                //throw new InvalidDataException();
            }

            return message;
        }
        catch (Exception e)
        {
            return new Message()
            {
                Type = "ParsingError",
                Contents = new List<object>
                {
                    data,
                    contents,
                    e,
                }
            };
        }
    }

    static object ReadShortString(Stream stream, byte token) => Encoding.UTF8.GetString(ReadBytes(stream, token));
    static object ReadShortUnsignedInt(Stream stream, byte token) => token;
    static object ReadShortByteArray(Stream stream, byte token) => ReadBytes(stream, token);
    static object ReadUnsignedLong(Stream stream, byte token) => ReadBytes(stream, token).Length & 0; // Longs are unsupported in ActionScript, returning (int)0.
    static object ReadLong(Stream stream, byte token) => ReadBytes(stream, token).Length & 0;
    static object ReadByteArray(Stream stream, byte token)
    {
        var length = Enumerable.Range(0, token + 1).Aggregate(0, (result, _) => result << 8 | stream.ReadByte());
        var value = ReadBytes(stream, length);
    
        return value;
    }
    static object ReadString(Stream stream, byte token)
    {
        var length = Enumerable.Range(0, token + 1).Aggregate(0, (result, _) => result << 8 | stream.ReadByte());
        var value = Encoding.UTF8.GetString(ReadBytes(stream, length));

        return value;
    }
    static object ReadUnsignedInt(Stream stream, byte token) => Enumerable.Range(0, token + 1).Aggregate(0, (result, _) => result << 8 | stream.ReadByte());
    static object ReadInt(Stream stream, byte token) => Enumerable.Range(0, token + 1).Aggregate(0, (result, _) => result << 8 | stream.ReadByte());
    static object ReadDouble(Stream stream, byte token) => BitConverter.ToDouble(ReadBytes(stream, 8), 0);
    static object ReadFloat(Stream stream, byte token) => BitConverter.ToSingle(ReadBytes(stream, 4), 0);
    static object ReadBooleanTrue(Stream stream, byte token) => true;
    static object ReadBooleanFalse(Stream stream, byte token) => false;

    static byte[] ReadBytes(Stream stream, int length)
    {
        var buffer = new byte[length];
        stream.Read(buffer, 0, length);

        return buffer;
    }
}
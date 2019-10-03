<Query Kind="Program" />

void Main()
{
    var data = GetData();
    var contents = ParseData(data).Dump(0);
    var message = ParseMessage(contents).Dump();


}

// Define other methods and classes here
byte[] GetData()
{
    return @"
    
96 c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 79 00 07 ff e0 29 1e 06 05 ae d8 80 80 05 0d 6e 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 31 c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 80 80 07 ff ec 14 b0 06 02 aa ab 80 80 05 0d 08 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 30 c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 80 80 07 ff f0 94 0b 06 02 af f8 80 80 05 12 2f 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 2f c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 80 80 07 ff f0 cc a1 06 02 7b f6 80 80 05 11 79 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 2e c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 80 80 07 ff ef 0e a0 07 ff f8 5f 6e 80 80 05 10 f5 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 2d c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f 80 80 07 ff ea df 78 06 01 7e 26 80 80 05 10 21 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 33 c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f a0 00 07 ff e8 d8 74 07 ff ff 15 67 80 80 05 0d 4c 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 34 c6 41 49 49 64 6c 65 80 03 42 cc f2 4b 36 0f bf 00 07 ff ee 2b 68 06 05 65 d2 80 80 05 0e 08 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 01 ce 41 49 54 65 6c 65 70 6f 72 74 45 78 69 74 80 03 42 cc f2 4b 36 0f bf 00 07 ff e5 ee c1 06 0a ea 6d 07 ff ff e2 71 07 ff ff f0 81 05 0e 28 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 02 ce 41 49 54 65 6c 65 70 6f 72 74 45 78 69 74 80 03 42 cc f2 4b 36 0f d6 80 07 ff e1 5c 08 06 0a 09 c8 07 ff ff e4 18 07 ff ff ed 02 05 0e 9b 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7c 03 ce 41 49 54 65 6c 65 70 6f 72 74 45 78 69 74 80 03 42 cc f2 4b 36 0f f5 80 07 ff e8 95 b4 07 ff ff 25 34 07 ff ff f3 19 07 ff ff f3 be 05 0f 3d 00 00 00 00 03 00 00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 00 c1 63 91 05 7b a5 ca 41 49 54 65 6c 65 70 6f 72 74 05 03 20 03 42 cc f2 4b 36 0f f6 00 07 ff e8 88 4e 06 03 8a 85 07 ff ff f9 22 05 a5 de 05 04 bc 01 00 00 00 03 c0 cc 60 5b cf 47 54 ba 03 40 a0 7a 06 32 17 36 72 c1 61 8d d3 73 69 6d 70 6c 65 31 34 38 36 34 37 32 38 36 33 36 39 39 03 42 cc f2 4b 36 0f b3 00 07 ff e9 55 dc 06 02 7d 75 05 14 8b 07 ff ff e0 d8 05 0d b5 01 01 01 00 c1 61 8d d3 73 69 6d 70 6c 65 31 34 38 36 34 37 32 38 36 33 36 39 39 03 42 cc f2 4b 36 0f c3 80 07 ff e9 56 4f 06 02 7c 62 05 0d 99 07 ff ff df 7a 05 0d 34 01 00 01 00 c1 63 91 05 5e 44 ca 41 49 54 65 6c 65 70 6f 72 74 05 05 dc 03 42 cc f2 4b 36 10 03 00 07 ff d0 f0 b5 06 01 b9 c6 05 5a 05 05 4b ad 05 02 b2 01 00 00 00 03 c0 dd 7e 8f 2e 6d 73 16 03 40 99 c2 6e 34 ea 8e 7d c1 61 8d d3 73 69 6d 70 6c 65 31 34 38 36 34 37 32 38 36 33 36 39 39 03 42 cc f2 4b 36 10 26 80 07 ff e9 54 48 06 02 76 a2 07 ff ff e5 fb 07 ff ff e9 b5 05 0a 2f 01 00 00 00 c1 63 91 05 7c 27 ca 41 49 54 65 6c 65 70 6f 72 74 05 03 e8 03 42 cc f2 4b 36 10 5b 00 07 ff e7 e7 f8 06 03 1b bd 80 80 05 16 d3 01 00 00 00 03 c0 cd 5c e2 c5 db 73 8e 03 40 99 23 26 9e 92 9b 29

    "
        .Trim()
        .Split(' ')
        .Select(c => byte.Parse(c, System.Globalization.NumberStyles.HexNumber))
        .ToArray();
}
List<object> ParseData(byte[] data)
{
    var contents = new List<object>();

    using (var stream = new MemoryStream(data))
    {
        while (stream.Position < stream.Length)
        {
            var position = stream.Position;
            try
            {
                Console.Write($"position={stream.Position,3}");
                var token = (byte)stream.ReadByte();
                Console.Write($", token={token:X2} ({Convert.ToString(token, 2).PadLeft(8, '0')})");

                var parser = tokenParsers.FirstOrDefault(x => (x.Key & token) == x.Key);
                Console.Write(", parser=" + Convert.ToString(parser.Key, 2).PadLeft(8, '0'));

                var result = parser.Value.Invoke(stream, (byte)(token & ~parser.Key));
                Console.WriteLine($", result=({result.GetType().Name}){result}");

                contents.Add(result);


                var buffer = new byte[stream.Position - position];
                stream.Position = position;
                stream.Read(buffer, 0, buffer.Length);

                string.Join(" ", buffer.Select(x => x.ToString("X2"))).Dump();
                Console.WriteLine();
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
            }
        }
    }

    return contents;
}
Dictionary<string, object> ParseMessage(List<object> contents)
{
    var typeProperties = new Dictionary<string, string>
    {
        ["spawnEnemy"] = "templateKey,id,rareType,spawnerKey,orbitStartTime,orbitAngle,orbitRadius,ellipseAlpha,ellipseFactor,angleVelocity,someAITypeFlag?,canCloak",
    }.ToDictionary(x => x.Key, x => x.Value.Split(',').Select(p => p.Trim()).ToArray());

    var length = (byte)contents[0];
    var type = (string)contents[1];

    if (!typeProperties.ContainsKey(type))
        throw new NotImplementedException(type);

    var properties = typeProperties[type];
    return contents
        .Skip(2)
        .Select((x, i) => new { Index = i, Value = x })
        .ToDictionary(x => properties.Length > x.Index ? properties[x.Index] : $"unknown{x.Index}", x => x.Value);
}

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

public static class Helper
{
    public static string BytesSequenceToHexadecimalString(this IEnumerable<byte> bytes, string separator)
    {
        return string.Join(separator, bytes);
    }
}
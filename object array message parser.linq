<Query Kind="Program">
  <NuGetReference>protobuf-net</NuGetReference>
  <Namespace>ProtoBuf</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
    var bytes = ByteArrayHelper.ParseRawHex(@"
0A FB 01 0A 09 41 72 74 69 66 61 63 74 73 12 16
68 48 4E 66 62 4C 42 4F 66 6B 69 61 47 73 6D 4B
65 41 41 52 4A 67 12 16 56 2D 4D 79 34 56 5A 31
36 45 53 73 31 74 52 6D 4C 4B 62 55 42 51 12 16
55 6C 2D 4A 67 31 61 77 46 6B 75 4A 63 5F 66 4A
48 78 43 73 57 77 12 16 35 6C 61 30 41 50 2D 6B
57 30 6D 49 63 4D 50 31 63 65 5A 38 38 77 12 16
43 68 51 49 79 58 79 75 6E 6B 6D 43 73 58 47 6C
71 50 75 37 2D 51 12 16 42 77 59 4F 30 58 6D 2D
49 6B 4F 32 55 59 63 53 49 6E 35 59 6A 51 12 16
4E 30 52 31 69 6D 77 6F 6A 30 2D 48 6E 56 56 36
65 68 76 32 37 77 12 16 38 42 77 6C 6C 64 57 44
2D 45 53 73 54 39 6E 58 62 65 37 5A 63 77 12 16
4A 41 32 5A 66 53 36 54 37 30 36 63 70 67 30 51
6B 4F 65 57 4A 67 12 16 52 56 46 65 51 57 62 68
47 30 2D 2D 50 32 39 4A 72 51 4B 4B 33 77
    ");
    var message = ProtoBufHelper.Parse<LoadArrayMessage<BigDbObject>>(bytes);
    message.Dump();
    
    ProtoBufHelper.GetKeyAob("Artifacts").Dump();
}

// Define other methods and classes here

#region DTO definitions
[ProtoContract]
public class LoadArrayMessage<TMessage>
{
    [ProtoMember(1)]
    public TMessage[] Objects { get; set; }
}

[ProtoContract]
public class LoadError
{
    [ProtoMember(1)] public int ErrorCode { get; set; }
    [ProtoMember(2)] public string Message { get; set; }

}

[ProtoContract]
public class BigDbObject
{
    [ProtoMember(1)]
    public string Key { get; set; }
    [ProtoMember(2)]
    public string Version { get; set; }
    [ProtoMember(3)]
    public Dictionary<string, ValueObject> Properties { get; set; }
    [ProtoMember(4)]
    public int CreatorID { get; set; }
}

[ProtoContract]
public class ObjectProperty
{
    [ProtoMember(1)] public string Name { get; set; }
    [ProtoMember(2)] public ValueObject Value { get; set; }
}

[ProtoContract]
public class ValueObject
{
    [ProtoMember(1)] public int Type { get; set; }
    [ProtoMember(2)] public string Text { get; set; }
    [ProtoMember(3)] public int Integer { get; set; }
    [ProtoMember(4)] public uint UnsignedInteger { get; set; }
    [ProtoMember(5)] public long Long { get; set; }
    [ProtoMember(6)] public bool Bool { get; set; }
    [ProtoMember(7)] public float Float { get; set; }
    [ProtoMember(8)] public double Double { get; set; }
    [ProtoMember(9)] public byte[] Bytes { get; set; }
    [ProtoMember(10)] public DateTime DateTime { get; set; }
    [ProtoMember(11)] public Dictionary<int, ValueObject> Array { get; set; }
    [ProtoMember(12)] public ObjectProperty Object { get; set; }

    private static Dictionary<int, Func<ValueObject, object>> ValueGetterMapping = typeof(ValueObject)
        .GetProperties()
        .Where(p => p.SetMethod != null)
        .ToDictionary<PropertyInfo, int, Func<ValueObject, object>>(
            p => p.GetCustomAttribute<ProtoMemberAttribute>().Tag,
            p => v => p.GetValue(v));
    public dynamic Value => ValueGetterMapping[Type + 2](this);
}

[ProtoContract]
public class ArrayProperty
{
    [ProtoMember(1)] public int Index { get; set; }
    [ProtoMember(2)] public ValueObject Value { get; set; }
}
#endregion

public static class ProtoBufHelper
{
    public static T Parse<T>(byte[] bytes)
    {
        using (var stream = new MemoryStream(bytes))
        {
            return Serializer.Deserialize<T>(stream);
        }
    }

    /// <summary>Get AOB of the key section of a KVP</summary>
    public static byte[] GetKeyAob(string key)
    {
        const byte FirstStringPropertyTag = 0b00001_010; // index: 1, tag: 2(variable length)

        return new byte[] { FirstStringPropertyTag, (byte)key.Length }
            .Concat(Encoding.UTF8.GetBytes(key))
            .ToArray();
    }
}
public static class ByteArrayHelper
{
    public static byte[] ParseRawHex(string raw)
    {
        return raw.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => byte.Parse(x, NumberStyles.HexNumber))
            .ToArray();
    }
}
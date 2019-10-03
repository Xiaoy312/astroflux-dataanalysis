<Query Kind="Program">
  <NuGetReference>FiddlerCore2</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>protobuf-net</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>Fiddler</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>ProtoBuf</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

void Main()
{
    Util.Cleanup += (s,e) => FiddlerApplication.Shutdown();
    
    var artifactsAOB = Encoding.UTF8.GetBytes("Artififacts").ToHashSet();
    ObserveSession()
        .Where(x => x.LocalProcess.StartsWith("astroflux"))
        .Where(x => x.fullUrl.StartsWith("https://api.playerio.com/api/85"))
        .Where(x => artifactsAOB.IsSubsetOf(x.RequestBody))
        .SelectMany(session => 
        {
            try
            {
                return Artifact.Parse(session.ResponseBody);
            }
            catch (Exception e)
            {
                string.Join("\n", session.RequestBody.Batch(16)
                    .Select(line => string.Join(" ", line.Select(b => b.ToString("X2")))))
                    .Dump("RequestBody");
                string.Join("\n", session.ResponseBody.Batch(16)
                    .Select(line => string.Join(" ", line.Select(b => b.ToString("X2")))))
                    .Dump("ResponseBody");
                    throw;
            }
        })
        .Dump();
}

// Define other methods and classes here
public IObservable<Session> ObserveSession()
{
    return Observable.Create<Session>(o =>
    {
        FiddlerApplication.AfterSessionComplete += o.OnNext;
        FiddlerApplication.Startup(8889, FiddlerCoreStartupFlags.Default);
        
        return Disposable.Create(() => FiddlerApplication.Shutdown());
    });
}

public class Artifact
{
    internal string ID { get; set; }
    public string Name { get; set; }
    internal string Bitmap { get; set; }
    public int LevelPotential { get; set; }
    public int Level { get; set; }
    public bool Revealed { get; set; }
    internal bool Upgraded { get; set; }
    internal List<ArtifactAttribute> Attributes { get; set; }
    public Dictionary<string, double> Stats => Attributes.GroupBy(x => x.BaseType).ToDictionary(g => g.Key, g => g.Sum(x => x.Value));

    public static Artifact FromBigDbObject(BigDbObject data)
    {
        return new Artifact
        {
            ID = data.Key,
            Name = data.Properties["name"].Text,
            Bitmap = data.Properties["bitmap"].Text,
            Level = data.Properties["level"].Integer,
            LevelPotential = data.Properties["levelPotential"].Integer,
            Revealed = data.Properties["revealed"].Bool,
            Attributes = data.Properties["stats"].Array.Values
                .Select(x => x.Object.Value)
                .Select(x => ArtifactAttribute.Create(x.Text, x.Double))
                .ToList()
        };
    }
    public static Artifact[] Parse(byte[] data)
    {
            using (var stream = new MemoryStream(data))
            using (var reader = new BinaryReader(stream))
            {
                var token = default(string);
                if (reader.ReadByte() != 0)
                {
                    var length = reader.ReadUInt16();
                    var buffer = reader.ReadBytes(length);
                    token = Encoding.UTF8.GetString(buffer);
                }

                var error = reader.Read() == 0;
                if (error)
                {
                    Serializer.Deserialize<LoadError>(stream).Dump();
                    throw new InvalidDataException();
                }

                var results = Serializer.Deserialize<LoadArrayMessage<BigDbObject>>(stream);
                return results.Objects
                    .Select(x => Artifact.FromBigDbObject(x))
                    .ToArray();
            }
    }
}
public class ArtifactAttribute
{
    public string Type { get; set; }
    public string BaseType => Regex.Replace(Type, @"\d$", string.Empty);
    public double RawValue { get; set; }
    public double Value { get; set; } // some values like shield multi is actually weaker than stated, ninja-nerfed

    public ArtifactAttribute(string type, double rawValue) : this(type, rawValue, rawValue) { }
    public ArtifactAttribute(string type, double rawValue, double value)
    {
        this.Type = type;
        this.RawValue = rawValue;
        this.Value = value;
    }

    public static ArtifactAttribute Create(string key, double value)
    {
        switch (key.PadRight(15))
        {
            case "healthAdd      ":
            case "healthAdd2     ":
            case "healthAdd3     ": return new ArtifactAttribute(key, value, 2 * value);
            case "healthMulti    ": return new ArtifactAttribute(key, value, 1.35 * value);
            case "armorAdd       ":
            case "armorAdd2      ":
            case "armorAdd3      ": return new ArtifactAttribute(key, value, 7.5 * value);
            case "armorMulti     ": return new ArtifactAttribute(key, value);
            case "corrosiveAdd   ":
            case "corrosiveAdd2  ":
            case "corrosiveAdd3  ": return new ArtifactAttribute(key, value, 4 * value);
            case "corrosiveMulti ": return new ArtifactAttribute(key, value);
            case "energyAdd      ":
            case "energyAdd2     ":
            case "energyAdd3     ": return new ArtifactAttribute(key, value, 4 * value);
            case "energyMulti    ": return new ArtifactAttribute(key, value);
            case "kineticAdd     ":
            case "kineticAdd2    ":
            case "kineticAdd3    ": return new ArtifactAttribute(key, value, 4 * value);
            case "kineticMulti   ": return new ArtifactAttribute(key, value);
            case "shieldAdd      ":
            case "shieldAdd2     ":
            case "shieldAdd3     ": return new ArtifactAttribute(key, value, 2 * value);
            case "shieldMulti    ": return new ArtifactAttribute(key, value, 1.35 * value);
            case "shieldRegen    ": return new ArtifactAttribute(key, value);
            case "corrosiveResist": return new ArtifactAttribute(key, value);
            case "energyResist   ": return new ArtifactAttribute(key, value);
            case "kineticResist  ": return new ArtifactAttribute(key, value);
            case "allResist      ": return new ArtifactAttribute(key, value);
            case "allAdd         ":
            case "allAdd2        ":
            case "allAdd3        ": return new ArtifactAttribute(key, value, 1.5 * value);
            case "allMulti       ": return new ArtifactAttribute(key, value, 1.35 * value);
            case "speed          ":
            case "speed2         ":
            case "speed3         ": return new ArtifactAttribute(key, value, 0.1 * value);
            case "refire         ":
            case "refire2        ":
            case "refire3        ": return new ArtifactAttribute(key, value, 0.3 * value);
            case "convHp         ": return new ArtifactAttribute(key, value, Math.Min(0.1 * value, 100));
            case "convShield     ": return new ArtifactAttribute(key, value, Math.Min(0.1 * value, 100));
            case "powerReg       ":
            case "powerReg2      ":
            case "powerReg3      ": return new ArtifactAttribute(key, value, 0.15 * value);
            case "powerMax       ": return new ArtifactAttribute(key, value);
            case "cooldown       ":
            case "cooldown2      ":
            case "cooldown3      ": return new ArtifactAttribute(key, value, 0.1 * value);

            default: return new ArtifactAttribute(key, value, double.NaN);
        }
    }
}

#region ProtoBuf
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
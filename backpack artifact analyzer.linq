<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>protobuf-net</NuGetReference>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>ProtoBuf</Namespace>
</Query>

void Main()
{
    var successResponseIndicator = new byte[] { 0, 1 };
    
    var data = LoadData("temp.hexstring");
    if (!data.StartsWith(successResponseIndicator))
    {
        ProtoBufHelper.Parse<LoadError>(data.Skip(successResponseIndicator.Length)).Dump();
        throw new InvalidDataException();
    }
    
    var artifactDbObjects = ProtoBufHelper.Parse<LoadArrayMessage<BigDbObject>>(data.Skip(successResponseIndicator.Length)).Objects;
    var artifacts = artifactDbObjects.Select(Artifact.FromBigDbObject).ToList();
    
    artifacts
        .SelectMany(x => x.Attributes
            .GroupBy(affix => affix.BaseType, (k, g) => new { BaseType = k, Value = g.Sum(affix => affix.Value) })
            .Select(affix => new { Affix = affix, Artifact = x }))
        .GroupBy(x => x.Affix.BaseType, (k, g) => new
        {
            Type = k, 
            Artifacts = g
                .OrderByDescending(x => x.Affix.Value)
                .Select(x => x.Artifact)
                .Take(5)
                .ToList()
        })
        .Select(x => new
        {
            x.Type, 
            FoundNew = x.Artifacts.Any(art => !art.Revealed) ? "X" : null,
            Artifacts = Util.HighlightIf(x.Artifacts, arts => arts.Any(art => !art.Revealed))
        })
        .Dump("Top 5s of each Category", 2);
}

// Define other methods and classes here
byte[] LoadData(string file)
{
    var hex = File.ReadAllText(Util.CurrentQuery.GetFullPath(file));
    return hex.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => byte.Parse(x, NumberStyles.HexNumber))
        .ToArray();
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
    public List<ArtifactAttribute> Attributes { get; set; }

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
            //Upgraded = data.Properties["levelPotential"].Bool,
            Attributes = data.Properties["stats"].Array.Values
                .Select(x => x.Object.Value)
                .Select(x => ArtifactAttribute.Create(x.Text, x.Double))
                .ToList()
        };
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
    public static T Parse<T>(IEnumerable<byte> source) => Parse<T>(source.ToArray());
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

    public static string PrettyPrint(this IEnumerable<byte> bytes)
    {
        return string.Join("\n", bytes.Batch(16)
            .Select(line => string.Join(" ", line.Select(b => b.ToString("X2"))))
        );
    }
}
public static class Expandable
{
    public static object AsExpandable(this byte[] bytes, int maxLength = 16 * 5)
    {
        if (bytes.Length <= maxLength)
            return new DumpContainer(bytes.PrettyPrint());

        Action showSimpleView = null, showDetailView = null;
        var container = new DumpContainer();

        showSimpleView = () => container.Content = Util.VerticalRun(
            bytes.Take(maxLength).PrettyPrint(),
            Clickable.Create("more...", showDetailView)
        );
        showDetailView = () => container.Content = Util.VerticalRun(
            bytes.PrettyPrint(),
            Util.HorizontalRun(true, "===", Clickable.Create("^less", showSimpleView), "===")
        );

        showSimpleView();

        return container;
    }
    public static object TryEval(string header, Func<object> eval)
    {
        var container = new DumpContainer();
        container.Content = Clickable.Create(header, () =>
        {
            try
            {
                container.Content = eval();
            }
            catch (Exception e)
            {
                container.Content = e;
            }
        });

        return container;
    }
}
public static class Clickable
{
    public static Hyperlinq Create(string header, Action action) => new Hyperlinq(action, header);

    public static Hyperlinq CopyText(string text) => CopyText(text, text);
    public static Hyperlinq CopyText(string header, string text)
    {
        return Create(header, () => Clipboard.SetText(text));
    }
    //    public static Hyperlinq OpenInVisualStudio(string header, string path)
    //    {
    //        return Create(header, () => Util.Cmd(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe", $"-edit {path}"));
    //    }
    //    public static Hyperlinq OpenInVSCode(string header, string path)
    //    {
    //        return Create(header, () => Util.Cmd(@"code", $"-r {path}"));
    //    }
}
public static class QueryHelper
{
    /// <summary>Make a relative path to full based on location of current query.</summary>
    public static string GetFullPath(this LINQPad.ObjectModel.Query query, string path)
    {
        path = Path.Combine(query.Location, path);

        // make directory separators all from one direction
        if (path.Contains(Path.AltDirectorySeparatorChar))
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        return path;
    }
}
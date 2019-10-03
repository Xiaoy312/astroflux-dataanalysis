<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <NuGetReference>FiddlerCore2</NuGetReference>
  <NuGetReference>morelinq</NuGetReference>
  <NuGetReference>protobuf-net</NuGetReference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>Fiddler</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>ProtoBuf</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
    Util.Cleanup += (s, e) => FiddlerApplication.Shutdown();

    var successResponseIndicator = new byte[] { 0, 1 };
    Func<Session, int, string, bool> isRequest = (session, endpoint, key) =>
        session.fullUrl == $"https://api.playerio.com/api/{endpoint}" &&
        ProtoBufHelper.GetKeyAob(key).ToHashSet().IsSubsetOf(session.RequestBody);
    
    ObserveSession()
        .Where(x => x.LocalProcess.StartsWith("astroflux"))
        //.Where(x => x.fullUrl.StartsWith("https://api.playerio.com/api/" + ApiEndpoints.LoadObjects))
        //.Where(x => isRequest(x, ApiEndpoints.LoadObjects, "Artifacts")) // loads artifacts in setups
        .Where(x => isRequest(x, ApiEndpoints.loadIndexRange, "Artifacts")) // loads artifacts in setups
        .Select(session =>
        {
            try
            {
                return new
                {
                    FullUrl = session.fullUrl,
                    Request = Util.VerticalRun(
                        Clickable.CopyText("Copy", session.RequestBody.PrettyPrint()),
                        session.RequestBody.AsExpandable()
                        //,Expandable.TryEval("Parse RequestBody", () => ProtoBufHelper.Parse<TODO:LoadIndexRangeOutputMessage>(session.RequestBody))
                    ),
                    Response = Util.VerticalRun(
                        Clickable.CopyText("Copy", session.ResponseBody.PrettyPrint()),
                        session.ResponseBody.AsExpandable(),
                        Expandable.TryEval("Parse ResponseBody", () => session.ResponseBody.StartsWith(successResponseIndicator)
                            ? ProtoBufHelper.Parse<LoadArrayMessage<BigDbObject>>(session.ResponseBody.Skip(successResponseIndicator.Length)).Objects
                            : ProtoBufHelper.Parse<LoadError>(session.ResponseBody.Skip(successResponseIndicator.Length)) as object
                        )
                    ),
                };
            }
            catch (Exception e)
            {
                Util.VerticalRun(
                    $"===== {e.GetType().Name} =====",
                    e,
                    $"====== Request size={session.RequestBody.Length} =====",
                    session.RequestBody.PrettyPrint(),
                    $"====== Response size={session.ResponseBody.Length} =====",
                    session.ResponseBody.PrettyPrint()
                ).Dump(session.fullUrl);
                return null;
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

public class ApiEndpoints
{
    // search /Request\(\d+/ in *.as
    public const int Authenticate = 13;
    public const int JoinRoom = 27;
    public const int ListRooms = 30;
    public const int LoadObjects = 85;
    public const int loadIndexRange = 97;
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
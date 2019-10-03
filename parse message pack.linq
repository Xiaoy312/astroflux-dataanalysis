<Query Kind="Program" />

void Main()
{
    var msgPack = new Message()
    {
        Type = "msgPack",
        Contents = GetPackContents()
    };
    
    ParsePack(msgPack);
}

// Define other methods and classes here
List<object> GetPackContents()
{
    return @"c 
17 
650145 
AIChase 
1 
-2.29586997996464E-304 
-999847 
949892 
4635 
-3257 
5670 
True 
False 
False 
False 
650047 
0 
c 
17 
650124 
AIIdle 
0 
-2.29586997996464E-304 
-964927 
197432 
0 
0 
4379 
False 
False 
False 
False 
0 
0 
c 
17 
650123 
AIIdle 
0 
-2.29586997996464E-304 
-1102401 
-485026 
0 
0 
4724 
False 
False 
False 
False 
0 
0 
c 
17 
650066 
AITeleportExit 
0 
-2.29586997996464E-304 
-1969241 
-334023 
-2063 
-3966 
4232 
False 
False 
False 
False 
0 
0".Trim().Split('\n').Select(x => x.Trim()).Cast<object>().ToList();
}
void ParsePack(Message pack)
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
                case "c": Message.FromMessagePack(pack, "asd", i + 2, length - 2).Dump(); break;
                
                default:
                    type.Dump();
                    break;
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Failed to parse `msgPack`: {e}");
        Console.WriteLine($"Type: {type}");
    }
}

public class Message
{
    public string Type { get; set; }
    public List<object> Contents { get; set; } = new List<object>();

    public T Get<T>(int index) => (T)Contents[index];
    public int GetLength(int index) => int.Parse((string)Contents[index]);
    
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
using System.Reflection;

namespace WorldGen.NoddleLib;

public class OutputPort : Port
{
    public OutputPort(FieldInfo member, Node node) : base(member, node)
    {
    }
    
    public object? GetData()
    {
        return PinMember.GetValue(Node);
    }
    
}
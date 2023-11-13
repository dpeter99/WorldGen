using System.Reflection;

namespace WorldGen.NoddleLib;

public class Port
{
    /// <summary>
    /// The name of this pin
    /// </summary>
    public string Name => PinMember.Name; 
    
    /// <summary>
    /// The member that represents this pin.
    /// </summary>
    public FieldInfo PinMember { get; }

    /// <summary>
    /// The node this pin is on.
    /// </summary>
    public Node Node;

    /// <summary>
    /// The type that this pin's field has.
    /// </summary>
    public Type FieldType => PinMember.FieldType;
    
    public Port(FieldInfo member, Node node)
    {
        PinMember = member;
        Node = node;
    }
}
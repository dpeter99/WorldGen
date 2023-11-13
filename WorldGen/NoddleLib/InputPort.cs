using System.Reflection;

namespace WorldGen.NoddleLib;

public class InputPort : Port
{

    /// <summary>
    /// The type that represents this port.
    /// For multi-pin ports, this would be a collection like List<> or an array.
    /// For single-pin ports, this is the same as PinType.
    /// </summary>
    public Type OuterType => this.FieldType;

    /// <summary>
    /// The type that represents each pin.
    /// </summary>
    public Type PinType { get; }

    /// <summary>
    /// If true, there can be an arbitrary number of pins connected at once to this port.
    /// </summary>
    public bool MultiPin { get; }

    
    public List<Connection> Connections;
    
    public InputPort(FieldInfo pinMember, Node node, Type pinType, bool multiPin) : base(pinMember, node)
    {
        PinType = pinType;
        MultiPin = multiPin;
        Connections = new List<Connection>();
    }
    
    public void AddConnection(Connection connection)
    {
        Connections.Add(connection);
    }
    
    public void RemoveConnection(Connection connection)
    {
        Connections.Remove(connection);
    }
    
    public void SetData(object? outputData, Connection connection)
    {
        PinMember.SetValue(Node, outputData);
    }
}
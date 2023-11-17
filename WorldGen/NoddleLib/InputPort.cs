using System.Collections;
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

    public bool Primary { get; set; }


    public List<Connection> Connections = new();
    
    public Dictionary<Connection, object?> listMapping = new();
    
    public InputPort(FieldInfo pinMember, Node node, Type pinType, bool multiPin, bool primary) : base(pinMember, node)
    {
        PinType = pinType;
        MultiPin = multiPin;
        Primary = primary;

        if (MultiPin)
        {
            if (!typeof(IList).IsAssignableFrom(OuterType))
            {
                throw new Exception("Port: " + pinMember.Name + " on: " + node.GetType().Name + " is marked as MultiPin, but is not a list type.");
            }
        }
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
        if(MultiPin)
        {
            var list = (IList)(PinMember.GetValue(Node) ?? throw new Exception("MultiPin port was null."));
            
            if(listMapping.TryGetValue(connection, out var value))
            {
                list.Remove(value);
            }
            
            list.Add(outputData);
            listMapping[connection] = outputData;
        }
        else
        {
            PinMember.SetValue(Node, outputData);
        }
    }
}
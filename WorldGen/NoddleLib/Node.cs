using System.Reflection;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.NoddleLib;

public class Node :INode
{
    private IReadOnlyCollection<InputPort> inputs;
    
    private IReadOnlyCollection<OutputPort> outputs;
    
    public Node(Type type)
    {
        List<InputPort> inputs = new();
        List<OutputPort> outputs = new();

        foreach (var field in type.GetFields())
        {
            var inputAttr = field.GetCustomAttribute<InputAttribute>();
            if (inputAttr != null)
            {
                var multi = field.GetCustomAttribute<MultiPinAttribute>();

                bool isMultiPin = multi != null;

                var outerType = field.FieldType;
                var pinType = field.FieldType;

                if (isMultiPin) 
                {
                    if (outerType.IsArray)
                    {
                        pinType = outerType.GetElementType();
                    }
                    else if (outerType.IsGenericType && outerType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        pinType = outerType.GetGenericArguments()[0];
                    }
                }

                var inputPort = new InputPort(field.Name, outerType, pinType, isMultiPin);

                inputs.Add(inputPort);
            }

            var outputAttr = field.GetCustomAttribute<OutputAttribute>();
            if (outputAttr != null)
            {
                var outputPort = new OutputPort(field.Name, field.FieldType);

                outputs.Add(outputPort);
            }
        }

        this.inputs = inputs.AsReadOnly();
        this.outputs = outputs.AsReadOnly();
        //return new NodeType(type.Name, type, inputs.AsReadOnly(), outputs.AsReadOnly());
    }
    
    public void Process()
    {
        throw new NotImplementedException();
    }
}
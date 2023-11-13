using System.Reflection;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.NoddleLib;

public abstract class Node :INode
{
    private IReadOnlyCollection<InputPort> inputs;
    private InputPort primaryInput;
    
    private IReadOnlyCollection<OutputPort> outputs;
    private OutputPort primaryOutput;
    
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

                var inputPort = new InputPort(field, this, pinType, isMultiPin);

                inputs.Add(inputPort);
            }

            var outputAttr = field.GetCustomAttribute<OutputAttribute>();
            if (outputAttr != null)
            {
                var outputPort = new OutputPort(field, this);

                outputs.Add(outputPort);
            }
        }

        this.inputs = inputs.AsReadOnly();
        if (inputs.Count > 0)
            primaryInput = inputs[0];
        this.outputs = outputs.AsReadOnly();
        if (outputs.Count > 0)
            primaryOutput = outputs[0];
        //return new NodeType(type.Name, type, inputs.AsReadOnly(), outputs.AsReadOnly());
    }

    public abstract void Process();

    public OutputPort GetPrimaryOutput()
    {
        return primaryOutput;
    }

    public InputPort GetPrimaryInput()
    {
        return primaryInput;
    }
}
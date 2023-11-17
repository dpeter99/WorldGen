using System.Reflection;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.NoddleLib;

public abstract class Node :INode
{
    private IReadOnlyCollection<InputPort> inputs;
    private InputPort? primaryInput;
    
    private IReadOnlyCollection<OutputPort> outputs;
    private OutputPort? primaryOutput;
    
    public Node()
    {
        Type type = this.GetType(); 
        
        List<InputPort> inputs = new();
        List<OutputPort> outputs = new();

        foreach (var field in type.GetFields())
        {
            var inputAttr = field.GetCustomAttribute<InputAttribute>();
            if (inputAttr != null)
            {
                var multi = field.GetCustomAttribute<MultiPinAttribute>();
                bool isMultiPin = multi != null;
                
                var primaryAttribute = field.GetCustomAttribute<PrimaryPinAttribute>();
                bool isPrimary = primaryAttribute != null;

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

                var inputPort = new InputPort(field, this, pinType, isMultiPin, isPrimary);

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
        if (inputs.Count == 1)
            primaryInput = inputs[0];
        
        var primInput = inputs.Where(i => i.Primary).ToList();
        if (primInput.Count > 1)
            throw new Exception("Can't have more than one Primary Pin");
        if (primInput.FirstOrDefault() is not null)
            primaryInput = primInput.First();
        
        this.outputs = outputs.AsReadOnly();
        if (outputs.Count == 1)
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

    public OutputPort? Output(string name)
    {
        return outputs.FirstOrDefault(i => i.Name == name);
    }
    
    public InputPort? Input(string name)
    {
        return inputs.FirstOrDefault(i => i.Name == name);
    }
}

public class PrimaryPinAttribute : Attribute
{
}
namespace WorldGen.NoddleLib;

public class Connection
{
    public InputPort input;

    public OutputPort output;

    public Node InputNode;
    public Node OutputNode;

    public Connection(Node outputNode, OutputPort outputPort, Node inputNode, InputPort inputPort)
    {
        OutputNode = outputNode;
        output = outputPort;
        InputNode = inputNode;
        input = inputPort;
    }
}
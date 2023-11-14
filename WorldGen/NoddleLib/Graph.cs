namespace WorldGen.NoddleLib;

public class Graph
{
    private List<Node> _nodes = new();

    private List<Connection> _connections = new();
    
    public Node AddNode(Node node)
    {
        _nodes.Add(node);
        return node;
    }

    public void RemoveNode(Node node)
    {
        // Find and remove all connections associated with this node
        var connectionsToRemove = _connections.Where(connection =>
            connection.OutputNode == node || connection.InputNode == node).ToList();

        foreach (var connection in connectionsToRemove)
        {
            // Update the input port of the connected node to remove the connection
            if (connection.InputNode == node)
            {
                connection.input.RemoveConnection(connection);
            }
            // Remove the connection from the graph
            _connections.Remove(connection);
        }

        // Finally, remove the node itself
        _nodes.Remove(node);
    }
   
    public void CreateConnection(Node outputNode, OutputPort outputPort, Node inputNode, InputPort inputPort)
    {
        // Validate nodes and ports before creating a connection
        if (!_nodes.Contains(outputNode) || !_nodes.Contains(inputNode))
        {
            throw new InvalidOperationException("Nodes must be part of the graph.");
        }
        
        var connection = new Connection(outputNode, outputPort, inputNode, inputPort);
        _connections.Add(connection);
        
        inputPort.AddConnection(connection);
    }
    
    public void CreateConnection(Node outputNode, Node inputNode)
    {
        CreateConnection(outputNode, outputNode.GetPrimaryOutput(), inputNode, inputNode.GetPrimaryInput());
    }
    
    public void RemoveConnection(Connection connection)
    {
        _connections.Remove(connection);

        // Update the input port to remove the disconnected connection
        connection.input.RemoveConnection(connection);
    }
    
    public void Execute()
    {
        // Execute each node in the order they were added
        foreach (var node in _nodes)
        {
            // Now process the node
            node.Process();

            // After processing, pass the output data to the connected nodes
            var relevantConnections = _connections.Where(c => c.OutputNode == node);
            foreach (var connection in relevantConnections)
            {
                var outputData = connection.output.GetData();
                connection.input.SetData(outputData, connection);
            }
        }
    }
}
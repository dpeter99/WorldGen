using System.Numerics;

namespace WorldGen.Nodes;

public class AdditionNode<A> : Node where A : IAdditionOperators<A,A,A> 
{
    [Input] public A a { get; set; }

    [Input]
    public A b { get; set; }

    [Output]
    public A output { get; set; }
    
    
    
    public override void Process()
    {
        output = a + b;
    }
}
using System.Numerics;
using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes;

public class AddNode<T> : Node where T : IAdditionOperators<T, T, T>
{
    [Input, MultiPin] public List<I1DDataReadOnly<T>> input = new ();

    [Output] public I1DData<T> output;
    
    public override void Process()
    {
        output = input[0].MutableCopy();

        for (int i = 0; i < input[0].Length; i++)
        {
            for (int j = 1; j < input.Count; j++)
            {
                output[i] += input[j][i];
            }
        }
    }
}
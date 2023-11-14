using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes;

public class Constant2DFieldNode : Node
{
    
    [Property] public int Width = 200;
    [Property] public int Height = 200;
    
    [Property] public int Value = 1;
    
    [Output] public I2DData<float> Field;
    
    public override void Process()
    {
        var data = new Array2D<float>(Width, Height);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Value;
        }

        Field = data;
    }
}
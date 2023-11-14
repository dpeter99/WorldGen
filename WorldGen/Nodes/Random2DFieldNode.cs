using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes;

public class Random2DFieldNode : Node
{
    [Property] public int Width = 200;
    [Property] public int Height = 200;
    
    [Property] public int Min = 0;
    [Property] public int Max = 1;
    
    [Property] public int Seed = 1;

    [Output] public I2DData<double> Field;

    public override void Process()
    {
        var rnd = new Random(Seed);
        
        var data = new Array2D<double>(Width, Height);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = rnd.Next(Min, Max);
        }

        Field = data;
    }
}
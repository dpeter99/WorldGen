using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes.Noise2D;

public class NoiseFiled2DNode : Node
{
    [Property] public int Seed { get; set; }
    
    [Property] public int Width { get; set; }
    [Property] public int Height { get; set; }
    
    [Property] public int Min { get; set; }
    [Property] public int Max { get; set; }
    
    [Property] public FastNoiseLite.NoiseType NoiseType { get; set; }

    [Output] public I2DData<double> Output;
    
    public override void Process()
    {
        var noise = new FastNoiseLite(Seed);
        noise.SetNoiseType(NoiseType);
        noise.SetSeed(Seed);

        var data = new Array2D<double>(Width, Height);

        for (var x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var num = (noise.GetNoise(x, y) + 1d)/2d;
                var value = num * (Max - Min) + Min;
                data[x, y] = value;
            }
        }

        Output = data;
    }
}
using System.Numerics;
using Aper_bot.Util;
using WorldGen.Values;

namespace WorldGen.Nodes;

public class NoiseField2D : Node
{
    [Output] public Field2D output { get; set; }

    protected FastNoiseLite noise;

    public NoiseField2D(FastNoiseLite.NoiseType type)
    {
        noise = new FastNoiseLite().Apply(n =>
        {
            n.SetNoiseType(type);
        });
    }

    public override void Process()
    {
        output = new Field2D();
        output.AddLayer(((pos, d) =>
        {
            return noise.GetNoise(pos.X, pos.Y);
        }));
    }
}

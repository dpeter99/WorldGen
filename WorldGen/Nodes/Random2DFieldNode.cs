using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes;

public class Random2DFieldNode : Node
{
    private readonly int _width;
    private readonly int _height;
    
    [Output]
    public I2DData<float>? Field;
    
    public Random2DFieldNode(int width, int height) : base(typeof(Random2DFieldNode))
    {
        _width = width;
        _height = height;
    }

    public override void Process()
    {
        var data = new Array2D<float>(_width, _height);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Random.Shared.Next();
        }

        Field = data;
    }
}
using WorldGen.NoddleLib;
using WorldGen.Nodes;
using WorldGen.Nodes.CSV;
using WorldGen.Nodes.Noise2D;
using WorldGen.Nodes.TiledNodes;
using WorldGen.Utils;

var g = new Graph();

var randomField = g.AddNode(new NoiseFiled2DNode()
{
    Width = 200,
    Height = 200,
    Min = 0,
    Max = 70,
    Seed = 1234,
    NoiseType = FastNoiseLite.NoiseType.Perlin,
});
var round = g.AddNode(new RoundArrayNode());
g.CreateConnection(randomField, round);


var simple = g.AddNode(new Constant2DFieldNode()
{
    Width = 200,
    Height = 200,
    Value = 79,
});
var round2 = g.AddNode(new RoundArrayNode());
g.CreateConnection(simple, round2);

var tsx = g.AddNode(new ImportTSXNode()
{
    InputFile = new FileInfo("/home/dpeter99/Desktop/test.tsx"),
});

var output = g.AddNode(new SaveTMXNode("test.tmx"));
g.CreateConnection(round2, output);
g.CreateConnection(round, output);

g.Execute();

Console.Write("hi");
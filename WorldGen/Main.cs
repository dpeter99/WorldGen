using WorldGen.NoddleLib;
using WorldGen.Nodes;
using WorldGen.Nodes.CSV;
using WorldGen.Nodes.Noise2D;
using WorldGen.Nodes.TiledNodes;
using WorldGen.Nodes.Wang;
using WorldGen.Utils;

var g = new Graph();

var randomField = g.AddNode(new NoiseFiled2DNode()
{
    Width = 50,
    Height = 50,
    Min = 0,
    Max = 10,
    Seed = 1234,
    Size = 10f,
    NoiseType = FastNoiseLite.NoiseType.Perlin,
});
var round = g.AddNode(new RoundArrayNode());
g.CreateConnection(randomField, round);

var remap = g.AddNode(new MapValuesNode<int, int>
{
    MappingDictionary = new Dictionary<int, int>() { {0, 79}, {6, 11} },
    MapType = MapType.GreaterThan,
});
g.CreateConnection(round, remap);


var tsx = g.AddNode(new ImportTSXNode()
{
    InputFile = new FileInfo("Assets/test_2.tsx"),
});


var wang = g.AddNode(new WangTileNode());
g.CreateConnection(remap, wang);
g.CreateConnection(tsx, wang, wang.Input("TileSet")!);

var output = g.AddNode(new SaveTMXNode("test.tmx"));
g.CreateConnection(wang, output);
g.CreateConnection(remap, output);

g.Execute();

Console.Write("hi");
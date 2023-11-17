using WorldGen.NoddleLib;
using WorldGen.Nodes;
using WorldGen.Nodes.CSV;
using WorldGen.Nodes.Noise2D;
using WorldGen.Nodes.Texture;
using WorldGen.Nodes.TiledNodes;
using WorldGen.Nodes.Wang;
using WorldGen.Utils;

var g = new Graph();

Node heightField;

{
    var randomField = g.AddNode(new NoiseFiled2DNode()
    {
        Width = 500,
        Height = 500,
        Min = 0,
        Max = 50,
        Seed = 1234,
        Size = 0.5f,
        NoiseType = FastNoiseLite.NoiseType.Perlin,
    });

    var smallDetail = g.AddNode(new NoiseFiled2DNode()
    {
        Width = 500,
        Height = 500,
        Min = 0,
        Max = 20,
        Seed = 1234,
        Size = 1f,
        NoiseType = FastNoiseLite.NoiseType.Perlin,
    });
    var add = g.AddNode(new AddNode<double>());
    g.CreateConnection(randomField, add);
    g.CreateConnection(smallDetail, add);


    var round = g.AddNode(new RoundArrayNode());
    g.CreateConnection(add, round);
    
    heightField = round;
}

var terraces = g.AddNode(new MapValuesNode<int, int>
{
    MappingDictionary = new Dictionary<int, int>() { {0, 0}, {20, 1}, {30, 2}, {40, 3}, {45, 4} },
    MapType = MapType.GreaterThan,
});
g.CreateConnection(heightField, terraces);

var visualize = g.AddNode(new ExportTextureNode(){ Remap = true,});
g.CreateConnection(terraces, visualize);



var remap = g.AddNode(new MapValuesNode<int, int>
{
    MappingDictionary = new Dictionary<int, int>() { {0, 79}, {6, 11} },
    MapType = MapType.GreaterThan,
});
g.CreateConnection(heightField, remap);


var tsx = g.AddNode(new ImportTSXNode()
{
    InputFile = new FileInfo("Assets/test_2.tsx"),
});


var wang = g.AddNode(new WangTileNode());
g.CreateConnection(remap, wang);
g.CreateConnection(tsx, wang, wang.Input("TileSet")!);

var output = g.AddNode(new SaveTMXNode("test.tmx"));

g.CreateConnection(remap, output);
g.CreateConnection(terraces, output);


g.Execute();

Console.Write("hi");
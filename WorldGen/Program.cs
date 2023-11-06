// See https://aka.ms/new-console-template for more information

using System.Numerics;
using WorldGen;
using WorldGen.Nodes;
using WorldGen.TiledLib;
using WorldGen.Values;

var g = new Graph();

//Generate a Perlin noise in the -1 to 1 range
var perlin = g.AddNode(new NoiseField2D(FastNoiseLite.NoiseType.Perlin));

//Normalize it to the 0 - 1 range 
var normalize = g.AddNode(new NormalizeField2DNode()
{
    Min = -1,
    Max = 1,
});
g.Connect(
    perlin.GetOutput("output"),
    normalize.GetInput("input")
);

//Limit the values to 10 different options
var q = g.AddNode(new QuantizeFieldValuesNode()
{
    StepCount = 10,
});
g.Connect(
    normalize.GetOutput("output"),
    q.GetInput("input")
);


var sample = g.AddNode(new SampleField2DNode()
{
    EndPos = new Vector2(200, 200), 
});
g.Connect(
    q.GetOutput("output"),
    sample.GetInput("input")
);

var export = g.AddNode(new ExportImageNodeDouble());
export.file = new FileInfo("./perlin.png");
g.Connect(
    sample.GetOutput("output"),
    export.GetInput("input")
    );

var mapValues = g.AddNode(new MapValuesNode<Table2D<double>, double, int>());
mapValues.Map = new()
{
    {0.3, (int)Type.Grass},
    {0.4, (int)Type.Dirt},
    {0.61, (int)Type.Cobble },
};
g.Connect(
    sample.GetOutput("output"),
    mapValues.GetInput("input")
);


var tileSet = g.AddNode(new ImportTsxNode());
tileSet.TsxFile = new FileInfo("/home/dpeter99/Desktop/test.tsx");

var wangRules = g.AddNode(new WangTileRulesNode());
g.Connect(
    tileSet.GetOutput("output"),
    wangRules.GetInput("tileset")
    );

g.Connect(
    mapValues.GetOutput("output"),
    wangRules.GetInput("input")
);

var exportTiled = g.AddNode(new ExportTmxNode());
exportTiled.file = new FileInfo("./map.tmx");
g.Connect(
    wangRules.GetOutput("output"),
    exportTiled.GetInput("input")
);

g.Run();

enum Type : int
{
    Grass = 78,
    Dirt = 10,
    Cobble = 124,
};
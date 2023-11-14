

using WorldGen.NoddleLib;
using WorldGen.Nodes;
using WorldGen.Nodes.CSV;
using WorldGen.Nodes.TiledNodes;
using WorldGen.Utils;

Console.Write("hi");


var g = new Graph();

var randomField = g.AddNode(new Random2DFieldNode().Apply(n =>
{
    n.Min = 0;
    n.Max = 70;
    n.Seed = 1234;
}));

var round = g.AddNode(new RoundArrayNode());
g.CreateConnection(randomField, round);


var simple = g.AddNode(new Constant2DFieldNode().Apply(n =>
{
    n.Width = 200;
    n.Height = 200;
    n.Value = 79;
}));
var round2 = g.AddNode(new RoundArrayNode());
g.CreateConnection(simple, round2);






var output = g.AddNode(new SaveTMXNode("test.tmx"));
g.CreateConnection(round2, output);
g.CreateConnection(round, output);


g.Execute();
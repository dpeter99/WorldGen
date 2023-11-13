

using WorldGen.NoddleLib;
using WorldGen.Nodes;
using WorldGen.Nodes.CSV;

Console.Write("hi");


var g = new Graph();

var randomField = g.AddNode(new Random2DFieldNode(200,200));

var output = g.AddNode(new ExportCsvNode(new FileInfo("test.csv")));

g.CreateConnection(randomField, output);

g.Execute();
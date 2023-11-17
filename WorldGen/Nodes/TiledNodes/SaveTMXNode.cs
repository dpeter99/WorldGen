using System.Numerics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;
using WorldGen.TiledLib;
using WorldGen.Utils;

namespace WorldGen.Nodes.TiledNodes;

public class SaveTMXNode : Node
{
    [Input, MultiPin] public List<I2DDataReadOnly<int>> Layers = new();

    public FileInfo OutputFile { get; private set; }

    public SaveTMXNode(string outputFilePath)
    {
        OutputFile = new FileInfo(outputFilePath);
    }

    public override void Process()
    {
        if (Layers == null || Layers.Count == 0)
        {
            throw new InvalidOperationException("No layers provided to the TMX loader.");
        }
        
        var map = new TiledMap(Layers[0].Width, Layers[0].Height).Apply(map =>
        {
            map.Tilesets.Add(new TileSet
            {
                Source = "Assets/test_2.tsx",
                Firstgid = 1,
            });
            map.Tileheight = 32;
            map.Tilewidth = 32;
        });
        
        
        

        foreach (var layerData in Layers)
        {
            var layer = new Layer(Layers[0].Width, Layers[0].Height)
            {
                Name = "Layer " + map.Layers.Count,
                Data = new Data
                {
                    MapData = layerData.MutableCopy().AsArray()
                }
            };

            map.Layers.Add(layer);
        }

        var ser = new XmlSerializer(typeof(TiledMap));
        ser.Serialize(OutputFile.OpenWrite(), map);
    }
}

using System.Xml.Serialization;
using WorldGen.TiledLib;
using WorldGen.Values;

namespace WorldGen.Nodes;

public class ExportTmxNode: Node
{
    [Input] public I2DDataStructureReadOnly<int> input { get; set; }
    
    public FileInfo file { get; set; }
    
    public override void Process()
    {
        TiledMap map = new TiledMap(200,200);
        XmlSerializer s = new XmlSerializer(typeof(TiledMap));

        var l = map.AddLayer();
        
        for (int x = 0; x < 200; x++)
        {
            for (int y = 0; y < 200; y++)
            {
                int id = input[x, y];
                l.Data.Data[x, y] = id + map.TileSets[0].FirstGridID;
            }
        }
        
        s.Serialize(file.Open(FileMode.Create),map);
    }
}
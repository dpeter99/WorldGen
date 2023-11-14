using System.Reflection;
using System.Xml.Serialization;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;
using WorldGen.TiledLib;

namespace WorldGen.Nodes.TiledNodes;

public class ImportTSXNode : Node
{
    [Property] public FileInfo? InputFile;
    
    [Output] public TileSet? output;
    
    public override void Process()
    {
        if (InputFile is null || !InputFile.Exists)
        {
            throw new Exception("File is null or does not exist");
        }

        TileSet tileSet;
        
        try
        {
            var ser = new XmlSerializer(typeof(TileSet));
            tileSet = (TileSet)ser.Deserialize(InputFile.OpenText())!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        output = tileSet;
    }
}
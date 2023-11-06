namespace WorldGen.Nodes;

using System.Xml.Serialization;
using WorldGen.TiledLib;
using WorldGen.Values;

public class ImportTmxNode : Node
{
    [Output]
    public I2DDataStructure<int> output { get; set; }
    
    public FileInfo file { get; set; }

    public override void Process()
    {
        if (!file.Exists)
            throw new FileNotFoundException("TMX file not found.", file.FullName);

        // Deserialize the TMX file into a TiledMap object
        TiledMap map;
        XmlSerializer serializer = new XmlSerializer(typeof(TiledMap));
        using (var stream = file.OpenRead())
        {
            map = (TiledMap)serializer.Deserialize(stream);
        }

        // Create a writable 2D data structure for the output
        var outputGrid = CreateWritableDataStructure(map.Width, map.Height);

        // Assume there is at least one layer and one tileset
        var l = map.Layers[0];
        var firstGid = map.TileSets[0].FirstGridID;

        // Iterate over the map's tiles and assign to output grid
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                int id = l.Data.Data[x, y] - firstGid;
                outputGrid[x, y] = id;
            }
        }

        output = outputGrid;
    }

    private I2DDataStructure<int> CreateWritableDataStructure(int width, int height)
    {
        // This method should create an instance of your I2DDataStructure<int>
        // that is writable. You may have a specific implementation of this
        // interface that allows you to create a new data structure with the given dimensions.
        // For example:
        // return new Your2DDataStructureImplementation(width, height);
        throw new NotImplementedException();
    }
}

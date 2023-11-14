using System.Xml.Serialization;

namespace WorldGen.TiledLib;

[XmlRoot(ElementName="tileset")]
public partial class TileSet { 

	[XmlAttribute(AttributeName="firstgid")] 
	public int Firstgid { get; set; } 

	[XmlAttribute(AttributeName="source")] 
	public string Source { get; set; } 
}

[XmlRoot(ElementName="data")]
public class Data
{
	[XmlAttribute(AttributeName = "encoding")]
	public string Encoding { get; set; } = "csv";

	[XmlIgnore] // We don't want to serialize the array directly
	public int[,] MapData;
	
	[XmlText] 
	public string Text
	{
		get => CSVHelpers.ConvertArrayToCSV(MapData);
		set => MapData = ParseCsvString(value);
	}
	
	private static int[,] ParseCsvString(string csv)
	{
		var lines = csv.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		var numRows = lines.Length;
		var numCols = lines[0].Split(',').Length;

		var result = new int[numRows, numCols];

		for (int row = 0; row < numRows; row++)
		{
			var cols = lines[row].Split(',');
			for (int col = 0; col < numCols; col++)
			{
				result[row, col] = int.Parse(cols[col]);
			}
		}

		return result;
	}
}


[XmlRoot(ElementName="layer")]
public class Layer
{
	[XmlElement(ElementName = "data")] public Data Data { get; set; } = new();

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlAttribute(AttributeName="name")] 
	public string Name { get; set; } 

	[XmlAttribute(AttributeName="width")] 
	public int Width { get; set; } 

	[XmlAttribute(AttributeName="height")] 
	public int Height { get; set; }

	[XmlAttribute(AttributeName = "opacity")]
	public double Opacity { get; set; } = 1;

	public Layer() { }
	
	public Layer(int width, int height)
	{
		Width = width;
		Height = height;
	}
}

[XmlRoot(ElementName="map")]
public class TiledMap
{

	[XmlElement(ElementName = "tileset")] public List<TileSet> Tilesets { get; set; } = new();

	[XmlElement(ElementName = "layer")] public List<Layer> Layers { get; set; } = new();

	[XmlAttribute(AttributeName = "version")]
	public string Version { get; set; } = "1.10";

	[XmlAttribute(AttributeName = "tiledversion")]
	public string Tiledversion { get; set; } = "1.10.2";

	[XmlAttribute(AttributeName = "orientation")]
	public string Orientation { get; set; } = "orthogonal";

	[XmlAttribute(AttributeName = "renderorder")]
	public string RenderOrder { get; set; } = "right-down";

	[XmlAttribute(AttributeName="width")] 
	public int Width { get; set; } 

	[XmlAttribute(AttributeName="height")] 
	public int Height { get; set; } 

	[XmlAttribute(AttributeName="tilewidth")] 
	public int Tilewidth { get; set; } 

	[XmlAttribute(AttributeName="tileheight")] 
	public int Tileheight { get; set; } 

	[XmlAttribute(AttributeName="infinite")] 
	public int Infinite { get; set; } 

	[XmlAttribute(AttributeName="nextlayerid")] 
	public int Nextlayerid { get; set; } 

	[XmlAttribute(AttributeName="nextobjectid")] 
	public int Nextobjectid { get; set; }

	public TiledMap() { }

	public TiledMap(int width, int height)
	{
		Width = width;
		Height = height;
	}
}


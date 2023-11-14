using System.Globalization;
using System.Xml.Serialization;
using WorldGen.Utils;
using WorldGen.WangTiling;

namespace WorldGen.TiledLib;

[XmlRoot(ElementName="image")]
public class Image { 

	[XmlAttribute(AttributeName="source")] 
	public string Source { get; set; } 

	[XmlAttribute(AttributeName="width")] 
	public int Width { get; set; } 

	[XmlAttribute(AttributeName="height")] 
	public int Height { get; set; } 
}

[XmlRoot(ElementName="wangcolor")]
public class Wangcolor { 

	[XmlAttribute(AttributeName="name")] 
	public string Name { get; set; }

	[XmlAttribute(AttributeName = "color")]
	public string Color { get; set; }


	[XmlAttribute(AttributeName="tile")] 
	public int Tile { get; set; } 

	[XmlAttribute(AttributeName="probability")] 
	public int Probability { get; set; }
}

[XmlRoot(ElementName="wangtile")]
public class Wangtile { 

	[XmlAttribute(AttributeName="tileid")] 
	public int Tileid { get; set; } 

	/// <summary>
	/// This is a proxy property for the wangid attribute.
	/// Because Tiled lists the colors starting from top-middle,
	/// and we want to list them starting from top-left,
	/// as that is how they are in the maps.
	/// </summary>
	[XmlAttribute(AttributeName="wangid")] 
	public string Wangid {
		get => ToTiled(WangColors);
		set => WangColors = DecodeTiled(value);
	}
	
	[XmlIgnore]
	public WangColors WangColors;
	
	static WangColors DecodeTiled(string str)
	{
		var nums = str.Split(',', StringSplitOptions.RemoveEmptyEntries)
			.Select(i => int.Parse(i))
			.ToArray();

		return new WangColors(ArrayHelpers.RotateLeft(nums, 1));
	}
	
	static string ToTiled(WangColors str)
	{
		return ArrayHelpers.RotateRight(str.colors, 1)
			.Select(i => i.ToString()).Let(l => string.Join(',',l));
	}
}

[XmlRoot(ElementName="wangset")]
public class Wangset { 

	[XmlElement(ElementName="wangcolor")] 
	public List<Wangcolor> Wangcolor { get; set; } 

	[XmlElement(ElementName="wangtile")] 
	public List<Wangtile> Wangtile { get; set; } 

	[XmlAttribute(AttributeName="name")] 
	public string Name { get; set; } 

	[XmlAttribute(AttributeName="type")] 
	public string Type { get; set; } 

	[XmlAttribute(AttributeName="tile")] 
	public int Tile { get; set; } 
}

[XmlRoot(ElementName="wangsets")]
public class Wangsets { 

	[XmlElement(ElementName="wangset")] 
	public Wangset Wangset { get; set; } 
}

public partial class TileSet { 

	[XmlElement(ElementName="image")] 
	public Image Image { get; set; } 

	[XmlElement(ElementName="wangsets")] 
	public Wangsets Wangsets { get; set; } 

	[XmlAttribute(AttributeName="version")] 
	public string Version { get; set; } 

	[XmlAttribute(AttributeName="tiledversion")] 
	public string Tiledversion { get; set; } 

	[XmlAttribute(AttributeName="name")] 
	public string Name { get; set; } 

	[XmlAttribute(AttributeName="tilewidth")] 
	public int Tilewidth { get; set; } 

	[XmlAttribute(AttributeName="tileheight")] 
	public int Tileheight { get; set; } 

	[XmlAttribute(AttributeName="tilecount")] 
	public int Tilecount { get; set; } 

	[XmlAttribute(AttributeName="columns")] 
	public int Columns { get; set; } 
}


using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;
using WorldGen.TiledLib;
using WorldGen.WangTiling;

namespace WorldGen.Nodes.Wang;

public class WangTileNode : Node
{
    [Input, PrimaryPin] public I2DDataReadOnly<int> tileMap;
    [Input] public TileSet TileSet;

    [Output] public I2DData<int> output;

    Dictionary<int, Wangtile> helperMap = new();
    
    public override void Process()
    {
        var iteration = tileMap.MutableCopy();

        helperMap = new (
            TileSet.Wangsets.Wangset.Wangtile.Select(i => new KeyValuePair<int, Wangtile>(i.Tileid, i))
        );
        
        while (!iteration.Equals(output))
        {
            output = iteration.MutableCopy();
            RunWangReplacement(iteration);
        }
    }

    private void RunWangReplacement(I2DData<int> data)
    {
        for (int x = 0; x < tileMap.Width; x++)
        {
            for (int y = 0; y < tileMap.Height; y++)
            {
                var color = GetTileData(data, x, y);

                var options = TileSet.Wangsets.Wangset.Wangtile.Select(t => (num: t.WangColors.Matches(color), val: t));

                var newTile = options.MaxBy(t => t.num).val;
                if (newTile is not null)
                    data[x, y] = newTile.Tileid + 1;
                if (newTile is null)
                    Console.WriteLine("No tile found for color: " + color);
            }
        }
    }

    public WangColors GetTileData(I2DDataReadOnly<int> map, int x, int y)
    {
        int[] neighbours = new int[] {0,0,0,0,0,0,0,0};
        
        int[] positions = new [] {0,1,2,3,4,5,6,7};
        //int[] positions = new [] {0, 2, 5, 7};
        
        WangColors? selfColor = TileSet.Wangsets.Wangset.Wangtile.Find(t => t.Tileid == map[x,y]-1)?.WangColors;
        
        foreach (var i in positions)
        {
            var a = WangColors.GetDirection(i);
            var x1 = x + a.x;
            var y1 = y + a.y;

            int neighbour;
            if (!map.IsInside(x1,y1))
            {
                var x_clamp = Math.Clamp(x1, 0, map.Width-1);
                var y_clamp = Math.Clamp(y1, 0, map.Height-1);
                neighbour = map[x_clamp, y_clamp]-1;
                //neighbours[i] = selfColor?.GetColor(i) ?? 0;
            }
            else
            {
                neighbour = map[x + a.x, y + a.y]-1;   
            }
             
            //var neighbourColor = helperMap;
            if(!helperMap.ContainsKey(neighbour))
                neighbours[i] = 0;
            else
                neighbours[i] = helperMap[neighbour].WangColors.GetOppositeColor(i);
        }

        return new WangColors(neighbours);
    }
}
using System.Numerics;
using WorldGen.DataStructures;

namespace WorldGen.WangTiling;

public struct WangColors
{
    public int[] colors;
    
    public WangColors(int[] colors)
    {
        this.colors = colors;
    }
    
    public int GetColor(int index)
    {
        return colors[index];
    }
    
    public int GetOppositeColor(int index)
    {
        switch (index)
        {
            case 0:
                return colors[7];
            case 1:
                return colors[6];
            case 2:
                return colors[5];
            case 3:
                return colors[4];
            case 4:
                return colors[3];
            case 5:
                return colors[2];
            case 6:
                return colors[1];
            case 7:
                return colors[0];
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int Matches(object? obj)
    {
        if (obj is WangColors other)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if (colors[i] == other.colors[i] && colors[i] != 0 && other.colors[i] != 0)
                    count++;
            }

            return count;
        }

        return 0;
    }

    public override string ToString()
    {
        return string.Join(',', colors);
    }

    private static Vector2I[] dirs =
    {
        new (-1, -1),new ( 0, -1),new ( 1, -1),
        new (-1,  0),                 new ( 1,  0),
        new (-1,  1),new ( 0,  1),new ( 1,  1),
    };
    
    public static Vector2I GetDirection(int i)
    {
        return dirs[i];
    }
}
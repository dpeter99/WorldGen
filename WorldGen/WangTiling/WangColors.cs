namespace WorldGen.WangTiling;

public class WangColors
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
        return colors[index + 4 % 8];
    }
}
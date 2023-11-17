namespace WorldGen.Utils;

public class MathFExtensions
{
    public static int FloorToInt(float v)
    {
        return (int) Math.Floor(v);
    }

    public static int Min(int lhsX, int rhsX)
    {
        return lhsX < rhsX ? lhsX : rhsX;
    }

    public static int Max(int lhsX, int rhsX)
    {
        return lhsX > rhsX ? lhsX : rhsX;
    }

    public static int CeilToInt(float p0)
    {
        return (int) Math.Ceiling(p0);
    }

    public static int RoundToInt(float vX)
    {
        return (int) Math.Round(vX);
    }
}
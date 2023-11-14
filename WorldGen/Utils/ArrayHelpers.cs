namespace WorldGen.Utils;

public class ArrayHelpers
{
    public static T[] RotateRight<T>(T[] t, int amount)
    {
        return t[amount..].Concat(t[0..amount]).ToArray();
    }
    
    public static T[] RotateLeft<T>(T[] t, int amount)
    {
        return t[^amount..^0].Concat(t[..^amount]).ToArray();
    }
}
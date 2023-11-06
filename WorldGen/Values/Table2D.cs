namespace WorldGen.Values;

public class Table2D<T> : I2DDataStructure<T>
{

    T[,] data;

    public T this[int x, int y]
    {
        get => data[x,y];
        set => data[x, y] = value;
    } 
}
namespace WorldGen.DataStructures;


public interface ICopyable<Self>
{
    public Self Copy();
}

public interface I2DDataReadOnly<T>
{
    public T this[int x, int y] { get; }
    
    public int Width { get; }
    public int Height { get; }
}

public interface I2DData<T> : I2DDataReadOnly<T>
{
    public T this[int x, int y] { get; set; }
}

public interface I1DDataReadOnly<T>
{
    public T this[int x] { get; }

    public int Length { get; }
}

public interface I1DData<T> : I1DDataReadOnly<T>
{
    public T this[int x] { get; set; }
}

public class Array2D<T> : I2DData<T>, I1DData<T>, ICopyable<Array2D<T>>
{
    private T[,] data;
    public int Width => data.GetLength(0);
    public int Height => data.GetLength(1);

    public int Length => Width * Height;

    public Array2D(int width, int height)
    {
        data = new T[width, height];
    }

    public Array2D(Array2D<T> other)
    {
        data = (T[,])other.data.Clone();
    }

    public T this[int x, int y]
    {
        get => data[x, y];
        set => data[x, y] = value;
    }

    public Array2D<T> Copy()
    {
        return new Array2D<T>(this);
    }

    public T this[int index]
    {
        get
        {
            int x = index / Width;
            int y = index % Width;
            return data[x, y];
        }
        set
        {
            int x = index / Width;
            int y = index % Width;
            data[x, y] = value;
        }
    }
}

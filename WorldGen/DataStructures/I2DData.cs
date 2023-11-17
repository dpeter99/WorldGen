using System.Collections;

namespace WorldGen.DataStructures;


public interface ICopyable<Self> where Self : ICopyable<Self>
{
    public Self Copy();
}

public interface I2DDataReadOnly<T> : ICopyable<I2DDataReadOnly<T>>, I1DDataReadOnly<T>, IEquatable<I2DDataReadOnly<T>>
{
    public T this[int x, int y] { get; }
    
    public int Width { get; }
    public int Height { get; }

    public new I2DData<T> MutableCopy();
    
    public new I2DData<F> MutableOfType<F>();

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < Width &&
               y >= 0 && y < Height;
    }
}

public interface I2DData<T> : I2DDataReadOnly<T>, I1DData<T>
{
    public new T this[int x, int y] { get; set; }
    
    public T[,] AsArray();
}

public interface I1DDataReadOnly<T> : ICopyable<I1DDataReadOnly<T>>
{
    public T this[int x] { get; }

    public int Length { get; }
    
    public I1DData<T> MutableCopy();

    public I1DData<F> MutableOfType<F>();
}

public interface I1DData<T> : I1DDataReadOnly<T>
{
    public T this[int x] { get; set; }
}

public class Array2D<T> : I2DData<T>, I1DData<T>
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

    public T[,] AsArray()
    {
        return data;
    }

    public Array2D<T> Copy()
    {
        return new Array2D<T>(this);
    }

    #region I2DData
    
    I2DData<T> I2DDataReadOnly<T>.MutableCopy()
    {
        return new Array2D<T>(this);
    }

    I2DDataReadOnly<T> ICopyable<I2DDataReadOnly<T>>.Copy()
    {
        return Copy();
    }
    
    I2DData<F> I2DDataReadOnly<T>.MutableOfType<F>()
    {
        return new Array2D<F>(Width, Height);
    }
    
    #endregion

    #region I1Data
    I1DData<T> I1DDataReadOnly<T>.MutableCopy()
    {
        return new Array2D<T>(this);
    }

    I1DDataReadOnly<T> ICopyable<I1DDataReadOnly<T>>.Copy()
    {
        return Copy();
    }

    I1DData<F> I1DDataReadOnly<T>.MutableOfType<F>()
    {
        return new Array2D<F>(Width, Height);
    }
    
    #endregion
    
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

    public bool Equals(I2DDataReadOnly<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        if (Height != other.Height || Width != other.Width)
            return false;
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (!this[x, y].Equals(other[x, y]))
                    return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Array2D<T>)obj);
    }

    public override int GetHashCode()
    {
        return data.GetHashCode();
    }

    public static bool operator ==(Array2D<T>? left, Array2D<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Array2D<T>? left, Array2D<T>? right)
    {
        return !Equals(left, right);
    }
}

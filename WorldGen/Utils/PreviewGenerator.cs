using System.Numerics;
using SkiaSharp;

namespace WorldGen.Values;

static class PreviewGenerator
{
    static SKBitmap makeBitmap(I2DDataStructureReadOnly<double> field)
    {
        int width = Math.Min(field.Width, 200);
        int height = Math.Min(field.Height, 200);
        
        var img = new SKBitmap(width,height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector2(x, y);
                byte val = (byte)( field[pos] * 255);
                img.SetPixel(x,y, new SKColor(val,val,val));
            }
        }

        return img;
    }
    
    static SKBitmap makeBitmap(I2DDataStructureReadOnly<int> field)
    {
        int width = Math.Min(field.Width, 200);
        int height = Math.Min(field.Height, 200);
        
        var img = new SKBitmap(width,height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector2(x, y);
                byte val = (byte)( field[pos] * 255);
                img.SetPixel(x,y, new SKColor(val,val,val));
            }
        }

        return img;
    }

    public static void SaveSampleTo(I2DDataStructureReadOnly<double> field, FileInfo target)
    {
        SKBitmap img = makeBitmap(field);
        
        using (var data = img.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(target.FullName))
        {
            // save the data to a stream
            data.SaveTo(stream);
        }
    }
    
    public static void SaveSampleTo(I2DDataStructureReadOnly<int> field, FileInfo target)
    {
        SKBitmap img = makeBitmap(field);
        
        using (var data = img.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(target.FullName))
        {
            // save the data to a stream
            data.SaveTo(stream);
        }
    }
    
}
using SkiaSharp;
using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes.Texture;

public class ExportTextureNode : Node
{
    [Input] public I2DDataReadOnly<int> data;
    
    [Property] public string OutputFile = "test.png";
    [Property] public bool Remap;
    
    public override void Process()
    {
        SkiaSharp.SKBitmap canvas = new SKBitmap(data.Width, data.Height);

        int min = 0;
        int max = 0;
        float factor = 0;
        if (Remap)
        {
             min = data.Min();
             max = data.Max();

             factor = (max - min) / (100f);
        }
        
        for (int x = 0; x < data.Width; x++)
        {
            for (int y = 0; y < data.Height; y++)
            {
                if(Remap)
                    canvas.SetPixel(x, y, SKColor.FromHsl(0,0, (float) ((data[x, y] - min) / factor)));
                else
                    canvas.SetPixel(x, y, SKColor.FromHsl(0,0,data[x, y]));
            }
        }
        
        using (var data = canvas.Encode(SKEncodedImageFormat.Png, 80))
        using (var stream = File.OpenWrite(OutputFile))
        {
            // save the data to a stream
            data.SaveTo(stream);
        }
    }
}
using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes.CSV;

using System;
using System.IO;
using System.Text;

public class ExportCsvNode : Node
{
    [Input] public I2DData<float>? InputData;

    public FileInfo FilePath { get; private set; }

    public ExportCsvNode(FileInfo filePath)
    {
        FilePath = filePath;
    }

    public override void Process()
    {
        if (InputData == null)
        {
            throw new InvalidOperationException("Input data is not set.");
        }

        StringBuilder csvContent = new StringBuilder();

        for (int i = 0; i < InputData.Height; i++)
        {
            for (int j = 0; j < InputData.Width; j++)
            {
                csvContent.Append(InputData[i, j].ToString());
                if (j < InputData.Width - 1)
                    csvContent.Append(",");
            }
            csvContent.AppendLine();
        }
        
        File.WriteAllText(FilePath.FullName, csvContent.ToString());
    }
}

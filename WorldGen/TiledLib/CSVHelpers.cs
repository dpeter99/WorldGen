using System.Text;

namespace WorldGen.TiledLib;

public class CSVHelpers
{
    public static string ConvertArrayToCSV(int[,] array)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        var sb = new StringBuilder();
        sb.AppendLine();
        int numRows = array.GetLength(0);
        int numColumns = array.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
                sb.Append(array[i, j]);

                // Add a comma unless it's the last element in the last row
                if (!(i == numRows - 1 && j == numColumns - 1))
                {
                    sb.Append(",");
                }
            }

            // Add a newline after each row
            sb.AppendLine();
        }

        return sb.ToString();
    }

}
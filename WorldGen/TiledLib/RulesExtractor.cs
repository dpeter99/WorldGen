using System;
using System.Collections.Generic;
using System.Numerics;

public class RulesExtractor
{
    public class Rule
    {
        public int[,] Pattern { get; }
        public int[,] Output { get; }

        public Rule(int[,] pattern, int[,] output)
        {
            Pattern = pattern;
            Output = output;
        }
    }

    public List<Rule> ExtractRules(int[,] patternArray, int[,] replacementArray)
    {
        if (patternArray.GetLength(0) != replacementArray.GetLength(0) || patternArray.GetLength(1) != replacementArray.GetLength(1))
        {
            throw new ArgumentException("Pattern and replacement arrays must be of the same size.");
        }

        int rows = patternArray.GetLength(0);
        int cols = patternArray.GetLength(1);
        bool[,] visited = new bool[rows, cols];
        List<Rule> rules = new List<Rule>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!visited[i, j] && patternArray[i, j] != -1)
                {
                    List<Vector2> contiguousArea = FindContiguousArea(patternArray, visited, i, j);
                    int[,] subPattern = CreateSubArray(patternArray, contiguousArea);
                    int[,] subReplacement = CreateSubArray(replacementArray, contiguousArea);
                    rules.Add(new Rule(subPattern, subReplacement));
                }
            }
        }

        return rules;
    }

    private List<Vector2> FindContiguousArea(int[,] array, bool[,] visited, int startX, int startY)
    {
        List<Vector2> contiguousArea = new List<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();
        queue.Enqueue(new Vector2(startX, startY));
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            Vector2 cell = queue.Dequeue();
            contiguousArea.Add(cell);

            // Check adjacent cells (non-diagonal)
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };

            for (int dir = 0; dir < 4; dir++)
            {
                int newX = (int)cell.X + dx[dir];
                int newY = (int)cell.Y + dy[dir];

                if (IsInBounds(array, newX, newY) && !visited[newX, newY] && array[newX, newY] == array[(int)cell.X, (int)cell.Y])
                {
                    queue.Enqueue(new Vector2(newX, newY));
                    visited[newX, newY] = true;
                }
            }
        }

        return contiguousArea;
    }

    private bool IsInBounds(int[,] array, int x, int y)
    {
        return x >= 0 && y >= 0 && x < array.GetLength(0) && y < array.GetLength(1);
    }

    private int[,] CreateSubArray(int[,] array, List<Vector2> indices)
    {
        int minX = int.MaxValue, minY = int.MaxValue;
        int maxX = 0, maxY = 0;

        // Determine the bounds of the subarray
        foreach (var index in indices)
        {
            minX = Math.Min(minX, (int)index.X);
            minY = Math.Min(minY, (int)index.Y);
            maxX = Math.Max(maxX, (int)index.X);
            maxY = Math.Max(maxY, (int)index.Y);
        }

        int[,] subArray = new int[maxX - minX + 1, maxY - minY + 1];

        // Initialize the subarray with -1s for "any"
        for (int i = 0; i < subArray.GetLength(0); i++)
        {
            for (int j = 0; j < subArray.GetLength(1); j++)
            {
                subArray[i, j] = -1;
            }
        }

        // Copy the relevant parts of the original array
        foreach (var index in indices)
        {
            subArray[(int)index.X - minX, (int)index.Y - minY] = array[(int)index.X, (int)index.Y];
        }

        return subArray;
    }
}

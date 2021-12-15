using System;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Y2021.D11
{
    internal class Parser : Parser<int[,]>
    {
        internal override int[,] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            SetupProgressTracker(lines.Length);
            int[,] result = new int[lines[0].Length, lines.Length];
            for (int y = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    result[x, y] = int.Parse(lines[y][x].ToString());
                }
                s_progressTracker.CurrentStep++;
            }
            return result;
        }
    }
}

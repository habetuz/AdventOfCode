namespace AdventOfCode.Solutions.Y2022.D08
{
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Parser : Parser<byte[,]>
    {
        internal override byte[,] Parse(string input)
        {
            var lines = input.Split('\n');

            var output = new byte[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    output[x, y] = (byte)(lines[y][x] - '0');
                }
            }

            return output;
        }
    }
}

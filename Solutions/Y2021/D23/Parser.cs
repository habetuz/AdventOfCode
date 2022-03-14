using System;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Y2021.D23
{
    internal class Parser : Parser<char[,]>
    {
        internal override char[,] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            char[,] result = new char[lines[0].Length ,lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    try
                    {
                        result[x, y] = lines[y][x];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        result[x, y] = ' ';
                    }
                }
            }

            Tools.Print2D(result);

            return result;
        }
    }
}

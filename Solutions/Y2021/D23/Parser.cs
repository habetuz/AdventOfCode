namespace AdventOfCode.Solutions.Y2021.D23
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<(char[,], char[,])>
    {
        internal override (char[,], char[,]) Parse(string input)
        {
            List<string> lines = new List<string>(input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries));

            char[,] resultA = new char[lines[0].Length, lines.Count];
            char[,] resultB = new char[lines[0].Length, lines.Count + 2];

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    try
                    {
                        resultA[x, y] = lines[y][x];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        resultA[x, y] = ' ';
                    }
                }
            }

            lines.Insert(3, "  #D#B#A#C#");
            lines.Insert(3, "  #D#C#B#A#");

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    try
                    {
                        resultB[x, y] = lines[y][x];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        resultB[x, y] = ' ';
                    }
                }
            }

            return (resultA, resultB);
        }
    }
}

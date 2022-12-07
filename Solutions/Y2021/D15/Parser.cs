﻿namespace AdventOfCode.Solutions.Y2021.D15
{
    using System;
    using AdventOfCode.Common;

    internal class Parser : Parser<Node[,]>
    {
        internal override Node[,] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Node[,] result = new Node[lines[0].Length, lines.Length];

            for (int y = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    result[x, y] = new Node(int.Parse(lines[y][x].ToString()), x, y);
                }
            }

            return result;
        }
    }
}

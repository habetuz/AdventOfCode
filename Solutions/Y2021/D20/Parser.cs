namespace AdventOfCode.Solutions.Y2021.D20
{
    using System;
    using AdventOfCode.Common;

    internal class Parser : Parser<(string, char[,])>
    {
        internal override (string, char[,]) Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string filter = lines[0];

            char[,] image = new char[lines[1].Length + 6, lines.Length - 1 + 6];

            for (int y = 0; y < image.GetLength(1); y++)
            {
                for (int x = 0; x < image.GetLength(0); x++)
                {
                    image[x, y] = '.';
                }
            }

            for (int y = 3; y < image.GetLength(1) - 3; y++)
            {
                for (int x = 3; x < image.GetLength(0) - 3; x++)
                {
                    image[x, y] = lines[y + 1 - 3][x - 3];
                }
            }

            return (filter, image);
        }
    }
}

namespace AdventOfCode.Solutions.Y2021.D20
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Solution : Solution<(string, char[,])>
    {
        internal override string Puzzle1((string, char[,]) input)
        {
            // Tools.Print2D(input.Item2);
            char[,] image = this.ApplyFilter(input.Item1, input.Item2);

            // Tools.Print2D(image);
            image = this.ApplyFilter(input.Item1, image);

            // Tools.Print2D(image);
            int lightCounter = 0;
            foreach (char pixel in image)
            {
                if (pixel == '#')
                {
                    lightCounter++;
                }
            }

            SharpLog.Logging.LogDebug($"There are {lightCounter} light pixels!");

            return lightCounter.ToString();
        }

        internal override string Puzzle2((string, char[,]) input)
        {
            string filter = input.Item1;
            char[,] image = input.Item2;

            for (int i = 1; i <= 50; i++)
            {
                image = this.ApplyFilter(filter, image);
            }

            int lightCounter = 0;
            foreach (char pixel in image)
            {
                if (pixel == '#')
                {
                    lightCounter++;
                }
            }

            SharpLog.Logging.LogDebug($"There are {lightCounter} light pixels!");

            return lightCounter.ToString();
        }

        private char[,] ApplyFilter(string filter, char[,] image)
        {
            char[,] output = new char[image.GetLength(0) + 2, image.GetLength(1) + 2];

            char infinityFill = this.ApplyFilter(filter, image, 1, 1);

            for (int x = 0; x < output.GetLength(0); x++)
            {
                for (int y = 0; y < output.GetLength(1); y++)
                {
                    output[x, y] = infinityFill;
                }
            }

            for (int x = 2; x < image.GetLength(0) - 2; x++)
            {
                for (int y = 2; y < image.GetLength(1) - 2; y++)
                {
                    output[x + 1, y + 1] = this.ApplyFilter(filter, image, x, y);
                }
            }

            return output;
        }

        private char ApplyFilter(string filter, char[,] image, int atX, int atY)
        {
            string code = string.Empty;

            for (int y = atY - 1; y <= atY + 1; y++)
            {
                for (int x = atX - 1; x <= atX + 1; x++)
                {
                    code += image[x, y] == '#' ? 1 : 0;
                }
            }

            return filter[Convert.ToInt32(code, 2)];
        }
    }
}

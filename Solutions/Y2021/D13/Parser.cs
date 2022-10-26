namespace AdventOfCode.Solutions.Y2021.D13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<((char, int)[], bool[,])>
    {
        internal override ((char, int)[], bool[,]) Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<(char, int)> instructions = new List<(char, int)>();
            List<(int, int)> coordinates = new List<(int, int)>();
            (int, int) maxIndex = (0, 0);

            foreach (string line in lines)
            {
                if (line[0] == 'f')
                {
                    instructions.Add((line[11], int.Parse(line.Substring(13))));
                }
                else
                {
                    coordinates.Add((int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1])));
                    if (coordinates.Last().Item1 > maxIndex.Item1)
                    {
                        maxIndex.Item1 = coordinates.Last().Item1;
                    }

                    if (coordinates.Last().Item2 > maxIndex.Item2)
                    {
                        maxIndex.Item2 = coordinates.Last().Item2;
                    }
                }
            }

            bool[,] paper = new bool[maxIndex.Item1 + 1, maxIndex.Item2 + 1];

            foreach ((int, int) coordinate in coordinates)
            {
                paper[coordinate.Item1, coordinate.Item2] = true;
            }

            return (instructions.ToArray(), paper);
        }
    }
}

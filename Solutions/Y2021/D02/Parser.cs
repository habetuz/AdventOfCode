namespace AdventOfCode.Solutions.Y2021.D02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;
    using SharpLog;
    using static AdventOfCode.Solutions.Y2021.D02.Parser;

    internal class Parser : Parser<KeyValuePair<Direction, int>[]>
    {
        internal enum Direction
        {
            Forward,
            Up,
            Down,
        }

        internal override KeyValuePair<Direction, int>[] Parse(string input)
        {
            // Split file into lines
            string[] lines = input.Split('\n');

            // Parsing to KeyValuePair
            List<KeyValuePair<Direction, int>> inputArray = new List<KeyValuePair<Direction, int>>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] inputPair = lines[i].Split(' ');
                if (inputPair.Length == 2 && int.TryParse(inputPair[1], out int number))
                {
                    inputArray.Add(new KeyValuePair<Direction, int>(
                        (Direction)Enum.Parse(typeof(Direction), inputPair[0], ignoreCase: true),
                        number));
                }
            }

            return inputArray.ToArray();
        }
    }
}

namespace AdventOfCode.Solutions.Y2022.D10
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<(string, int)[]>
    {
        internal override (string, int)[] Parse(string input)
        {
            var lines = input.Split('\n');

            var output = new (string, int)[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ');

                output[i] = (line[0], line.Length > 1 ? int.Parse(line[1]) : 0);
            }

            return output;
        }
    }
}

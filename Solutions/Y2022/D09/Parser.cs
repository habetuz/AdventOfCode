namespace AdventOfCode.Solutions.Y2022.D09
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<(char, int)[]>
    {
        internal override (char, int)[] Parse(string input)
        {
            var lines = input.Split('\n');

            var output = new (char, int)[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var move = lines[i].Split(' ');
                output[i] = (move[0][0], int.Parse(move[1]));
            }

            return output;
        }
    }
}

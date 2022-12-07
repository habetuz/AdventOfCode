namespace AdventOfCode.Solutions.Y2021.D01
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<int[]>
    {
        internal override int[] Parse(string input)
        {
            string[] lines = input.Split('\n');

            List<int> inputArray = new List<int>();

            // Parsing
            for (int i = 0; i < lines.Length; i++)
            {
                int number;
                int.TryParse(lines[i], out number);
                inputArray.Add(number);
            }

            return inputArray.ToArray();
        }
    }
}

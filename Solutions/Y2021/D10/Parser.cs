namespace AdventOfCode.Solutions.Y2021.D07
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<int[]>
    {
        internal override int[] Parse(string input)
        {
            string[] values = input.Split(',');

            List<int> startValues = new List<int>();

            foreach (string value in values)
            {
                startValues.Add(int.Parse(value));
            }

            return startValues.ToArray();
        }
    }
}

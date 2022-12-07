namespace AdventOfCode.Solutions.Y2021.D06
{
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using SharpLog;

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

            Logging.LogDebug($"There are {startValues.Count} fishes!");

            return startValues.ToArray();
        }
    }
}

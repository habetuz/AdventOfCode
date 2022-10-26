namespace AdventOfCode.Solutions.Y2021.D06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

            SharpLog.Logging.LogDebug($"There are {startValues.Count} fishes!");

            return startValues.ToArray();
        }
    }
}

namespace AdventOfCode.Solutions.Y2021.D19
{
    using AdventOfCode.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Parser : Parser<Scanner[]>
    {
        internal override Scanner[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<Scanner> scanners = new List<Scanner>();

            foreach (string line in lines)
            {
                if (line.Contains("scanner"))
                {
                    scanners.Add(new Scanner());
                    continue;
                }

                string[] coordinates = line.Split(',');

                var beacons = scanners.Last().Beacons;

                Array.Resize(ref beacons, scanners.Last().Beacons.Length + 1);

                scanners.Last().Beacons = beacons;

                scanners.Last().Beacons[scanners.Last().Beacons.Length - 1] = new Beacon()
                {
                    X = int.Parse(coordinates[0]),
                    Y = int.Parse(coordinates[1]),
                    Z = int.Parse(coordinates[2]),
                };
            }

            return scanners.ToArray();
        }
    }
}

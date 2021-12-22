using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D19
{
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
                Array.Resize(ref scanners.Last().Beacons, scanners.Last().Beacons.Length + 1);
                scanners.Last().Beacons[scanners.Last().Beacons.Length - 1] = new Beacon()
                {
                    X = int.Parse(coordinates[0]),
                    Y = int.Parse(coordinates[1]),
                    Z = int.Parse(coordinates[2])
                };
            }

            return scanners.ToArray();
        }

    }
}

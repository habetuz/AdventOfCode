namespace AdventOfCode.Solutions.Y2021.D03
{
    using AdventOfCode.Common;
    using SharpLog;
    using System.Collections.Generic;

    internal class Parser : Parser<int[][]>
    {
        internal override int[][] Parse(string input)
        {
            // Split file into lines
            string[] lines = input.Split('\n');

            // Parsing to integer
            List<int[]> inputArray = new List<int[]>();
            for (int i = 0; i < lines.Length - 1; i++)
            {
                int[] bits = new int[lines[0].Length];

                for (int bit = 0; bit < lines[0].Length; bit++)
                {
                    bits[bit] = lines[i][bit] - '0';
                    Logging.LogDebug(string.Format("{2}:{3} | From {0} to {1}", lines[i][bit], bits[bit], i, bit));
                }

                inputArray.Add(bits);
            }

            return inputArray.ToArray();
        }
    }
}

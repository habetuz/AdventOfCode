namespace AdventOfCode.Solutions.Y2021.D03
{
    using AdventOfCode.Common;
    using SharpLog;
    using System;
    using System.Collections.Generic;

    internal class Solution : Solution<int[][]>
    {
        internal override (object, string) Puzzle1(int[][] input)
        {
            ////Tools.Print2D(input);

            string gammaRate = string.Empty;
            string eplsilonRate = string.Empty;
            for (int x = 0; x < input[0].Length; x++)
            {
                int counter = 0;
                int counter0 = 0;
                for (int y = 0; y < input.Length; y++)
                {
                    counter += input[y][x];
                    counter0 += (input[y][x] - 1) * -1;
                }

                if (counter > input.Length / 2)
                {
                    gammaRate += "1";
                }
                else
                {
                    gammaRate += "0";
                }
            }

            int gammaRateDec = Convert.ToInt32(gammaRate, 2);
            int eplsilonRateDec = (int)Math.Pow(2, input[0].Length) - 1 - gammaRateDec;

            Logging.LogDebug(string.Format("Expected: {0} | Reality: {1}", (int)Math.Pow(2, input[0].Length) - 1, gammaRateDec + eplsilonRateDec));

            return ($"{gammaRateDec * eplsilonRateDec}", string.Format("Gamma rate is {0} and epsilon rate is {1}. Solution: {2}", gammaRateDec, eplsilonRateDec, gammaRateDec * eplsilonRateDec));
        }

        internal override (object, string) Puzzle2(int[][] input)
        {
            int[] oxygenRate = this.RecursiveFilter(input, filterForMostCommon: true, 0);
            int oxygenRateDec = Tools.BinaryIntArrayToDecInt(oxygenRate);

            int[] co2Rate = this.RecursiveFilter(input, filterForMostCommon: false, 0);
            int co2RateDec = Tools.BinaryIntArrayToDecInt(co2Rate);

            return ($"{oxygenRateDec * co2RateDec}", $"Oxygen rate is {oxygenRateDec} and CO2 rate is {co2RateDec}. Result is {oxygenRateDec * co2RateDec}");
        }

        private int[] RecursiveFilter(int[][] input, bool filterForMostCommon, int x)
        {
            if (input.Length == 1)
            {
                return input[0];
            }

            List<int[]> list1 = new List<int[]>();
            List<int[]> list0 = new List<int[]>();

            for (int y = 0; y < input.Length; y++)
            {
                if (input[y][x] == 0)
                {
                    list0.Add(input[y]);
                }
                else
                {
                    list1.Add(input[y]);
                }
            }

            if ((list1.Count >= list0.Count) == filterForMostCommon)
            {
                Logging.LogDebug($"{x} - {list1.Count}");
                if (list1.Count == 1)
                {
                    return list1[0];
                }

                return this.RecursiveFilter(list1.ToArray(), filterForMostCommon, x + 1);
            }
            else
            {
                Logging.LogDebug($"{x} - {list0.Count}");
                if (list0.Count == 1)
                {
                    return list0[0];
                }

                return this.RecursiveFilter(list0.ToArray(), filterForMostCommon, x + 1);
            }
        }
    }
}

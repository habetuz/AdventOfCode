namespace AdventOfCode.Solutions.Y2021.D03
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<int[][]>
    {
        internal override string Puzzle1(int[][] input)
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

            SharpLog.Logging.LogDebug(string.Format("Expected: {0} | Reality: {1}", (int)Math.Pow(2, input[0].Length) - 1, gammaRateDec + eplsilonRateDec));

            SharpLog.Logging.LogDebug(string.Format("Gamma rate is {0} and epsilon rate is {1}. Solution: {2}", gammaRateDec, eplsilonRateDec, gammaRateDec * eplsilonRateDec));

            return "" + (gammaRateDec * eplsilonRateDec);
        }

        internal override string Puzzle2(int[][] input)
        {
            int[] oxygenRate = this.recursiveFilter(input, filterForMostCommon: true, 0);
            int oxygenRateDec = Tools.BinaryIntArrayToDecInt(oxygenRate);

            int[] co2Rate = this.recursiveFilter(input, filterForMostCommon: false, 0);
            int co2RateDec = Tools.BinaryIntArrayToDecInt(co2Rate);

            SharpLog.Logging.LogDebug($"Oxygen rate is {oxygenRateDec} and CO2 rate is {co2RateDec}. Result is {oxygenRateDec * co2RateDec}");

            return string.Empty + (oxygenRateDec * co2RateDec);
        }

        private int[] recursiveFilter(int[][] input, bool filterForMostCommon, int x)
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
                SharpLog.Logging.LogDebug($"{x} - {list1.Count}");
                if (list1.Count == 1)
                {
                    return list1[0];
                }

                return this.recursiveFilter(list1.ToArray(), filterForMostCommon, x + 1);
            }
            else
            {
                SharpLog.Logging.LogDebug($"{x} - {list0.Count}");
                if (list0.Count == 1)
                {
                    return list0[0];
                }

                return this.recursiveFilter(list0.ToArray(), filterForMostCommon, x + 1);
            }
        }
    }
}

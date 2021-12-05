using AdventOfCode.Common;
using SharpLog;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Y2021.D03
{
    internal class Solution : Solution<int[][]>
    {
        internal override string Puzzle1(int[][] input)
        {
            s_progressTracker = new ProgressTracker(input.Length * input[0].Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

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
                    s_progressTracker.CurrentStep ++;
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

            s_logger.Log(string.Format("Expected: {0} | Reality: {1}", (int)Math.Pow(2, input[0].Length) - 1, gammaRateDec + eplsilonRateDec));

            s_logger.Log(string.Format("Gamma rate is {0} and epsilon rate is {1}. Solution: {2}", gammaRateDec, eplsilonRateDec, gammaRateDec * eplsilonRateDec), LogType.Info);

            return "" + (gammaRateDec * eplsilonRateDec);
        }

        internal override string Puzzle2(int[][] input)
        {
            s_progressTracker = new ProgressTracker( 2 * (input[0].Length -2), (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            int[] oxygenRate = recursiveFilter(input, filterForMostCommon: true, 0);
            int oxygenRateDec = Tools.BinaryIntArrayToDecInt(oxygenRate);

            int[] co2Rate = recursiveFilter(input, filterForMostCommon: false, 0);
            int co2RateDec = Tools.BinaryIntArrayToDecInt(co2Rate);

            s_progressTracker.CurrentStep = s_progressTracker.NeededSteps;
            s_logger.Log($"Oxygen rate is {oxygenRateDec} and CO2 rate is {co2RateDec}. Result is {oxygenRateDec * co2RateDec}", LogType.Info);

            return string.Empty + (oxygenRateDec * co2RateDec);
        }

        private int[] recursiveFilter(int[][] input, bool filterForMostCommon, int x)
        {
            s_progressTracker.CurrentStep++;

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
                s_logger.Log($"{x} - {list1.Count}");
                if (list1.Count == 1) return list1[0];
                return recursiveFilter(list1.ToArray(), filterForMostCommon, x + 1);
            }
            else
            {
                s_logger.Log($"{x} - {list0.Count}");
                if (list0.Count == 1) return list0[0];
                return recursiveFilter(list0.ToArray(), filterForMostCommon, x + 1);
            }
        }
    }
}

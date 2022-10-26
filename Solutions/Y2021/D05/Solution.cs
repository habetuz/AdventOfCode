namespace AdventOfCode.Solutions.Y2021.D05
{
    using System;
    using AdventOfCode.Common;
    using SharpLog;
    using static System.Windows.Forms.LinkLabel;

    internal class Solution : Solution<Tuple<Line[], Point>>
    {
        private int[,] field;

        internal override string Puzzle1(Tuple<Line[], Point> input)
        {
            this.field = new int[input.Item2.X, input.Item2.Y];

            foreach (Line line in input.Item1)
            {
                if (!line.IsVertical && !line.IsHorizontal)
                {
                    continue;
                }

                foreach (Point point in line.CoveredPoints)
                {
                    this.field[point.X, point.Y]++;
                }
            }

            /// Tools.Print2D(_field);

            int overlapCounter = 0;
            foreach (int i in this.field)
            {
                if (i >= 2)
                {
                    overlapCounter++;
                }
            }

            SharpLog.Logging.LogDebug($"There are {overlapCounter} overlapping points!");

            return overlapCounter.ToString();
        }

        internal override string Puzzle2(Tuple<Line[], Point> input)
        {
            this.field = new int[input.Item2.X, input.Item2.Y];

            foreach (Line line in input.Item1)
            {
                foreach (Point point in line.CoveredPoints)
                {
                    this.field[point.X, point.Y]++;
                }
            }

            /// Tools.Print2D(_field);

            int overlapCounter = 0;
            foreach (int i in this.field)
            {
                if (i >= 2)
                {
                    overlapCounter++;
                }
            }

            SharpLog.Logging.LogDebug($"There are {overlapCounter} overlapping points!");

            return overlapCounter.ToString();
        }
    }
}

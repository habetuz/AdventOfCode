namespace AdventOfCode.Solutions.Y2021.D05
{
    using System;
    using AdventOfCode.Common;

    internal class Solution : Solution<Tuple<Line[], Point>>
    {
        private int[,] field;

        internal override (object, string) Puzzle1(Tuple<Line[], Point> input)
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

            return (overlapCounter.ToString(), $"There are {overlapCounter} overlapping points!");
        }

        internal override (object, string) Puzzle2(Tuple<Line[], Point> input)
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

            return (overlapCounter.ToString(), $"There are {overlapCounter} overlapping points!");
        }
    }
}

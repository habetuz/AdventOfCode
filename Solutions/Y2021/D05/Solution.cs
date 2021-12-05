using AdventOfCode.Common;
using SharpLog;
using System;
using static System.Windows.Forms.LinkLabel;

namespace AdventOfCode.Solutions.Y2021.D05
{
    internal class Solution : Solution<Tuple<Line[], Point>>
    {
        private int[,] _field;

        internal override string Puzzle1(Tuple<Line[], Point> input)
{
            s_progressTracker = new ProgressTracker(input.Item1.Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            _field = new int[input.Item2.X, input.Item2.Y];

            foreach (Line line in input.Item1)
            {
                s_progressTracker.CurrentStep++;

                if (!line.IsVertical && !line.IsHorizontal) continue;
                foreach (Point point in line.CoveredPoints)
                {
                    _field[point.X, point.Y]++;
                }
            }

            /// Tools.Print2D(_field);

            int overlapCounter = 0;
            foreach (int i in _field)
            {
                if (i >= 2) overlapCounter++;
            }

            s_logger.Log($"There are {overlapCounter} overlapping points!", LogType.Info);

            return overlapCounter.ToString();
        }

        internal override string Puzzle2(Tuple<Line[], Point> input)
        {
            s_progressTracker = new ProgressTracker(input.Item1.Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            _field = new int[input.Item2.X, input.Item2.Y];

            foreach (Line line in input.Item1)
            {
                s_progressTracker.CurrentStep++;

                foreach (Point point in line.CoveredPoints)
                {
                    _field[point.X, point.Y]++;
                }
            }

            /// Tools.Print2D(_field);

            int overlapCounter = 0;
            foreach (int i in _field)
            {
                if (i >= 2) overlapCounter++;
            }

            s_logger.Log($"There are {overlapCounter} overlapping points!", LogType.Info);

            return overlapCounter.ToString();
        }
    }
}

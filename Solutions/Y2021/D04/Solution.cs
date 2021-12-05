using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D04
{
    internal class Solution : Solution<Tuple<int[], Board[]>>
    {
        internal event NewDrawHandler NewDrawEvent;

        internal delegate void NewDrawHandler(int draw);

        private int _solution = -1;

        internal override string Puzzle1(Tuple<int[], Board[]> input)
        {
            s_progressTracker = new ProgressTracker(input.Item1.Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            foreach (Board board in input.Item2)
            {
                board.AddToDrawEvent(this);
                board.CompletedEvent += BoardCompleted;
            }

            foreach (int draw in input.Item1)
            {
                if (_solution != -1) break;
                NewDrawEvent(draw);
                s_progressTracker.CurrentStep++;
            }

            s_progressTracker.CurrentStep = s_progressTracker.NeededSteps;
            s_logger.Log($"The solution is {_solution}!", LogType.Info);

            return _solution.ToString();
        }

        private void BoardCompleted(Board sender, int lastDraw)
        {
            _solution = lastDraw * sender.Value;
        }
    }
}

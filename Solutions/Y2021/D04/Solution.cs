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

        internal delegate void NewDrawHandler(Solution sender, int draw);

        private int _lastCompletion = -1;
        private int _drawIndex = 0;

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

            for (int i = 0; i < input.Item1.Length; i++)
            {
                int draw = input.Item1[i];
                if (_lastCompletion != -1) break;
                NewDrawEvent(this, draw);
                s_progressTracker.CurrentStep = i;
                _drawIndex = i;
            }

            s_progressTracker.CurrentStep = s_progressTracker.NeededSteps;
            s_logger.Log($"The solution is {_lastCompletion}!", LogType.Info);

            return _lastCompletion.ToString();
        }

        internal override string Puzzle2(Tuple<int[], Board[]> input)
        {
            s_progressTracker = new ProgressTracker(input.Item1.Length - _drawIndex, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            for (int i = _drawIndex +1; i < input.Item1.Length; i++)
            {
                int draw = input.Item1[i];
                if (NewDrawEvent == null) break;
                NewDrawEvent(this, draw);
                s_progressTracker.CurrentStep = i - _drawIndex - 1;
            }

            s_progressTracker.CurrentStep = s_progressTracker.NeededSteps;
            s_logger.Log($"The solution is {_lastCompletion}!", LogType.Info);

            return _lastCompletion.ToString();
        }

        private void BoardCompleted(Board sender, int lastDraw)
        {
            _lastCompletion = lastDraw * sender.Value;
        }
    }
}

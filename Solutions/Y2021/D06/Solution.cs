using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D06
{
    internal class Solution : Solution<List<Lanternfish>>
    {
        private int _LanternfishCounter;

        internal override string Puzzle1(List<Lanternfish> input)
        {
            s_progressTracker = new ProgressTracker(80, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            _LanternfishCounter = input.Count;

            foreach (Lanternfish lanternfish in input)
            {
                lanternfish.AddToNewDayEvent(this);
                lanternfish.Breed += BreedEvent;
            }

            for (int day = 1; day <= 80; day++)
            {
                NewDay();
                s_progressTracker.CurrentStep = day;
                ////s_logger.Log($"After {day:D2} days: {_LanternfishCounter}", LogType.Info);
            }

            s_logger.Log($"After 80 days there will be {_LanternfishCounter} laternfish!", LogType.Info);

            return _LanternfishCounter.ToString();
        }

        internal override string Puzzle2(List<Lanternfish> input)
        {
            s_progressTracker = new ProgressTracker(256 - 80, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            for (int day = 81; day <= 256; day++)
            {
                NewDay();
                s_progressTracker.CurrentStep = day - 80;
                s_logger.Log($"After {day:D2} days: {_LanternfishCounter}", LogType.Info);
            }

            s_logger.Log($"After 256 days there will be {_LanternfishCounter} laternfish!", LogType.Info);

            return _LanternfishCounter.ToString();
        }

        internal void BreedEvent()
        {
            _LanternfishCounter++;
            new Lanternfish(this, BreedEvent);
        }

        internal event NewDayHandler NewDay;

        internal delegate void NewDayHandler();
    }
}

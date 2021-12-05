using System;

namespace AdventOfCode.Common
{
    internal class ProgressTracker
    {
        private readonly int _neededSteps;

        private int _currentStep;
        private int _lastProgressChange = -1;
        private event ProgressChangeEventHandler _progressChanged;

        public int CurrentStep
        {
            get { return _currentStep; }
            set
            {
                _currentStep = value;

                if (((float)_currentStep / (float)_neededSteps) * 10 >= _lastProgressChange + 1 || ((float)_currentStep / (float)_neededSteps) * 10 <= _lastProgressChange - 1)
                {
                    _lastProgressChange = (int) ((float)_currentStep / (float)_neededSteps * 10);
                    _progressChanged(_lastProgressChange);
                }
            }
        }

        public int NeededSteps { get { return _neededSteps; } }

        internal ProgressTracker(int neededSteps, ProgressChangeEventHandler progressChange)
        {
            _neededSteps = neededSteps;
            _progressChanged = progressChange;
            this.CurrentStep = 0;
        }

        public delegate void ProgressChangeEventHandler(int progress);


        public static string ProgressToString(int progress)
        {
            string progressString = "[";

            for (int i = 0; i < 10; i++)
            {
                if (i < progress)
                {
                    progressString += "#";
                }
                else
                {
                    progressString += " ";
                }
            }

            progressString += "]";
            return progressString;
        }
    }
}

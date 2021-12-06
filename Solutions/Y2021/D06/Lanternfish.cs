using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D06
{
    internal class Lanternfish
    {
        private int _timeToBreed;

        internal event BreadHandler Breed;

        internal delegate void BreadHandler();

        internal Lanternfish(Solution solution, BreadHandler breadHandler)
        {
            _timeToBreed = 8;
            AddToNewDayEvent(solution);
            Breed += breadHandler;
        }

        internal Lanternfish(int timeToBreed)
        {
            _timeToBreed= timeToBreed;
        }

        internal void AddToNewDayEvent(Solution solution)
        {
            solution.NewDay += this.NewDayEvent;
        }

        private void NewDayEvent()
        {
            _timeToBreed--;
            if (_timeToBreed < 0)
            {
                _timeToBreed = 6;
                Breed();
            }
        }


    }
}

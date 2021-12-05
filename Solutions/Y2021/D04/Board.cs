using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D04
{
    internal class Board
    {
        internal event CompletedHandler CompletedEvent;

        internal delegate void CompletedHandler(Board sender, int lastDraw);

        internal int Value
        {
            get
            {
                int value = 0;
                for (int x = 0; x < _boardValues.GetLength(0); x++)
                {
                    for (int y = 0; y < _boardValues.GetLength(1); y++)
                    {
                        if (!_boardChecked[x, y])
                        {
                            value += _boardValues[x, y];
                        }
                    }
                }

                return value;
            }
        }

        private readonly int[,] _boardValues;
        private readonly bool[,] _boardChecked = new bool[5, 5];

        internal Board(int[,] boardValues)
        {
            _boardValues = boardValues;
        }

        internal void AddToDrawEvent(Solution solution)
        {
            solution.NewDrawEvent += Draw;
        }

        private void Draw(int draw)
        {
            for (int x = 0; x < _boardValues.GetLength(0); x++)
            {
                for (int y = 0; y < _boardValues.GetLength(1); y++)
                {
                    if (_boardValues[x, y] == draw)
                    {
                        _boardChecked[x, y] = true;
                        if (IsCompleted(x, y))
                        {
                            CompletedEvent(this, draw);
                        }
                    }
                }
            }
        }

        private bool IsCompleted(int x, int y)
        {
            bool xComplete = true;
            bool yComplete = true;

            for (int i = 0; i < _boardValues.GetLength(0); i++)
            {
                if (!_boardChecked[x, i]) yComplete = false;
                if (!_boardChecked[i, y]) xComplete = false;
            }

            return xComplete || yComplete;
        }
    }
}

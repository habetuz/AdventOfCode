using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D21
{
    internal class Player
    {
        internal int RepresentingDimentions { get; set; } = 1;

        internal int RoleCount { get; set; }

        internal int StartingPosition { get; set; }

        private int _position;
        internal int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = (value - 1) % 10 + 1;
                _score += Position;
                if (Won)
                {
                    WinEvent(StartingPosition, RepresentingDimentions);
                }
            }
        }

        private int _neededScore;

        private int _score;
        internal int Score
        {
            get
            {
                return _score;
            }
        }

        internal bool Won
        {
            get
            {
                return _score >= _neededScore;
            }
        }

        internal Player(int startingPosition, int neededScore)
        {
            _position = startingPosition;
            StartingPosition = startingPosition;
            _neededScore = neededScore;
        }

        internal event Win WinEvent;

        internal delegate void Win(int startingPosition, int dimentions = 1);

        internal Player Clone()
        {
            Player player = new Player(StartingPosition, _neededScore)
            {
                RoleCount = RoleCount,
                RepresentingDimentions = RepresentingDimentions,
            };

            player._position = this._position;
            player._score = this._score;
            player.WinEvent += WinEvent;

            return player;
        }
    }
}

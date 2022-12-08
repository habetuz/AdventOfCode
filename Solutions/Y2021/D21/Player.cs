// <copyright file="Player.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D21
{
    internal class Player
    {
        private readonly int neededScore;
        private int position;
        private int score;

        internal Player(int startingPosition, int neededScore)
        {
            this.position = startingPosition;
            this.StartingPosition = startingPosition;
            this.neededScore = neededScore;
        }

        internal delegate void Win(int startingPosition = 1, int dimentions = 1);

        internal event Win WinEvent;

        internal int RepresentingDimentions { get; set; } = 1;

        internal int RoleCount { get; set; }

        internal int StartingPosition { get; set; }

        internal int Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = ((value - 1) % 10) + 1;
                this.score += this.Position;
                if (this.Won)
                {
                    this.WinEvent(this.StartingPosition, this.RepresentingDimentions);
                }
            }
        }

        internal int Score
        {
            get
            {
                return this.score;
            }
        }

        internal bool Won
        {
            get
            {
                return this.score >= this.neededScore;
            }
        }

        internal Player Clone()
        {
            Player player = new Player(this.StartingPosition, this.neededScore)
            {
                RoleCount = this.RoleCount,
                RepresentingDimentions = this.RepresentingDimentions,
                position = this.position,
                score = this.score
            };
            player.WinEvent += this.WinEvent;

            return player;
        }
    }
}

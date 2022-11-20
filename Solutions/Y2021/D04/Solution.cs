namespace AdventOfCode.Solutions.Y2021.D04
{
    using AdventOfCode.Common;
    using System;

    internal class Solution : Solution<Tuple<int[], Board[]>>
    {
        private int lastCompletion = -1;
        private int drawIndex = 0;

        internal delegate void NewDrawHandler(Solution sender, int draw);

        internal event NewDrawHandler NewDrawEvent;

        internal override (object, string) Puzzle1(Tuple<int[], Board[]> input)
        {
            foreach (Board board in input.Item2)
            {
                board.AddToDrawEvent(this);
                board.CompletedEvent += this.BoardCompleted;
            }

            for (int i = 0; i < input.Item1.Length; i++)
            {
                int draw = input.Item1[i];
                if (this.lastCompletion != -1)
                {
                    break;
                }

                this.NewDrawEvent(this, draw);
                this.drawIndex = i;
            }

            return (this.lastCompletion.ToString(), $"The solution is {this.lastCompletion}!");
        }

        internal override (object, string) Puzzle2(Tuple<int[], Board[]> input)
        {
            for (int i = this.drawIndex + 1; i < input.Item1.Length; i++)
            {
                int draw = input.Item1[i];
                if (this.NewDrawEvent == null)
                {
                    break;
                }

                this.NewDrawEvent(this, draw);
            }

            return (this.lastCompletion.ToString(), $"The solution is {this.lastCompletion}!");
        }

        private void BoardCompleted(Board sender, int lastDraw)
        {
            this.lastCompletion = lastDraw * sender.Value;
        }
    }
}

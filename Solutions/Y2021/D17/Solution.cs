namespace AdventOfCode.Solutions.Y2021.D17
{
    using AdventOfCode.Common;

    internal class Solution : Solution<Target>
    {
        internal override (object, string) Puzzle1(Target input)
        {
            int horizontalSpeed = 1;

            for (; this.EndingPosition(horizontalSpeed) <= input.X + input.Width; horizontalSpeed++)
            {
            }

            var verticalSpeed = (input.Y * -1) - 1;

            int verticalEndingPosition = this.EndingPosition(verticalSpeed);

            // SimulateTrajectory(horizontalSpeed, verticalSpeed, input);
            return (verticalEndingPosition.ToString(), $"The ending position is {verticalEndingPosition}!");
        }

        internal override (object, string) Puzzle2(Target input)
        {
            int counter = 0;

            int minHorizontalSpeed = 0;
            for (; this.EndingPosition(minHorizontalSpeed) < input.X; minHorizontalSpeed++)
            {
            }

            for (int verticalSpeed = input.Y; verticalSpeed <= (input.Y * -1) - 1; verticalSpeed++)
            {
                for (int horizontalSpeed = minHorizontalSpeed; horizontalSpeed <= input.X + input.Width; horizontalSpeed++)
                {
                    if (this.HitsTarget(horizontalSpeed, verticalSpeed, input))
                    {
                        counter++;

                        // SimulateTrajectory(horizontalSpeed, verticalSpeed, input);
                    }
                }
            }

            return (counter.ToString(), $"There are {counter} possible velocity values!");
        }

        private int EndingPosition(int speed)
        {
            int pos = 0;
            for (; speed > 0; speed--)
            {
                pos += speed;
            }

            return pos;
        }

        private bool HitsTarget(int horizontalSpeed, int verticalSpeed, Target target)
        {
            int posX = 0;
            int posY = 0;

            while (posX <= target.X + target.Width && posY >= target.Y)
            {
                posX += horizontalSpeed;
                posY += verticalSpeed;
                horizontalSpeed -= horizontalSpeed > 0 ? 1 : 0;
                verticalSpeed--;

                if (
                    posX >= target.X &&
                    posY >= target.Y &&
                    posY <= target.Y + target.Height &&
                    posX <= target.X + target.Width)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D17
{
    internal class Solution : Solution<Target>
    {
        internal override string Puzzle1(Target input)
        {
            int horizontalSpeed = 1;

            for (; EndingPosition(horizontalSpeed) <= input.X + input.Width; horizontalSpeed++) ;

            horizontalSpeed--;

            int verticalSpeed = 1;

            verticalSpeed = input.Y * - 1 - 1;

            int VerticalEndingPosition = EndingPosition(verticalSpeed);

            //SimulateTrajectory(horizontalSpeed, verticalSpeed, input);

            s_logger.Log($"The ending position is {VerticalEndingPosition}!", SharpLog.LogType.Info);
            return VerticalEndingPosition.ToString();
        }

        internal override string Puzzle2(Target input)
        {
            int counter = 0;

            int minHorizontalSpeed = 0;
            for (; EndingPosition(minHorizontalSpeed) < input.X; minHorizontalSpeed++) ;

            for (int verticalSpeed = input.Y; verticalSpeed <= input.Y * -1 - 1; verticalSpeed++)
            {
                for (int horizontalSpeed = minHorizontalSpeed; horizontalSpeed <= input.X + input.Width; horizontalSpeed++)
                {
                    if (HitsTarget(horizontalSpeed, verticalSpeed, input))
                    {
                        counter++;
                        //SimulateTrajectory(horizontalSpeed, verticalSpeed, input);
                    }
                }
            }

            s_logger.Log($"There are {counter} possible velocity values!", SharpLog.LogType.Info);
            return counter.ToString();
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

        private void SimulateTrajectory(int horizontalSpeed, int verticalSpeed, Target target)
        {
            int posX = 0;
            int posY = 0;

            List<(int, int)> coordinates = new List<(int, int)>();

            while (posX <= target.X + target.Width && posY >= target.Y)
            {
                coordinates.Add((posX, posY));

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
                    coordinates.Add((posX, posY));
                    break;
                }
            }

            coordinates.RemoveAt(0);

            PrintTrajectory(target, coordinates);
        }

        private void PrintTrajectory(Target target, List<(int, int)> coordinates)
        {
            s_logger.Log($"-------------- Printing trajectory: {coordinates[0].Item1}|{coordinates[0].Item2} --------------");

            int highestY = 0;
            foreach((int, int) coordinate in coordinates)
            {
                if (highestY < coordinate.Item2) highestY = coordinate.Item2;
            }

            char[][] image = highestY > 0? new char[highestY - target.Y + 1][] : new char[target.Y * -1 + 1][];
            for (int i = 0; i < image.Length; i++)
            {
                image[i] = new char[target.X + target.Width + 1];
            }

            for (int y = 0; y < image.Length; y++)
            {
                for (int x = 0; x < image[0].Length; x++)
                {
                    image[y][x] = '.';
                }
            }

            image[highestY][0] = 'S';

            for (int y = highestY - target.Y - target.Height; y < image.Length; y++)
            {
                for (int x = target.X; x < image[0].Length; x++)
                {
                    image[y][x] = 'T';
                }
            }

            foreach ((int, int) coordinate in coordinates)
            {
                image[highestY - coordinate.Item2][coordinate.Item1] = '#';
            }

            foreach (char[] line in image)
            {
                s_logger.Log(new string(line));
            }
        }
    }
}

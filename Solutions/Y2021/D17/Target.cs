// <copyright file="Target.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D17
{
    internal class Target
    {
        internal Target(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        internal int X { get; }

        internal int Y { get; }

        internal int Width { get; }

        internal int Height { get; }
    }
}

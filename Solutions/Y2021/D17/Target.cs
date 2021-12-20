using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D17
{
    internal class Target
    {
        internal int X { get; }
        internal int Y { get; }
        internal int Width { get; }
        internal int Height { get; }

        internal Target(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}

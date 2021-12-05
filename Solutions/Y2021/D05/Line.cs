using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Y2021.D05
{
    internal class Line
    {
        private Point _a;
        private Point _b;

        internal bool IsHorizontal
        {
            get
            {
                return _a.Y == _b.Y;
            }
        }

        internal bool IsVertical
        {
            get
            {
                return _a.X == _b.X;
            }
        }

        internal Point[] CoveredPoints
        {
            get
            {
                int delta = IsHorizontal? _b.X - _a.X : _b.Y - _a.Y;
                int signX = Math.Sign(_b.X - _a.X) * Math.Sign(delta);
                int signY = Math.Sign(_b.Y - _a.Y) * Math.Sign(delta);


                delta += Math.Sign(delta);
                List<Point> points = new List<Point>();

                for (int i = 0; i != delta; i += Math.Sign(delta))
                {
                    if (IsHorizontal) points.Add(new Point
                    {
                        X = _a.X + i,
                        Y = _a.Y
                    });
                    else if (IsVertical) points.Add(new Point
                    {
                        X = _a.X,
                        Y = _a.Y + i
                    });
                    else
                    {
                        points.Add(new Point
                        {
                            X = _a.X + (i * signX),
                            Y = _a.Y + (i * signY)
                        });
                    }
                }

                return points.ToArray();
            }
        }

        internal Line(Point a, Point b)
        {
            _a = a;
            _b = b;
        }
    }
}

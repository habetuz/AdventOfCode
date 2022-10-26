// <copyright file="Line.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D05
{
    using System;
    using System.Collections.Generic;

    internal class Line
    {
        private Point a;
        private Point b;

        internal Line(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }

        internal bool IsHorizontal
        {
            get
            {
                return this.a.Y == this.b.Y;
            }
        }

        internal bool IsVertical
        {
            get
            {
                return this.a.X == this.b.X;
            }
        }

        internal Point[] CoveredPoints
        {
            get
            {
                int delta = this.IsHorizontal ? this.b.X - this.a.X : this.b.Y - this.a.Y;
                int signX = Math.Sign(this.b.X - this.a.X) * Math.Sign(delta);
                int signY = Math.Sign(this.b.Y - this.a.Y) * Math.Sign(delta);

                delta += Math.Sign(delta);
                List<Point> points = new List<Point>();

                for (int i = 0; i != delta; i += Math.Sign(delta))
                {
                    if (this.IsHorizontal)
                    {
                        points.Add(new Point
                        {
                            X = this.a.X + i,
                            Y = this.a.Y,
                        });
                    }
                    else if (this.IsVertical)
                    {
                        points.Add(new Point
                        {
                            X = this.a.X,
                            Y = this.a.Y + i,
                        });
                    }
                    else
                    {
                        points.Add(new Point
                        {
                            X = this.a.X + (i * signX),
                            Y = this.a.Y + (i * signY),
                        });
                    }
                }

                return points.ToArray();
            }
        }
    }
}

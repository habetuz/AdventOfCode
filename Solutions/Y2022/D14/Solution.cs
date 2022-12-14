namespace AdventOfCode.Solutions.Y2022.D14
{
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using AdventOfCode.Common.Space;
using SharpLog;

    internal class Solution : Solution<(bool[,], int)>
    {
        internal override (object clipboard, string message) Puzzle1((bool[,], int) input)
        {
            (var canvas, var maxDepth) = input;

            canvas = (bool[,])canvas.Clone();

            var sandCounter = 0;

            var current = new Coordinate2D(500, 0);

            var trace = new Stack<Coordinate2D>();

            trace.Push(current);

            while (current.Y < maxDepth)
            {
                // Fall straight down
                current.Y++;

                if (!canvas[current.X, current.Y])
                {
                    trace.Push(current);
                    continue;
                }

                // Fall diagonally left
                current.X--;

                if (!canvas[current.X, current.Y])
                {
                    trace.Push(current);
                    continue;
                }

                // Fall diagonally right
                current.X += 2;

                if (!canvas[current.X, current.Y])
                {
                    trace.Push(current);
                    continue;
                }

                // Sand unit cannot fall any further and comes to a rest
                var restPosition = trace.Pop();
                canvas[restPosition.X, restPosition.Y] = true;
                current = trace.Peek();
                sandCounter++;
            }

            return (sandCounter, $"[yellow]{sandCounter}[/] sand units come to rest.");
        }

        internal override (object clipboard, string message) Puzzle2((bool[,], int) input)
        {
            (var canvas, var maxDepth) = input;

            var sandCounter = 0;

            var current = new Coordinate2D(500, 0);

            var trace = new Stack<Coordinate2D>();

            trace.Push(current);

            while (true)
            {
                if (current.Y < maxDepth)
                {
                    // Fall straight down
                    current.Y++;

                    if (!canvas[current.X, current.Y])
                    {
                        trace.Push(current);
                        continue;
                    }

                    // Fall diagonally left
                    current.X--;

                    if (!canvas[current.X, current.Y])
                    {
                        trace.Push(current);
                        continue;
                    }

                    // Fall diagonally right
                    current.X += 2;

                    if (!canvas[current.X, current.Y])
                    {
                        trace.Push(current);
                        continue;
                    }
                }

                // Sand unit cannot fall any further and comes to a rest
                sandCounter++;
                var restPosition = trace.Pop();
                canvas[restPosition.X, restPosition.Y] = true;

                // Logging.LogDebug(Tools.Format(canvas, new Coordinate2D(485, 0), new Coordinate2D(515, 11)));
                if (restPosition == new Coordinate2D(500, 0))
                {
                    break;
                }

                current = trace.Peek();
            }

            return (sandCounter, $"[yellow]{sandCounter}[/] sand units come to rest.");
        }
    }
}

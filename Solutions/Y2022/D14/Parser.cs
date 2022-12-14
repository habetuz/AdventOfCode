namespace AdventOfCode.Solutions.Y2022.D14
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;
    using AdventOfCode.Common.Space;
    using SharpLog;

    internal class Parser : Parser<(bool[,], int)>
    {
        internal override (bool[,], int) Parse(string input)
        {
            var canvas = new bool[1000, 200];
            var maxDepth = 0;

            var lines = input.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                var coordinates = Regex.Matches(lines[i], "[0-9]+,[0-9]+")
                    .Cast<Match>()
                    .Select(match => new Coordinate2D(match.Value))
                    .ToArray();

                for (int j = 1; j < coordinates.Length; j++)
                {
                    var from = coordinates[j - 1];
                    var to = coordinates[j];

                    if (from.X != to.X)
                    {
                        if (to.X < from.X)
                        {
                            (from, to) = (to, from);
                        }

                        for (int x = from.X; x <= to.X; x++)
                        {
                            canvas[x, from.Y] = true;
                        }
                    }
                    else
                    {
                        if (to.Y < from.Y)
                        {
                            (from, to) = (to, from);
                        }

                        for (int y = from.Y; y <= to.Y; y++)
                        {
                            canvas[from.X, y] = true;
                        }
                    }

                    if (to.Y > maxDepth)
                    {
                        maxDepth = to.Y;
                    }
                }
            }

            // Logging.LogDebug(Tools.Format(canvas, new Coordinate2D(494, 0), new Coordinate2D(503, 9)));
            return (canvas, maxDepth + 1);
        }
    }
}
namespace AdventOfCode.Solutions.Y2021.D05
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Parser : Parser<Tuple<Line[], Point>>
    {
        internal override Tuple<Line[], Point> Parse(string input)
        {
            string[] lines = input.Split('\n');

            // Parsing
            List<Line> parsedLines = new List<Line>();
            int maxX = 0;
            int maxY = 0;

            foreach (string line in lines)
            {
                string[] pointStrings = line.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                Point[] points = new Point[2];
                for (int i = 0; i < pointStrings.Length; i++)
                {
                    string[] coordinates = pointStrings[i].Split(',');
                    int x = int.Parse(coordinates[0]);
                    int y = int.Parse(coordinates[1]);
                    points[i] = new Point
                    {
                        X = x,
                        Y = y,
                    };

                    if (x > maxX)
                    {
                        Logging.LogDebug($"x: {maxX} -> {x} | line: {line}");
                        maxX = x;
                    }

                    if (y > maxY)
                    {
                        Logging.LogDebug($"y: {maxY} -> {y} | line: {line}");
                        maxY = y;
                    }
                }

                parsedLines.Add(new Line(points[0], points[1]));
            }

            Logging.LogDebug($"There are {parsedLines.Count} lines with a dimension of {maxX} times {maxY}.");
            return new Tuple<Line[], Point>(parsedLines.ToArray(), new Point
            {
                X = maxX + 1,
                Y = maxY + 1,
            });
        }
    }
}

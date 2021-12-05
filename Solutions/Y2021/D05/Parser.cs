using AdventOfCode.Common;
using SharpLog;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Y2021.D05
{
    internal class Parser : Parser<Tuple<Line[], Point>>
    {
        internal override Tuple<Line[], Point> Parse(string input)
        {
            s_logger.LogDebug = false;

            string[] lines = input.Split('\n');

            s_progressTracker = new ProgressTracker(lines.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

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
                        Y = y
                    };

                    if (x > maxX)
                    {
                        s_logger.Log($"x: {maxX} -> {x} | line: {line}");
                        maxX = x;
                    }
                    if (y > maxY)
                    {
                        s_logger.Log($"y: {maxY} -> {y} | line: {line}");
                        maxY = y;
                    }
                }

                parsedLines.Add(new Line(points[0], points[1]));

                s_progressTracker.CurrentStep++;
            }

            s_logger.Log($"There are {parsedLines.Count} lines with a dimension of {maxX} times {maxY}.", LogType.Info);
            return new Tuple<Line[], Point>(parsedLines.ToArray(), new Point
            {
                X = maxX +1,
                Y = maxY +1
            });
        }
    }
}

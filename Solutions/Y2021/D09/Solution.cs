using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D09
{
    internal class Solution : Solution<int[,]>
    {
        private List<Tuple<int, int>> _lowPoints = new List<Tuple<int, int>>();

        internal override string Puzzle1(int[,] input)
        {
            s_progressTracker = new ProgressTracker(input.GetLength(1), (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            int riskLevel = 0;

            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    if (
                        (x - 1 <  0                  || input[x -1, y] > input[x, y]) &&
                        (x + 1 >= input.GetLength(0) || input[x +1, y] > input[x, y]) &&
                        (y - 1 <  0                  || input[x, y -1] > input[x, y]) &&
                        (y + 1 >= input.GetLength(1) || input[x, y +1] > input[x, y]))
                    {
                        riskLevel += input[x, y] + 1;
                        _lowPoints.Add(new Tuple<int, int>(x, y));
                        x++;
                    }
                }

                s_progressTracker.CurrentStep++;
            }

            s_logger.Log($"The sum of all risk levels is {riskLevel}!", LogType.Info);
            return riskLevel.ToString();
        }

        internal override string Puzzle2(int[,] input)
        {
            s_progressTracker = new ProgressTracker(_lowPoints.Count, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            List<int> basinSizes = new List<int>();

            foreach (Tuple<int, int> lowPoint in _lowPoints)
            {
                basinSizes.Add(GetBasinSize(lowPoint, input));
                s_progressTracker.CurrentStep++;
            }

            basinSizes.Sort();
            basinSizes.Reverse();
            int solution = basinSizes[0] * basinSizes[1] * basinSizes[2];
            s_logger.Log($"The multiplication of the three largest basins is {solution}!", LogType.Info);
            return solution.ToString();
        }

        private int GetBasinSize(Tuple<int, int> lowPoint, int[,] map)
        {
            return GetBasingSize(lowPoint, map, new bool[map.GetLength(0), map.GetLength(1)]);
        }

        private int GetBasingSize(Tuple<int, int> point, int[,] map, bool[,] visited)
        {
            visited[point.Item1, point.Item2] = true;
            int size = 1;
            if (
                point.Item1 -1 >= 0 && 
                !visited[point.Item1 -1, point.Item2] && 
                map[point.Item1 - 1, point.Item2] > map[point.Item1, point.Item2] &&
                map[point.Item1 - 1, point.Item2] < 9)
            {
                size += GetBasingSize(new Tuple<int, int>(point.Item1 - 1, point.Item2), map, visited);
            }
            if (
                point.Item1 + 1 < map.GetLength(0) &&
                !visited[point.Item1 + 1, point.Item2] &&
                map[point.Item1 + 1, point.Item2] > map[point.Item1, point.Item2] &&
                map[point.Item1 + 1, point.Item2] < 9)
            {
                size += GetBasingSize(new Tuple<int, int>(point.Item1 + 1, point.Item2), map, visited);
            }
            if (
                point.Item2 - 1 >= 0 &&
                !visited[point.Item1, point.Item2 - 1] &&
                map[point.Item1, point.Item2 - 1] > map[point.Item1, point.Item2] &&
                map[point.Item1, point.Item2 - 1] < 9)
            {
                size += GetBasingSize(new Tuple<int, int>(point.Item1, point.Item2 - 1), map, visited);
            }
            if (
                point.Item2 + 1 < map.GetLength(1) &&
                !visited[point.Item1, point.Item2 + 1] &&
                map[point.Item1, point.Item2 + 1] > map[point.Item1, point.Item2] &&
                map[point.Item1, point.Item2 + 1] < 9)
            {
                size += GetBasingSize(new Tuple<int, int>(point.Item1, point.Item2 + 1), map, visited);
            }

            return size;
        }
    }
}

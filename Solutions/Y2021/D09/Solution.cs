namespace AdventOfCode.Solutions.Y2021.D09
{
    using AdventOfCode.Common;
    using System;
    using System.Collections.Generic;

    internal class Solution : Solution<int[,]>
    {
        private readonly List<Tuple<int, int>> lowPoints = new List<Tuple<int, int>>();

        internal override (object, string) Puzzle1(int[,] input)
        {
            int riskLevel = 0;

            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    if (
                        (x - 1 < 0 || input[x - 1, y] > input[x, y]) &&
                        (x + 1 >= input.GetLength(0) || input[x + 1, y] > input[x, y]) &&
                        (y - 1 < 0 || input[x, y - 1] > input[x, y]) &&
                        (y + 1 >= input.GetLength(1) || input[x, y + 1] > input[x, y]))
                    {
                        riskLevel += input[x, y] + 1;
                        this.lowPoints.Add(new Tuple<int, int>(x, y));
                        x++;
                    }
                }
            }

            return (riskLevel.ToString(), $"The sum of all risk levels is {riskLevel}!");
        }

        internal override (object, string) Puzzle2(int[,] input)
        {
            List<int> basinSizes = new List<int>();

            foreach (Tuple<int, int> lowPoint in this.lowPoints)
            {
                basinSizes.Add(this.GetBasinSize(lowPoint, input));
            }

            basinSizes.Sort();
            basinSizes.Reverse();
            int solution = basinSizes[0] * basinSizes[1] * basinSizes[2];
            return (solution.ToString(), $"The multiplication of the three largest basins is {solution}!");
        }

        private int GetBasinSize(Tuple<int, int> lowPoint, int[,] map)
        {
            return this.GetBasingSize(lowPoint, map, new bool[map.GetLength(0), map.GetLength(1)]);
        }

        private int GetBasingSize(Tuple<int, int> point, int[,] map, bool[,] visited)
        {
            visited[point.Item1, point.Item2] = true;
            int size = 1;
            if (
                point.Item1 - 1 >= 0 &&
                !visited[point.Item1 - 1, point.Item2] &&
                map[point.Item1 - 1, point.Item2] < 9)
            {
                size += this.GetBasingSize(new Tuple<int, int>(point.Item1 - 1, point.Item2), map, visited);
            }

            if (
                point.Item1 + 1 < map.GetLength(0) &&
                !visited[point.Item1 + 1, point.Item2] &&
                map[point.Item1 + 1, point.Item2] < 9)
            {
                size += this.GetBasingSize(new Tuple<int, int>(point.Item1 + 1, point.Item2), map, visited);
            }

            if (
                point.Item2 - 1 >= 0 &&
                !visited[point.Item1, point.Item2 - 1] &&
                map[point.Item1, point.Item2 - 1] < 9)
            {
                size += this.GetBasingSize(new Tuple<int, int>(point.Item1, point.Item2 - 1), map, visited);
            }

            if (
                point.Item2 + 1 < map.GetLength(1) &&
                !visited[point.Item1, point.Item2 + 1] &&
                map[point.Item1, point.Item2 + 1] < 9)
            {
                size += this.GetBasingSize(new Tuple<int, int>(point.Item1, point.Item2 + 1), map, visited);
            }

            return size;
        }
    }
}

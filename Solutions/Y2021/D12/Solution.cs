using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D12
{
    internal class Solution : Solution<Node>
    {
        internal override string Puzzle1(Node input)
        {
            int paths = FindAllPaths(input, true);

            s_logger.Log($"There are {paths} paths!", LogType.Info);
            return paths.ToString();
        }

        internal override string Puzzle2(Node input)
        {
            int paths = FindAllPaths(input, false);

            s_logger.Log($"There are {paths} paths!", LogType.Info);
            return paths.ToString();
        }

        private int FindAllPaths(Node input, bool visitedTwice)
        {
            return FindAllPaths(input, new List<Node>(), visitedTwice);
        }

        private int FindAllPaths(Node input, List<Node> visitedSmallNodes, bool visitedTwice)
        {
            if (input.Name == "end")
            {
                return 1;
            }

            visitedSmallNodes =  visitedSmallNodes.ToList();
            if (!input.IsBig)
            {
                visitedSmallNodes.Add(input);
            }

            int paths = 0;

            foreach (Node node in input.Connections)
            {
                if (node.IsBig || !visitedSmallNodes.Contains(node) || !visitedTwice)
                {
                    paths += FindAllPaths(node, visitedSmallNodes, visitedSmallNodes.Contains(node) || visitedTwice);
                }
            }

            return paths;
        }
    }
}

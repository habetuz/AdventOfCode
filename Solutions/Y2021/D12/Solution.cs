namespace AdventOfCode.Solutions.Y2021.D12
{
    using AdventOfCode.Common;
    using System.Collections.Generic;
    using System.Linq;

    internal class Solution : Solution<Node>
    {
        internal override (object, string) Puzzle1(Node input)
        {
            int paths = this.FindAllPaths(input, true);

            return (paths.ToString(), $"There are {paths} paths!");
        }

        internal override (object, string) Puzzle2(Node input)
        {
            int paths = this.FindAllPaths(input, false);

            return (paths.ToString(), $"There are {paths} paths!");
        }

        private int FindAllPaths(Node input, bool visitedTwice)
        {
            return this.FindAllPaths(input, new List<Node>(), visitedTwice);
        }

        private int FindAllPaths(Node input, List<Node> visitedSmallNodes, bool visitedTwice)
        {
            if (input.Name == "end")
            {
                return 1;
            }

            visitedSmallNodes = visitedSmallNodes.ToList();
            if (!input.IsBig)
            {
                visitedSmallNodes.Add(input);
            }

            int paths = 0;

            foreach (Node node in input.Connections)
            {
                if (node.IsBig || !visitedSmallNodes.Contains(node) || !visitedTwice)
                {
                    paths += this.FindAllPaths(node, visitedSmallNodes, visitedSmallNodes.Contains(node) || visitedTwice);
                }
            }

            return paths;
        }
    }
}

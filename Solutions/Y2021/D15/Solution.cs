using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D15
{
    internal class Solution : Solution<Node[,]>
    {
        internal override string Puzzle1(Node[,] input)
        {
            s_logger.LogDebug = true;

            List<Node> queue = new List<Node>();

            queue.Add(input[0, 0]);

            int riskLevel = 0;

            int iterationCount = 0;

            while (true)
            {
                iterationCount++;

                Node node = queue[0];

                if (node.X == input.GetLength(0) - 1 && node.Y == input.GetLength(1) - 1)
                {
                    riskLevel = node.F;
                    break;
                }

                queue.RemoveAt(0);
                node.Discovered = true;

                if (node.X - 1 >= 0) DiscoverNode(node, input[node.X - 1, node.Y], input, queue);
                if (node.Y - 1 >= 0) DiscoverNode(node, input[node.X, node.Y - 1], input, queue);
                if (node.X + 1 < input.GetLength(0)) DiscoverNode(node, input[node.X + 1, node.Y], input, queue);
                if (node.Y + 1 < input.GetLength(1)) DiscoverNode(node, input[node.X, node.Y + 1], input, queue);
            }

            s_logger.Log(iterationCount);

            s_logger.Log($"The the path with the lowest risk level hast the risk level {riskLevel}", SharpLog.LogType.Info);
            return riskLevel.ToString();
        }

        internal override string Puzzle2(Node[,] input)
        {
            s_logger.LogDebug = true;

            Node[,] map = new Node[input.GetLength(0) * 5, input.GetLength(1) * 5];

            for (int tileX = 0; tileX < 5; tileX++)
            {
                for (int tileY = 0; tileY < 5; tileY++)
                {
                    for (int x = 0; x < input.GetLength(0); x++)
                    {
                        for (int y = 0; y < input.GetLength(1); y++)
                        {
                            int riskLevel = input[x, y].RiskLevel;
                            riskLevel += tileX + tileY;
                            if (riskLevel > 9) riskLevel -= 9;
                            map[tileX * 100 + x, tileY * 100 + y] = new Node(riskLevel, tileX * 100 + x, tileY * 100 + y);
                        }
                    }
                }
            }

            List<Node> queue = new List<Node>();

            queue.Add(map[0, 0]);

            int pahtRiskLevel = 0;

            while (true)
            {
                Node node = queue[0];

                if (node.X == map.GetLength(0) - 1 && node.Y == map.GetLength(1) - 1)
                {
                    s_logger.Log($"Left: {map[map.GetLength(0) - 2, map.GetLength(1) - 1].F} | Top: {map[map.GetLength(0) - 2, map.GetLength(1) - 1].F}");
                    s_logger.Log($"F: {node.F} | Risk: {node.RiskLevel}");

                    pahtRiskLevel = node.F;
                    break;
                }

                queue.RemoveAt(0);
                node.Discovered = true;

                if (node.X - 1 >= 0) DiscoverNode(node, map[node.X - 1, node.Y], map, queue);
                if (node.Y - 1 >= 0) DiscoverNode(node, map[node.X, node.Y - 1], map, queue);
                if (node.X + 1 < map.GetLength(0)) DiscoverNode(node, map[node.X + 1, node.Y], map, queue);
                if (node.Y + 1 < map.GetLength(1)) DiscoverNode(node, map[node.X, node.Y + 1], map, queue);
            }


            s_logger.Log($"The the path with the lowest risk level hast the risk level {pahtRiskLevel}", SharpLog.LogType.Info);
            return pahtRiskLevel.ToString();
        }

        private void DiscoverNode(Node origin, Node node, Node[,] map, List<Node> queue)
        {
            if (node.Discovered) return;

            int f = origin.F + node.RiskLevel;

            node.F = node.F == 0 || node.F > f? f : node.F;

            while(queue.Remove(node)) { }
            
            AddToQueue(node, queue);
        }

        private void AddToQueue(Node node, List<Node> queue)
        {
            for (int i = queue.Count - 1; i >= 0; i--)
            {
                if (node.CompareTo(queue[i]) >= 0)
                {
                    if (i + 1 < queue.Count)
                    {
                        queue.Insert(i + 1, node);
                        return;
                    }
                    break;
                }
            }

            queue.Add(node);
        }
    }
}

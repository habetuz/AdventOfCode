namespace AdventOfCode.Solutions.Y2022.D12
{
    using AdventOfCode.Common;
    using AdventOfCode.Common.PathFinding;

    internal class Solution : Solution<(Node[,], Node, Node)>
    {
        internal override (object clipboard, string message) Puzzle1((Node[,], Node, Node) input)
        {
            (var map, var start, var end) = input;

            var queue = new PriorityQueue<Node>();

            start.Discovered = true;
            start.Distance = Node.GetDistance(start, end);
            queue.Enqueue(start);

            while (!end.Discovered)
            {
                var node = queue.Dequeue();

                // Right
                if (node.X < map.GetLength(0) - 1 &&
                    map[node.X + 1, node.Y].Height <= node.Height + 1 &&
                    (!map[node.X + 1, node.Y].Discovered || node.Cost + 1 < map[node.X + 1, node.Y].Cost))
                {
                    map[node.X + 1, node.Y].Discovered = true;
                    map[node.X + 1, node.Y].Distance = Node.GetDistance(map[node.X + 1, node.Y], end);
                    map[node.X + 1, node.Y].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X + 1, node.Y]);
                }

                // Left
                if (node.X > 0 &&
                    map[node.X - 1, node.Y].Height <= node.Height + 1 &&
                    (!map[node.X - 1, node.Y].Discovered || node.Cost + 1 < map[node.X - 1, node.Y].Cost))
                {
                    map[node.X - 1, node.Y].Discovered = true;
                    map[node.X - 1, node.Y].Distance = Node.GetDistance(map[node.X - 1, node.Y], end);
                    map[node.X - 1, node.Y].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X - 1, node.Y]);
                }

                // Down
                if (node.Y < map.GetLength(1) - 1 &&
                    map[node.X, node.Y + 1].Height <= node.Height + 1 &&
                    (!map[node.X, node.Y + 1].Discovered || node.Cost + 1 < map[node.X, node.Y + 1].Cost))
                {
                    map[node.X, node.Y + 1].Discovered = true;
                    map[node.X, node.Y + 1].Distance = Node.GetDistance(map[node.X, node.Y + 1], end);
                    map[node.X, node.Y + 1].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X, node.Y + 1]);
                }

                // Up
                if (node.Y > 0 &&
                    map[node.X, node.Y - 1].Height <= node.Height + 1 &&
                    (!map[node.X, node.Y - 1].Discovered || node.Cost + 1 < map[node.X, node.Y - 1].Cost))
                {
                    map[node.X, node.Y - 1].Discovered = true;
                    map[node.X, node.Y - 1].Distance = Node.GetDistance(map[node.X, node.Y - 1], end);
                    map[node.X, node.Y - 1].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X, node.Y - 1]);
                }
            }

            return (end.Cost, $"The shortest path has a distance of [yellow]{end.Cost}[/]!");
        }

        internal override (object clipboard, string message) Puzzle2((Node[,], Node, Node) input)
        {
            (var map, _, var end) = input;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y].Discovered = false;
                }
            }

            var queue = new PriorityQueue<Node>();

            end.Discovered = true;
            end.Distance = end.X;
            end.Cost = 0;

            queue.Enqueue(end);

            while (true)
            {
                var node = queue.Dequeue();

                if (node.X == 0)
                {
                    return (node.Cost, $"The shortest path has a distance of [yellow]{node.Cost}[/]!");
                }

                // Right
                if (node.X < map.GetLength(0) - 1 &&
                    map[node.X + 1, node.Y].Height >= node.Height - 1 &&
                    (!map[node.X + 1, node.Y].Discovered || node.Cost + 1 < map[node.X + 1, node.Y].Cost))
                {
                    map[node.X + 1, node.Y].Discovered = true;
                    map[node.X + 1, node.Y].Distance = node.X + 1;
                    map[node.X + 1, node.Y].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X + 1, node.Y]);
                }

                // Left
                if (node.X > 0 &&
                    map[node.X - 1, node.Y].Height >= node.Height - 1 &&
                    (!map[node.X - 1, node.Y].Discovered || node.Cost + 1 < map[node.X - 1, node.Y].Cost))
                {
                    map[node.X - 1, node.Y].Discovered = true;
                    map[node.X - 1, node.Y].Distance = node.X - 1;
                    map[node.X - 1, node.Y].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X - 1, node.Y]);
                }

                // Down
                if (node.Y < map.GetLength(1) - 1 &&
                    map[node.X, node.Y + 1].Height >= node.Height - 1 &&
                    (!map[node.X, node.Y + 1].Discovered || node.Cost + 1 < map[node.X, node.Y + 1].Cost))
                {
                    map[node.X, node.Y + 1].Discovered = true;
                    map[node.X, node.Y + 1].Distance = node.X;
                    map[node.X, node.Y + 1].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X, node.Y + 1]);
                }

                // Up
                if (node.Y > 0 &&
                    map[node.X, node.Y - 1].Height >= node.Height - 1 &&
                    (!map[node.X, node.Y - 1].Discovered || node.Cost + 1 < map[node.X, node.Y - 1].Cost))
                {
                    map[node.X, node.Y - 1].Discovered = true;
                    map[node.X, node.Y - 1].Distance = node.X;
                    map[node.X, node.Y - 1].Cost = node.Cost + 1;
                    queue.Enqueue(map[node.X, node.Y - 1]);
                }
            }
        }
    }
}

namespace AdventOfCode.Solutions.Y2021.D12
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<Node>
    {
        internal override Node Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (string line in lines)
            {
                string[] connections = line.Split('-');

                if (!nodes.TryGetValue(connections[0], out Node nodeA))
                {
                    nodeA = new Node(connections[0]);
                    nodes.Add(connections[0], nodeA);
                }

                if (!nodes.TryGetValue(connections[1], out Node nodeB))
                {
                    nodeB = new Node(connections[1]);
                    nodes.Add(connections[1], nodeB);
                }

                var nodeAConnections = nodeA.Connections;
                Array.Resize(ref nodeAConnections, nodeA.Connections.Length + 1);
                nodeA.Connections = nodeAConnections;

                nodeA.Connections[nodeA.Connections.Length - 1] = nodeB;

                if (connections[0] != "start" && connections[1] != "end")
                {
                    var nodeBConnections = nodeB.Connections;
                    Array.Resize(ref nodeBConnections, nodeB.Connections.Length + 1);
                    nodeB.Connections = nodeBConnections;
                    nodeB.Connections[nodeB.Connections.Length - 1] = nodeA;
                }
            }

            return nodes["start"];
        }
    }
}

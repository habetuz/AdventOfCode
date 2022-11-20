namespace AdventOfCode.Solutions.Y2021.D12
{
    using AdventOfCode.Common;
    using System;
    using System.Collections.Generic;

    internal class Parser : Parser<Node>
    {
        internal override Node Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (string line in lines)
            {
                string[] connections = line.Split('-');

                Node nodeA;
                Node nodeB;
                if (!nodes.TryGetValue(connections[0], out nodeA))
                {
                    nodeA = new Node(connections[0]);
                    nodes.Add(connections[0], nodeA);
                }

                if (!nodes.TryGetValue(connections[1], out nodeB))
                {
                    nodeB = new Node(connections[1]);
                    nodes.Add(connections[1], nodeB);
                }

                Array.Resize(ref nodeA.Connections, nodeA.Connections.Length + 1);
                nodeA.Connections[nodeA.Connections.Length - 1] = nodeB;

                if (connections[0] != "start" && connections[1] != "end")
                {
                    Array.Resize(ref nodeB.Connections, nodeB.Connections.Length + 1);
                    nodeB.Connections[nodeB.Connections.Length - 1] = nodeA;
                }
            }

            return nodes["start"];
        }
    }
}

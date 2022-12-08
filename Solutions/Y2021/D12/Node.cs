// <copyright file="Node.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D12
{
    internal class Node
    {
        private Node[] connections = new Node[0];

        internal Node(string name)
        {
            this.Name = name;
            this.IsBig = char.IsUpper(this.Name[0]);
        }

        internal Node[] Connections { get => this.connections; set => this.connections = value; }

        internal string Name { get; }

        internal bool IsBig { get; }
    }
}

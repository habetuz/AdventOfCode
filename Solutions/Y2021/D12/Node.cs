// <copyright file="Node.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Node
    {
        internal Node(string name)
        {
            this.Name = name;
            this.IsBig = char.IsUpper(this.Name[0]);
        }

        internal Node[] Connections = new Node[0];

        internal string Name { get; }

        internal bool IsBig { get; }
    }
}

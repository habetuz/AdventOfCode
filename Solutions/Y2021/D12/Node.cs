using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D12
{
    internal class Node
    {
        internal Node[] Connections = new Node[0];

        internal string Name { get; }

        internal bool IsBig { get; }

        internal Node(string name)
        {
            Name = name;
            IsBig = char.IsUpper(Name[0]);
        }
    }
}

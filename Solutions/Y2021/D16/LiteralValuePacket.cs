using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D16
{
    internal class LiteralValuePacket : Packet
    {
        internal long Value { get; set; }

        internal LiteralValuePacket()
        {
            ID = 4;
        }
    }
}

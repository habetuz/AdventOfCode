using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D16
{
    internal class OperatorPacket : Packet
    {
        internal bool LengthInSubPackets { get; set; }
        internal short Length { get; set; }
        internal Packet[] SubPackets { get; set; }
    }
}

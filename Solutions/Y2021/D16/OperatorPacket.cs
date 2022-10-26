// <copyright file="OperatorPacket.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class OperatorPacket : Packet
    {
        internal bool LengthInSubPackets { get; set; }

        internal short Length { get; set; }

        internal Packet[] SubPackets { get; set; }
    }
}

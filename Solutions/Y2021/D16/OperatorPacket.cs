// <copyright file="OperatorPacket.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D16
{
    internal class OperatorPacket : Packet
    {
        internal bool LengthInSubPackets { get; set; }

        internal short Length { get; set; }

        internal Packet[] SubPackets { get; set; }
    }
}

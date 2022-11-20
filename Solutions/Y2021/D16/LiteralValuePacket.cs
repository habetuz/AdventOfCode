// <copyright file="LiteralValuePacket.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D16
{
    internal class LiteralValuePacket : Packet
    {
        internal LiteralValuePacket()
        {
            this.ID = 4;
        }

        internal long Value { get; set; }
    }
}

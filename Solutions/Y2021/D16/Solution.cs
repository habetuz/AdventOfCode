namespace AdventOfCode.Solutions.Y2021.D16
{
    using AdventOfCode.Common;

    internal class Solution : Solution<Packet>
    {
        internal override (object, string) Puzzle1(Packet input)
        {
            int versionSum = this.RecursiveVersionSum(input);

            return (versionSum.ToString(), $"The version sum is {versionSum}!");
        }

        internal override (object, string) Puzzle2(Packet input)
        {
            long value = this.RecursiveValueCalculation(input);

            return (value.ToString(), $"The value is {value}!");
        }

        private int RecursiveVersionSum(Packet packet)
        {
            if (packet.GetType() == typeof(LiteralValuePacket))
            {
                return packet.Version;
            }

            int versionSum = packet.Version;

            foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
            {
                versionSum += this.RecursiveVersionSum(subPacket);
            }

            return versionSum;
        }

        private long RecursiveValueCalculation(Packet packet)
        {
            long value = 0;

            switch (packet.ID)
            {
                // Sum
                case 0:
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        value += this.RecursiveValueCalculation(subPacket);
                    }

                    break;

                // Product
                case 1:
                    value = 1;
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        value *= this.RecursiveValueCalculation(subPacket);
                    }

                    break;

                // Minimum
                case 2:
                    value = long.MaxValue;
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        long subPacketValue = this.RecursiveValueCalculation(subPacket);
                        if (value > subPacketValue)
                        {
                            value = subPacketValue;
                        }
                    }

                    break;

                // Maximum
                case 3:
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        long subPacketValue = this.RecursiveValueCalculation(subPacket);
                        if (value < subPacketValue)
                        {
                            value = subPacketValue;
                        }
                    }

                    break;

                // Literal Value
                case 4:
                    value = ((LiteralValuePacket)packet).Value;
                    break;

                // Greater than
                case 5:
                    value =
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) >
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ?
                        1 : 0;

                    break;

                // Less than
                case 6:
                    value =
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) <
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ?
                        1 : 0;

                    break;

                // Equal to
                case 7:
                    value =
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) ==
                        this.RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ?
                        1 : 0;

                    break;
            }

            return value;
        }
    }
}

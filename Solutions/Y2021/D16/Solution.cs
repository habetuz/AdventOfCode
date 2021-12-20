using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D16
{
    internal class Solution : Solution<Packet>
    {
        internal override string Puzzle1(Packet input)
        {
            int versionSum = RecursiveVersionSum(input);

            s_logger.Log($"The version sum is {versionSum}!", SharpLog.LogType.Info);
            return versionSum.ToString();
        }

        internal override string Puzzle2(Packet input)
        {
            long value = RecursiveValueCalculation(input);

            s_logger.Log($"The value is {value}!", SharpLog.LogType.Info);
            return value.ToString();
        }

        private int RecursiveVersionSum(Packet packet)
        {
            if (packet.GetType() == typeof(LiteralValuePacket))
            {
                return packet.Version;
            }

            int versionSum = packet.Version;

            foreach (Packet subPacket in ((OperatorPacket) packet).SubPackets)
            {
                versionSum += RecursiveVersionSum(subPacket);
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
                        value += RecursiveValueCalculation(subPacket);
                    }

                    break;

                // Product
                case 1:
                    value = 1;
                    foreach (Packet subPacket in ((OperatorPacket) packet).SubPackets)
                    {
                        value *= RecursiveValueCalculation(subPacket);
                    }

                    break;

                // Minimum
                case 2:
                    value = long.MaxValue;
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        long subPacketValue = RecursiveValueCalculation(subPacket);
                        if (value > subPacketValue) value = subPacketValue;
                    }

                    break;

                // Maximum
                case 3:
                    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
                    {
                        long subPacketValue = RecursiveValueCalculation(subPacket);
                        if (value < subPacketValue) value = subPacketValue;
                    }

                    break;

                // Literal Value
                case 4:
                    value =  ((LiteralValuePacket) packet).Value;
                    break;

                // Greater than
                case 5:
                    value = 
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) > 
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ? 
                        1 : 0;

                    break;

                // Less than
                case 6:
                    value =
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) <
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ?
                        1 : 0;

                    break;

                // Equal to
                case 7:
                    value =
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0]) ==
                        RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1]) ?
                        1 : 0;

                    break;
            }

            return value;
        }
    }
}

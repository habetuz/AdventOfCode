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
    }
}

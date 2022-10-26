namespace AdventOfCode.Solutions.Y2021.D16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<Packet>
    {
        private readonly Dictionary<char, string> hexBinaryPairs = new Dictionary<char, string>()
        {
            {'0', "0000" },
            {'1', "0001" },
            {'2', "0010" },
            {'3', "0011" },
            {'4', "0100" },
            {'5', "0101" },
            {'6', "0110" },
            {'7', "0111" },
            {'8', "1000" },
            {'9', "1001" },
            {'A', "1010" },
            {'B', "1011" },
            {'C', "1100" },
            {'D', "1101" },
            {'E', "1110" },
            {'F', "1111" },
        };

        internal override Packet Parse(string input)
        {
            string binary = string.Empty;
            foreach (char c in input)
            {
                binary += this.hexBinaryPairs[c];
            }

            (Packet packet, int index) = this.ParsePacket(binary);
            return packet;
        }

        private (Packet, int) ParsePacket(string binary, int index = 0)
        {
            byte version = Convert.ToByte(binary.Substring(index, 3), 2);
            byte id = Convert.ToByte(binary.Substring(index + 3, 3), 2);

            index += 6;

            // Literal value packet
            if (id == 4)
            {
                LiteralValuePacket packet = new LiteralValuePacket()
                {
                    Version = version,
                };

                string valueBinary = string.Empty;

                index -= 5;

                do
                {
                    index += 5;
                    valueBinary += binary.Substring(index + 1, 4);
                }
                while (binary[index] == '1');

                index += 5;

                packet.Value = Convert.ToInt64(valueBinary, 2);

                return (packet, index);
            }

            // Operator packet
            else
            {
                OperatorPacket packet = new OperatorPacket()
                {
                    Version = version,
                    ID = id,
                    LengthInSubPackets = binary[index] == '1',
                };

                packet.Length = packet.LengthInSubPackets ? Convert.ToInt16(binary.Substring(index + 1, 11), 2) : Convert.ToInt16(binary.Substring(index + 1, 15), 2);

                index += packet.LengthInSubPackets ? 12 : 16;

                List<Packet> subPackets = new List<Packet>();

                int subPacketsStartIndex = index;

                while ((packet.LengthInSubPackets && subPackets.Count < packet.Length) || (!packet.LengthInSubPackets && index < packet.Length + subPacketsStartIndex))
                {
                    (Packet subPacket, int newIndex) = this.ParsePacket(binary, index);
                    subPackets.Add(subPacket);
                    index = newIndex;
                }

                packet.SubPackets = subPackets.ToArray();
                return (packet, index);
            }
        }
    }
}

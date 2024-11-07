using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D16;

public class Solver : ISolver<Packet>
{
  private readonly Dictionary<char, string> hexBinaryPairs = new Dictionary<char, string>()
  {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'A', "1010" },
    { 'B', "1011" },
    { 'C', "1100" },
    { 'D', "1101" },
    { 'E', "1110" },
    { 'F', "1111" },
  };

  public void Parse(string input, IPartSubmitter<Packet> partSubmitter)
  {
    string binary = string.Empty;
    foreach (char c in input)
    {
      binary += hexBinaryPairs[c];
    }

    (Packet packet, _) = ParsePacket(binary);
    partSubmitter.Submit(packet);
  }

  public void Solve(Packet input, IPartSubmitter partSubmitter)
  {
    int versionSum = RecursiveVersionSum(input);

    partSubmitter.SubmitPart1(versionSum);

    long value = RecursiveValueCalculation(input);

    partSubmitter.SubmitPart2(value);
  }

  private static int RecursiveVersionSum(Packet packet)
  {
    if (packet.GetType() == typeof(LiteralValuePacket))
    {
      return packet.Version;
    }

    int versionSum = packet.Version;

    foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
    {
      versionSum += RecursiveVersionSum(subPacket);
    }

    return versionSum;
  }

  private static long RecursiveValueCalculation(Packet packet)
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
        foreach (Packet subPacket in ((OperatorPacket)packet).SubPackets)
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
          long subPacketValue = RecursiveValueCalculation(subPacket);
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
          RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0])
          > RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1])
            ? 1
            : 0;

        break;

      // Less than
      case 6:
        value =
          RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0])
          < RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1])
            ? 1
            : 0;

        break;

      // Equal to
      case 7:
        value =
          RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[0])
          == RecursiveValueCalculation(((OperatorPacket)packet).SubPackets[1])
            ? 1
            : 0;

        break;
    }

    return value;
  }

  private static (Packet, int) ParsePacket(string binary, int index = 0)
  {
    byte version = Convert.ToByte(binary.Substring(index, 3), 2);
    byte id = Convert.ToByte(binary.Substring(index + 3, 3), 2);

    index += 6;

    // Literal value packet
    if (id == 4)
    {
      LiteralValuePacket packet = new() { Version = version };

      string valueBinary = string.Empty;

      index -= 5;

      do
      {
        index += 5;
        valueBinary += binary.Substring(index + 1, 4);
      } while (binary[index] == '1');

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

      packet.Length = packet.LengthInSubPackets
        ? Convert.ToInt16(binary.Substring(index + 1, 11), 2)
        : Convert.ToInt16(binary.Substring(index + 1, 15), 2);

      index += packet.LengthInSubPackets ? 12 : 16;

      List<Packet> subPackets = [];

      int subPacketsStartIndex = index;

      while (
        (packet.LengthInSubPackets && subPackets.Count < packet.Length)
        || (!packet.LengthInSubPackets && index < packet.Length + subPacketsStartIndex)
      )
      {
        (Packet subPacket, int newIndex) = ParsePacket(binary, index);
        subPackets.Add(subPacket);
        index = newIndex;
      }

      packet.SubPackets = subPackets.ToArray();
      return (packet, index);
    }
  }
}

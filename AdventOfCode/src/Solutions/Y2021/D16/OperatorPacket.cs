namespace AdventOfCode.Solutions.Y2021.D16
{
  public class OperatorPacket : Packet
  {
    public bool LengthInSubPackets { get; set; }

    public short Length { get; set; }

    public Packet[] SubPackets { get; set; } = [];
  }
}

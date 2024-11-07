namespace AdventOfCode.Solutions.Y2021.D16;

public class LiteralValuePacket : Packet
{
  public LiteralValuePacket()
  {
    ID = 4;
  }

  public long Value { get; set; }
}

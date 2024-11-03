namespace AdventOfCode.Solutions.Y2023.D04;

public struct Game(byte[] winningNumbers, byte[] numbers)
{
  public byte[] WinningNumbers { get; init; } = winningNumbers;
  public byte[] Numbers { get; init; } = numbers;

  public uint Instances { get; set; } = 1;
}

namespace AdventOfCode.Solutions.Y2023.D04;

public struct Game
{
    public Game(byte[] winningNumbers, byte[] numbers)
    {
        this.WinningNumbers = winningNumbers;
        this.Numbers = numbers;
        this.Instances = 1;
    }

    public byte[] WinningNumbers { get; init; }
    public byte[] Numbers { get; init; }

    public uint Instances { get; set; }
}

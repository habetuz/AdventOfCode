using AdventOfCode.Commands;

namespace AdventOfCode.Solutions.Y2021.D08;

public readonly struct Display(string[] inputs, string[] digits)
{
  public static readonly Dictionary<short, char[]> DigitSegments = new Dictionary<short, char[]>()
  {
    { 0, ['a', 'b', 'c', 'e', 'f', 'g'] },
    { 1, ['c', 'f'] },
    { 2, ['a', 'c', 'd', 'e', 'g'] },
    { 3, ['a', 'c', 'd', 'f', 'g'] },
    { 4, ['b', 'c', 'd', 'f'] },
    { 5, ['a', 'b', 'd', 'f', 'g'] },
    { 6, ['a', 'b', 'd', 'e', 'f', 'g'] },
    { 7, ['a', 'c', 'f'] },
    { 8, ['a', 'b', 'c', 'd', 'e', 'f', 'g'] },
    { 9, ['a', 'b', 'c', 'd', 'f', 'g'] },
  };

  public string[] Inputs { get; } = inputs;
  public string[] Digits { get; } = digits;
}

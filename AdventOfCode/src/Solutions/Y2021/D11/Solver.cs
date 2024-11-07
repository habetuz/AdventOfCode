using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2021.D11;

public class Solver : CustomGridSplittingSolver<byte>
{
  public override byte Convert(char value, int x, int y)
  {
    return (byte)(value - '0');
  }

  public override void Solve(byte[,] input, IPartSubmitter partSubmitter)
  {
    int flashes = 0;

    for (int i = 1; i <= 100; i++)
    {
      for (int x = 0; x < input.GetLength(0); x++)
      {
        for (int y = 0; y < input.GetLength(1); y++)
        {
          IncreaseEnergy(input, x, y, ref flashes);
        }
      }

      for (int x = 0; x < input.GetLength(0); x++)
      {
        for (int y = 0; y < input.GetLength(1); y++)
        {
          if (input[x, y] > 9)
          {
            input[x, y] = 0;
          }
        }
      }
    }

    partSubmitter.SubmitPart1(flashes);

    int syncStep;

    for (int i = 101; true; i++)
    {
      for (int x = 0; x < input.GetLength(0); x++)
      {
        for (int y = 0; y < input.GetLength(1); y++)
        {
          IncreaseEnergy(input, x, y);
        }
      }

      for (int x = 0; x < input.GetLength(0); x++)
      {
        for (int y = 0; y < input.GetLength(1); y++)
        {
          if (input[x, y] > 9)
          {
            input[x, y] = 0;
          }
        }
      }

      int sum = 0;
      foreach (int value in input)
      {
        sum += value;
      }

      if (sum == 0)
      {
        syncStep = i;
        break;
      }
    }

    partSubmitter.SubmitPart2(syncStep);
  }

  private static void IncreaseEnergy(byte[,] input, int x, int y, ref int flashes)
  {
    if (x < 0 || x >= input.GetLength(0) || y < 0 || y >= input.GetLength(1))
    {
      return;
    }

    input[x, y]++;
    if (input[x, y] == 10)
    {
      flashes++;
      IncreaseEnergy(input, x - 1, y + 0, ref flashes);
      IncreaseEnergy(input, x - 1, y - 1, ref flashes);
      IncreaseEnergy(input, x + 0, y - 1, ref flashes);
      IncreaseEnergy(input, x + 1, y - 1, ref flashes);
      IncreaseEnergy(input, x + 1, y + 0, ref flashes);
      IncreaseEnergy(input, x + 1, y + 1, ref flashes);
      IncreaseEnergy(input, x + 0, y + 1, ref flashes);
      IncreaseEnergy(input, x - 1, y + 1, ref flashes);
    }
  }

  private static void IncreaseEnergy(byte[,] input, int x, int y)
  {
    if (x < 0 || x >= input.GetLength(0) || y < 0 || y >= input.GetLength(1))
    {
      return;
    }

    input[x, y]++;
    if (input[x, y] == 10)
    {
      IncreaseEnergy(input, x - 1, y + 0);
      IncreaseEnergy(input, x - 1, y - 1);
      IncreaseEnergy(input, x + 0, y - 1);
      IncreaseEnergy(input, x + 1, y - 1);
      IncreaseEnergy(input, x + 1, y + 0);
      IncreaseEnergy(input, x + 1, y + 1);
      IncreaseEnergy(input, x + 0, y + 1);
      IncreaseEnergy(input, x - 1, y + 1);
    }
  }
}

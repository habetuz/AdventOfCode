using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2021.D07;

public class Solver : NumbersSolver
{
  public override void Solve(int[] input, IPartSubmitter partSubmitter)
  {
    Array.Sort(input);

    int fuel = 0;
    int alignPosition =
      input.Length % 2 == 0
        ? input[input.Length / 2]
        : (input[input.Length / 2] + input[(input.Length / 2) + 1]) / 2;

    foreach (int crab in input)
    {
      fuel += Math.Abs(crab - alignPosition);
    }

    partSubmitter.SubmitPart1(fuel);

    int fuelLeft = 0;
    int fuelRight = 0;
    int fuelMiddle = 0;
    alignPosition = 0;
    foreach (int crab in input)
    {
      alignPosition += crab;
    }

    alignPosition = (int)Math.Round((double)alignPosition / (double)input.Length);

    foreach (int crab in input)
    {
      for (int i = Math.Abs(crab - alignPosition); i > 0; i--)
      {
        fuelMiddle += i;
      }

      for (int i = Math.Abs(crab - alignPosition - 1); i > 0; i--)
      {
        fuelLeft += i;
      }

      for (int i = Math.Abs(crab - alignPosition + 1); i > 0; i--)
      {
        fuelRight += i;
      }
    }

    fuel = fuelLeft < fuelMiddle ? fuelLeft : fuelMiddle;
    fuel = fuelRight < fuel ? fuelRight : fuel;

    partSubmitter.SubmitPart2(fuel);
  }
}

using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2021.D06;

public class Solver : NumbersSolver
{
  public override void Solve(int[] input, IPartSubmitter partSubmitter)
  {
    long[] fishCounter = new long[256 + 10];

    fishCounter[0] = input.Length;
    foreach (int fish in input)
    {
      fishCounter[fish + 1]++;
    }

    for (int day = 1; day <= 256; day++)
    {
      fishCounter[day + 7] += fishCounter[day];
      fishCounter[day + 9] += fishCounter[day];
      fishCounter[day] += fishCounter[day - 1];

      if (day == 80)
      {
        partSubmitter.SubmitPart1(fishCounter[80]);
      }
    }

    partSubmitter.SubmitPart2(fishCounter[256]);
  }
}

using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver.Templates;

public abstract class NumbersSolver : ISolver<int[]>
{
  public void Parse(string input, IPartSubmitter<int[]> partSubmitter)
  {
    partSubmitter.Submit(input.Split(',').Select((value) => int.Parse(value)).ToArray());
  }

  public abstract void Solve(int[] input, IPartSubmitter partSubmitter);
}

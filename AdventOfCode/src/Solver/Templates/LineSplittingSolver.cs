using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver.Templates;

public abstract class LineSplittingSolver : ISolver<string[]>
{
  public void Parse(string input, IPartSubmitter<string[]> partSubmitter)
  {
    partSubmitter.Submit(input.Split('\n'));
  }

  public abstract void Solve(string[] input, IPartSubmitter partSubmitter);
}

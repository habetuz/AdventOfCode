using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver.Templates;

public abstract class UnmodifingSolver : ISolver<string>
{
  public void Parse(string input, IPartSubmitter<string> partSubmitter)
  {
    partSubmitter.Submit(input);
  }

  public abstract void Solve(string input, IPartSubmitter partSubmitter);
}

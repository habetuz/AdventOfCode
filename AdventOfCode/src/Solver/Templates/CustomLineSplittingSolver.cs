using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver.Templates;

public abstract class CustomLineSplittingSolver<T> : ISolver<T[]>
{
  public void Parse(string input, IPartSubmitter<T[]> partSubmitter)
  {
    partSubmitter.Submit(input.Split('\n').Select(Convert).ToArray());
  }

  public abstract T Convert(string value);

  public abstract void Solve(T[] input, IPartSubmitter partSubmitter);
}

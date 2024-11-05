using AdventOfCode.PartSubmitter;
using AdventOfCode.Utils;

namespace AdventOfCode.Solver.Templates;

public abstract class CustomGridSplittingSolver<T> : ISolver<T[,]>
{
  public void Parse(string input, IPartSubmitter<T[,]> partSubmitter)
  {
    partSubmitter.Submit(Array2D.FromString(input, Convert));
  }

  public abstract T Convert(char value, int x, int y);

  public abstract void Solve(T[,] input, IPartSubmitter partSubmitter);
}

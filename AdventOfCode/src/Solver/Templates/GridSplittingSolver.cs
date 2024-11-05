using AdventOfCode.PartSubmitter;
using AdventOfCode.Utils;

namespace AdventOfCode.Solver.Templates;

public abstract class GridSplittingSolver : ISolver<char[,]>
{
  public void Parse(string input, IPartSubmitter<char[,]> partSubmitter)
  {
    partSubmitter.Submit(Array2D.FromString(input));
  }

  public abstract void Solve(char[,] input, IPartSubmitter partSubmitter);
}

using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D01;

public class Solver : ISolver<int[]>
{
  public void Parse(string input, IPartSubmitter<int[]> partSubmitter)
  {
    string[] lines = input.Split('\n');

    List<int> inputArray = [];

    // Parsing
    for (int i = 0; i < lines.Length; i++)
    {
      int.TryParse(lines[i], out int number);
      inputArray.Add(number);
    }

    partSubmitter.Submit(inputArray.ToArray());
  }

  public void Solve(int[] input, IPartSubmitter partSubmitter)
  {
    int increaseCounter = 0;

    for (int i = 1; i < input.Length; i++)
    {
      if (input[i - 1] < input[i])
      {
        increaseCounter++;
      }
    }

    partSubmitter.SubmitPart1(increaseCounter);

    increaseCounter = 0;

    for (int i = 3; i < input.Length; i++)
    {
      int a = input[i - 3] + input[i - 2] + input[i - 1];
      int b = input[i - 2] + input[i - 1] + input[i];
      if (a < b)
      {
        increaseCounter++;
      }
    }

    partSubmitter.SubmitPart2(increaseCounter);
  }
}

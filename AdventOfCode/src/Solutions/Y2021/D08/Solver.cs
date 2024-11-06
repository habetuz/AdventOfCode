using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AdventOfCode.Solutions.Y2021.D08;

public class Solver : CustomLineSplittingSolver<Display>
{
  public override Display Convert(string value)
  {
    string[] values = value.Split(" | ", StringSplitOptions.RemoveEmptyEntries);
    string[] inputs = values[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] outputs = values[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    return new Display(inputs, outputs);
  }

  public override void Solve(Display[] input, IPartSubmitter partSubmitter)
  {
    int uniqueCounter = 0;

    foreach (Display display in input)
    {
      foreach (string digit in display.Digits)
      {
        if (
          digit.Length == Display.DigitSegments[1].Length
          || digit.Length == Display.DigitSegments[4].Length
          || digit.Length == Display.DigitSegments[7].Length
          || digit.Length == Display.DigitSegments[8].Length
        )
        {
          uniqueCounter++;
        }
      }
    }

    partSubmitter.SubmitPart1(uniqueCounter);

    int sum = 0;
    foreach (Display display in input)
    {
      sum += GetValue(display);
    }
    partSubmitter.SubmitPart2(sum);
  }

  private int GetValue(Display display)
  {
    string[] inputs = new string[10];

    inputs[1] = new string(
      [
        .. display
          .Inputs.Where((input) => input.Length == Display.DigitSegments[1].Length)
          .First()
          .Order(),
      ]
    );
    inputs[4] = new string(
      [
        .. display
          .Inputs.Where((input) => input.Length == Display.DigitSegments[4].Length)
          .First()
          .Order(),
      ]
    );
    inputs[7] = new string(
      [
        .. display
          .Inputs.Where((input) => input.Length == Display.DigitSegments[7].Length)
          .First()
          .Order(),
      ]
    );
    inputs[8] = new string(
      [
        .. display
          .Inputs.Where((input) => input.Length == Display.DigitSegments[8].Length)
          .First()
          .Order(),
      ]
    );

    List<string> sixSegments =
      new(
        display
          .Inputs.Where((input) => input.Length == 6)
          .Select((value) => new string([.. value.Order()]))
      );
    inputs[6] = sixSegments
      .Where((input) => input.Where((value) => inputs[1].Contains(value)).Count() == 1)
      .First();
    sixSegments.Remove(inputs[6]);

    inputs[0] = sixSegments
      .Where((input) => input.Where((value) => inputs[4].Contains(value)).Count() == 3)
      .First();
    sixSegments.Remove(inputs[0]);

    inputs[9] = sixSegments.First();

    List<string> fiveSegments =
      new(
        display
          .Inputs.Where((input) => input.Length == 5)
          .Select((value) => new string([.. value.Order()]))
      );

    inputs[3] = fiveSegments
      .Where((input) => input.Where((value) => inputs[7].Contains(value)).Count() == 3)
      .First();
    fiveSegments.Remove(inputs[3]);

    inputs[2] = fiveSegments
      .Where((input) => input.Where((value) => inputs[4].Contains(value)).Count() == 2)
      .First();
    fiveSegments.Remove(inputs[2]);

    inputs[5] = fiveSegments.First();

    Dictionary<string, int> numbers = [];

    for (int i = 0; i < inputs.Length; i++)
    {
      numbers[inputs[i]] = i;
    }

    int sum = 0;
    for (int i = 0; i < display.Digits.Length; i++)
    {
      sum += numbers[new string([.. display.Digits[i].Order()])] * ((int)Math.Pow(10, display.Digits.Length - i - 1));
    }

    return sum;
  }
}

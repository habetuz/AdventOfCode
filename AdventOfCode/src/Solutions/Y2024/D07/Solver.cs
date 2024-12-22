using System.Reflection.Metadata.Ecma335;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using AdventOfCode.Utils;
using SharpLog;
using YamlDotNet.Core.Tokens;

namespace AdventOfCode.Solutions.Y2024.D07;

public class Solver : CustomLineSplittingSolver<Equation>
{
  public override Equation Convert(string value)
  {
    var split = value.Split(':');
    var result = ulong.Parse(split[0]);
    var values = split[1]
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(ulong.Parse)
      .ToArray();
    return new Equation(result, values);
  }

  public override void Solve(Equation[] input, IPartSubmitter partSubmitter)
  {
    ulong totalSum = 0;
    List<Equation> unsolvable = new(input.Length);

    foreach (Equation equation in input)
    {
      bool solvable = false;

      foreach (char[] permutation in Permutation.Permutate(['+', '*'], equation.Values.Length - 1))
      {
        ulong result = equation
          .Values.Skip(1)
          .Zip(permutation)
          .Aggregate(
            equation.Values[0],
            (acc, value) => value.Second == '*' ? acc * value.First : acc + value.First
          );

        if (result == equation.Result)
        {
          totalSum += result;
          solvable = true;
          break;
        }
      }

      if (!solvable)
      {
        unsolvable.Add(equation);
      }
    }

    partSubmitter.SubmitPart1(totalSum);

    foreach (Equation equation in unsolvable)
    {
      foreach (
        char[] permutation in Permutation.Permutate(['+', '*', '|'], equation.Values.Length - 1)
      )
      {
        ulong result = equation
          .Values.Skip(1)
          .Zip(permutation)
          .Aggregate(
            equation.Values[0],
            (acc, value) =>
              value.Second switch
              {
                '+' => acc + value.First,
                '*' => acc * value.First,
                '|' => (ulong)(acc * Math.Pow(10, Math.Floor(Math.Log10(value.First * 10))))
                  + value.First,
                _ => throw new Exception($"Operator {value.Second} is not handled."),
              }
          );

        if (result == equation.Result)
        {
          totalSum += result;
          break;
        }
      }
    }

    partSubmitter.SubmitPart2(totalSum);
  }
}

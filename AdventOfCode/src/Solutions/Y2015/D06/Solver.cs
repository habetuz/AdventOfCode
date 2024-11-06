using System.Text.RegularExpressions;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2015.D06;

public class Solver : CustomLineSplittingSolver<Instruction>
{
  public override Instruction Convert(string value)
  {
    string pattern = @"^(?<type>.+) (?<AX>\d+),(?<AY>\d+) through (?<BX>\d+),(?<BY>\d+)$";

    Match match = Regex.Match(value, pattern);
    return new Instruction()
    {
      Type = match.Groups["type"].Captures.First().Value switch
      {
        "turn on" => Instruction.InstructionType.ENABLE,
        "turn off" => Instruction.InstructionType.DISABLE,
        "toggle" => Instruction.InstructionType.TOGGLE,
        _ => throw new Exception(
          $"Instruction \"{match.Groups["type"].Captures.First().Value}\" is not handled."
        ),
      },
      Area = (
        (
          int.Parse(match.Groups["AX"].Captures.First().Value),
          int.Parse(match.Groups["AY"].Captures.First().Value)
        ),
        (
          int.Parse(match.Groups["BX"].Captures.First().Value),
          int.Parse(match.Groups["BY"].Captures.First().Value)
        )
      ),
    };
  }

  public override void Solve(Instruction[] input, IPartSubmitter partSubmitter)
  {
    bool[,] lightsOnOff = new bool[1000, 1000];
    int[,] lightsRange = new int[1000, 1000];

    foreach (Instruction instruction in input)
    {
      foreach (var coordinate in instruction.Area.Points())
      {
        lightsOnOff[coordinate.X, coordinate.Y] = instruction.Type switch
        {
          Instruction.InstructionType.ENABLE => true,
          Instruction.InstructionType.DISABLE => false,
          Instruction.InstructionType.TOGGLE => !lightsOnOff[coordinate.X, coordinate.Y],
          _ => throw new Exception("Unexpected instruction type!"),
        };
        lightsRange[coordinate.X, coordinate.Y] += instruction.Type switch
        {
          Instruction.InstructionType.ENABLE => 1,
          Instruction.InstructionType.DISABLE => lightsRange[coordinate.X, coordinate.Y] > 0
            ? -1
            : 0,
          Instruction.InstructionType.TOGGLE => 2,
          _ => throw new Exception("Unexpected instruction type!"),
        };
      }
    }

    int enabledCount = 0;
    foreach (bool light in lightsOnOff)
    {
      if (light)
        enabledCount++;
    }

    partSubmitter.SubmitPart1(enabledCount);

    int brightnessCount = 0;
    foreach (int brightness in lightsRange)
    {
      brightnessCount += brightness;
    }

    partSubmitter.SubmitPart2(brightnessCount);
  }
}

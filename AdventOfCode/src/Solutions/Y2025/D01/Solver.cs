using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using AdventOfCode.Utils;
using Castle.Core.Logging;
using SharpLog;

namespace AdventOfCode.Solutions.Y2025.D01;

public class Solver : CustomLineSplittingSolver<Instruction>
{
  public override Instruction Convert(string value)
  {
    Instruction.Action action = value[0] switch
    {
      'L' => Instruction.Action.Left,
      'R' => Instruction.Action.Right,
      _ => throw new ArgumentException("Invalid instruction action"),
    };
    int valueParsed = int.Parse(value[1..]);
    return new Instruction { InstrAction = action, Value = valueParsed };
  }

  public override void Solve(Instruction[] input, IPartSubmitter partSubmitter)
  {
    int pointerPosition = 50;
    int onZero = 0;
    int passedZero = 0;

    foreach (var instruction in input)
    {
      int previousPosition = pointerPosition;

      switch (instruction.InstrAction)
      {
        case Instruction.Action.Left:
          pointerPosition -= instruction.Value;
          break;
        case Instruction.Action.Right:
          pointerPosition += instruction.Value;
          break;
      }

      // Passing zero check
      if (Math.Sign(previousPosition) != Math.Sign(pointerPosition) && previousPosition != 0 && pointerPosition != 0)
      {
        //Logging.LogDebug($"Pointer passed zero moving from {previousPosition} to {pointerPosition}.");
        passedZero++;
      }

      // Wrap around check
      int wrappedAroundCount = Math.Abs(pointerPosition) / 100;

      pointerPosition = AdventMath.Mod(pointerPosition, 100);

      if (pointerPosition == 0)
      {
        wrappedAroundCount--;
      }

      passedZero += Math.Max(0, wrappedAroundCount);

      //Logging.LogDebug($"Pointer moved {instruction.InstrAction} by {instruction.Value} to position {pointerPosition}.");

      if (pointerPosition == 0)
      {
        onZero++;
        passedZero++;
      }
    }

    Logging.LogInfo($"Pointer landed on zero [yellow]{onZero}[/] times.");
    partSubmitter.SubmitPart1(onZero);
    Logging.LogInfo($"Pointer passed zero [yellow]{passedZero}[/] times.");
    partSubmitter.SubmitPart2(passedZero);
  }
}

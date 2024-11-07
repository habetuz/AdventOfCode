using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D13;

public class Solver : ISolver<(Instruction[] instructions, bool[,] paper)>
{
  public void Parse(
    string input,
    IPartSubmitter<(Instruction[] instructions, bool[,] paper)> partSubmitter
  )
  {
    string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    List<Instruction> instructions = [];
    List<Coordinate> coordinates = [];
    Coordinate maxIndex = (0, 0);

    foreach (string line in lines)
    {
      if (line[0] == 'f')
      {
        instructions.Add(new Instruction(line[11] == 'x' ? Axis.X : Axis.Y, int.Parse(line[13..])));
      }
      else
      {
        coordinates.Add((int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1])));
        if (coordinates.Last().X > maxIndex.X)
        {
          maxIndex.X = coordinates.Last().X;
        }

        if (coordinates.Last().Y > maxIndex.Y)
        {
          maxIndex.Y = coordinates.Last().Y;
        }
      }
    }

    bool[,] paper = new bool[maxIndex.X + 1, maxIndex.Y + 1];

    foreach (var coordinate in coordinates)
    {
      paper[coordinate.X, coordinate.Y] = true;
    }

    partSubmitter.Submit((instructions.ToArray(), paper));
  }

  public void Solve((Instruction[] instructions, bool[,] paper) input, IPartSubmitter partSubmitter)
  {
    Instruction firstInstruction = input.instructions.First();

    bool[,] paper = Fold(input.paper, firstInstruction.Axis, firstInstruction.Index);
    int counter = 0;

    foreach (bool item in paper)
    {
      if (item)
      {
        counter++;
      }
    }

    partSubmitter.SubmitPart1(counter);

    foreach (Instruction instruction in input.instructions)
    {
      paper = Fold(paper, instruction.Axis, instruction.Index);
    }

    Logging.LogInfo($"After the folds the following image appears:");
    Array2D.Print(paper, (value, _, _) => value ? "█" : " ");

    partSubmitter.SubmitPart2("s.o");
  }

  private static bool[,] Fold(bool[,] paper, Axis axis, int index)
  {
    if (axis == Axis.X)
    {
      return FoldX(paper, index);
    }

    return FoldY(paper, index);
  }

  private static bool[,] FoldY(bool[,] paper, int index)
  {
    bool[,] result = new bool[paper.GetLength(0), index];

    for (int y = 0; y < result.GetLength(1); y++)
    {
      for (int x = 0; x < result.GetLength(0); x++)
      {
        result[x, y] = paper[x, y];
      }
    }

    for (int y = index + 1; y < paper.GetLength(1); y++)
    {
      for (int x = 0; x < paper.GetLength(0); x++)
      {
        if (paper[x, y])
        {
          result[x, (2 * index) - y] = true;
        }
      }
    }

    return result;
  }

  private static bool[,] FoldX(bool[,] paper, int index)
  {
    bool[,] result = new bool[index, paper.GetLength(1)];

    for (int y = 0; y < result.GetLength(1); y++)
    {
      for (int x = 0; x < result.GetLength(0); x++)
      {
        result[x, y] = paper[x, y];
      }
    }

    for (int y = 0 + 1; y < paper.GetLength(1); y++)
    {
      for (int x = index; x < paper.GetLength(0); x++)
      {
        if (paper[x, y])
        {
          result[(2 * index) - x, y] = true;
        }
      }
    }

    return result;
  }
}

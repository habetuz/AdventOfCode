using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using Spectre.Console;

namespace AdventOfCode.Solutions.Y2024.D09;

public class Solver : ISolver<LinkedList<char>, (Queue<Solver.Block> Used, List<Solver.Block> Free)>
{
  public void Parse(
    string input,
    IPartSubmitter<LinkedList<char>, (Queue<Block> Used, List<Block> Free)> partSubmitter
  )
  {
    partSubmitter.SubmitPart1(
      new LinkedList<char>(
        input.SelectMany(
          (c, index) =>
            index % 2 == 0
              ? Enumerable.Repeat((char)(index / 2 + '0'), c - '0')
              : Enumerable.Repeat('.', c - '0')
        )
      )
    );

    List<Block> used = new(input.Length / 2 + 1);
    List<Block> free = new(input.Length / 2 + 1);

    uint position = 0;
    int id = 0;

    for (int i = 0; i < input.Length; i++)
    {
      if (i % 2 == 0)
      {
        used.Add(new Block(position, (uint)(input[i] - '0'), id++));
      }
      else
      {
        free.Add(new Block(position, (uint)(input[i] - '0')));
      }

      position += (byte)(input[i] - '0');
    }

    used.Reverse();

    partSubmitter.SubmitPart2((new Queue<Block>(used), free));
  }

  public void Solve(
    LinkedList<char> input1,
    (Queue<Block> Used, List<Block> Free) input2,
    IPartSubmitter partSubmitter
  )
  {
    ulong checksum = 0;

    for (ulong index = 0; input1.First is not null; index++)
    {
      var next = input1.First.Value;
      input1.RemoveFirst();

      while (next == '.' && input1.Last is not null)
      {
        next = input1.Last.Value;
        input1.RemoveLast();
      }

      if (next == '.')
      {
        break;
      }

      checksum += (ulong)(next - '0') * index;
    }

    partSubmitter.SubmitPart1(checksum);

    checksum = 0;
    while (input2.Used.TryDequeue(out var usedSpace))
    {
      var fittingSpace = input2
        .Free.TakeWhile((freeSpace) => freeSpace.Position < usedSpace.Position)
        .FirstOrDefault((freeSpace) => freeSpace.Size >= usedSpace.Size);

      if (fittingSpace is not null)
      {
        usedSpace.Position = fittingSpace.Position;
        fittingSpace.Position += usedSpace.Size;
        fittingSpace.Size -= usedSpace.Size;

        if (fittingSpace.Size == 0)
        {
          input2.Free.Remove(fittingSpace);
        }
      }

      var update =
        (AdventMath.TriangleNumber(usedSpace.Size - 1) + usedSpace.Size * usedSpace.Position)
        * (ulong)(usedSpace.ID);

      checksum +=
        (AdventMath.TriangleNumber(usedSpace.Size - 1) + usedSpace.Size * usedSpace.Position)
        * (ulong)(usedSpace.ID);
    }

    partSubmitter.SubmitPart2(checksum);
  }

  public class Block(uint position, uint size, int id = -1)
  {
    public uint Position { get; set; } = position;
    public uint Size { get; set; } = size;
    public int ID { get; set; } = id;
  }
}

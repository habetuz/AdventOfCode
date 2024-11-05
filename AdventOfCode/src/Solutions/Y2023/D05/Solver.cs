using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D05;

public class Solver : ISolver<Almanac, AlmanacWithRange>
{
  public void Parse(string input, IPartSubmitter<Almanac, AlmanacWithRange> partSubmitter)
  {
    var blocks = input.Split("\n\n");

    var almanac = new Almanac { Maps = new LinkedList<Dictionary<BigRange, long>>() };

    var almanacWithRange = new AlmanacWithRange
    {
      Maps = new LinkedList<Dictionary<BigRange, long>>(),
    };

    var seeds = blocks[0][7..].Split(' ');
    almanac.Seeds = seeds.Select(s => long.Parse(s)).ToArray();
    almanacWithRange.Seeds = new BigRange[seeds.Length / 2];
    for (int i = 0; i < almanac.Seeds.Length; i += 2)
    {
      almanacWithRange.Seeds[i / 2] = new BigRange(
        almanac.Seeds[i],
        almanac.Seeds[i] + almanac.Seeds[i + 1] - 1
      );
    }

    foreach (var block in blocks[1..])
    {
      var map = new Dictionary<BigRange, long>();
      var lines = block.Split('\n', StringSplitOptions.RemoveEmptyEntries);
      foreach (var line in lines[1..])
      {
        var values = line.Split(' ').Select(s => long.Parse(s)).ToArray();
        map.Add(new BigRange(values[1], values[1] + values[2] - 1), values[0] - values[1]);
      }

      almanac.Maps.AddLast(map);
      almanacWithRange.Maps.AddLast(map);
    }

    partSubmitter.SubmitPart1(almanac);
    partSubmitter.SubmitPart2(almanacWithRange);
  }

  public void Solve(Almanac input1, AlmanacWithRange input2, IPartSubmitter partSubmitter)
  {
    var lowestResult = long.MaxValue;
    foreach (long seed in input1.Seeds)
    {
      var result = seed;
      foreach (var map in input1.Maps)
      {
        foreach (var (range, offset) in map)
        {
          if (range.Contains(result))
          {
            result += offset;
            break;
          }
        }
      }

      if (result < lowestResult)
      {
        lowestResult = result;
      }
    }

    partSubmitter.SubmitPart1(lowestResult);

    var inputRanges = new Queue<BigRange>(input2.Seeds);
    var resultRanges = new SortedSet<BigRange>();

    foreach (var map in input2.Maps)
    {
      while (inputRanges.Count > 0)
      {
        BigRange range = inputRanges.Dequeue();
        bool intersected = false;
        foreach (var (mapRange, offset) in map)
        {
          var intersection = range.Intersect(mapRange);
          if (!intersection.HasValue)
          {
            continue;
          }

          intersected = true;
          resultRanges.Add(intersection.Value + offset);
          foreach (var newRange in range - intersection.Value)
          {
            inputRanges.Enqueue(newRange);
          }
          break;
        }

        if (!intersected)
        {
          resultRanges.Add(range);
        }
      }

      // Combine overlapping ranges from resultRanges and save them into inputRanges
      var combinedRanges = CombineRanges(resultRanges);
      inputRanges = new Queue<BigRange>(combinedRanges);
      resultRanges.Clear();
    }

    partSubmitter.SubmitPart2(inputRanges.First().Start);
  }

  private IEnumerable<BigRange> CombineRanges(SortedSet<BigRange> ranges)
  {
    var combinedRanges = new List<BigRange>();
    foreach (var range in ranges)
    {
      if (combinedRanges.Count == 0 || range.Start > combinedRanges.Last().End)
      {
        combinedRanges.Add(range);
      }
      else
      {
        combinedRanges[combinedRanges.Count - 1] = new BigRange(
          combinedRanges.Last().Start,
          Math.Max(combinedRanges.Last().End, range.End)
        );
      }
    }
    return combinedRanges;
  }
}

using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2015.D05;

public class Solver : ISolver<string[]>
{
  private readonly Dictionary<char, char> disallowed = new()
  {
    { 'a', 'b' },
    { 'c', 'd' },
    { 'p', 'q' },
    { 'x', 'y' },
  };

  private readonly char[] vowels = ['a', 'e', 'i', 'o', 'u'];

  public void Parse(string input, IPartSubmitter<string[]> partSubmitter)
  {
    partSubmitter.Submit(input.Split('\n'));
  }

  public void Solve(string[] input, IPartSubmitter partSubmitter)
  {
    partSubmitter.SubmitPart1((from word in input where IsNiceOne(word) select word).Count());
    partSubmitter.SubmitPart2((from word in input where IsNiceTwo(word) select word).Count());
  }

  private bool IsNiceOne(string word)
  {
    byte vowelCount = 0;
    bool doubleChar = false;

    for (int i = 0; i < word.Length; i++)
    {
      if (
        i < word.Length - 1
        && disallowed.TryGetValue(word[i], out char follow)
        && word[i + 1] == follow
      )
      {
        return false;
      }

      if (i < word.Length - 1 && word[i + 1] == word[i])
      {
        doubleChar = true;
      }

      if (vowels.Contains(word[i]))
      {
        vowelCount++;
      }
    }

    return doubleChar && vowelCount >= 3;
  }

  private bool IsNiceTwo(string word)
  {
    bool doubleChar = false;
    bool doublePair = false;
    LinkedList<string> pairs = new();

    for (int i = 0; i < word.Length - 1 && !(doubleChar && doublePair); i++)
    {
      if (i < word.Length - 2 && word[i] == word[i + 2])
      {
        doubleChar = true;
      }

      string pair = word[i..(i + 2)];
      if (pairs.Contains(pair))
      {
        if (pair[0] != pair[1] || pair[0] != word[i - 1] || (i > 2 && word[i - 2] == pair[0]))
        {
          doublePair = true;
        }
      }
      else
      {
        pairs.AddLast(pair);
      }
    }

    return doubleChar && doublePair;
  }
}

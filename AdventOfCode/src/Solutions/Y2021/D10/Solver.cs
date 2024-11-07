using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2021.D10;

public class Solver : LineSplittingSolver
{
  public override void Solve(string[] input, IPartSubmitter partSubmitter)
  {
    int errorScore = 0;

    foreach (string line in input)
    {
      try
      {
        WrongSyntaxAnalysis(line);
      }
      catch (WrongSyntaxException ex)
      {
        errorScore += WrongCharacterScore[ex.Character];
      }
      catch (MissingSyntaxException) { }
    }

    partSubmitter.SubmitPart1(errorScore);

    List<long> errorScores = [];

    foreach (string line in input)
    {
      try
      {
        string missingString = MissingSyntaxAnalysis(line);
        long score = 0;
        foreach (char ch in missingString)
        {
          score *= 5;
          score += MissingCharacterScore[ch];
        }

        errorScores.Add(score);
      }
      catch (WrongSyntaxException) { }
    }

    errorScores.Sort();
    long middleScore = errorScores[errorScores.Count / 2];

    partSubmitter.SubmitPart2(middleScore);
  }

  private static readonly Dictionary<char, int> WrongCharacterScore = new Dictionary<char, int>()
  {
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
  };

  private static readonly Dictionary<char, int> MissingCharacterScore = new Dictionary<char, int>()
  {
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 },
  };

  private static readonly Dictionary<char, char> BracketPairs = new Dictionary<char, char>()
  {
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' },
  };

  private static void WrongSyntaxAnalysis(string line)
  {
    WrongSyntaxAnalysis(line, 0);
  }

  private static int WrongSyntaxAnalysis(string line, int index)
  {
    char openingBracket = line[index];
    char closingBracket = BracketPairs[openingBracket];

    for (int i = index + 1; i < line.Length; i++)
    {
      if (BracketPairs.ContainsKey(line[i]))
      {
        i = WrongSyntaxAnalysis(line, i);
      }
      else if (line[i] == closingBracket)
      {
        return i;
      }
      else
      {
        throw new WrongSyntaxException(line[i]);
      }
    }

    throw new MissingSyntaxException(openingBracket);
  }

  private static string MissingSyntaxAnalysis(string line)
  {
    string completionString = string.Empty;
    for (int i = 0; i < line.Length; i++)
    {
      (completionString, i) = MissingSyntaxAnalysis(line, string.Empty, i);
    }

    return completionString;
  }

  private static (string, int) MissingSyntaxAnalysis(
    string line,
    string completionString,
    int index
  )
  {
    char openingBracket = line[index];
    char closingBracket = BracketPairs[openingBracket];

    for (int i = index + 1; i < line.Length; i++)
    {
      if (BracketPairs.ContainsKey(line[i]))
      {
        (completionString, i) = MissingSyntaxAnalysis(line, completionString, i);
      }
      else if (line[i] == closingBracket)
      {
        return (completionString, i);
      }
      else
      {
        throw new WrongSyntaxException(line[i]);
      }
    }

    completionString += closingBracket;
    return (completionString, line.Length);
  }

  private class SyntaxException(char character) : Exception
  {
    public char Character { get; } = character;
  }

  private class WrongSyntaxException(char character) : SyntaxException(character) { }

  private class MissingSyntaxException(char character) : SyntaxException(character) { }
}

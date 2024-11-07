using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D14;

public class Solver : ISolver<(Dictionary<string, char> rules, string template)>
{
  public void Parse(
    string input,
    IPartSubmitter<(Dictionary<string, char> rules, string template)> partSubmitter
  )
  {
    string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

    Dictionary<string, char> rules = [];

    for (int i = 1; i < lines.Length; i++)
    {
      string[] rule = lines[i].Split(" -> ");
      rules.Add(rule[0], rule[1][0]);
    }

    partSubmitter.Submit((rules, lines[0]));
  }

  public void Solve(
    (Dictionary<string, char> rules, string template) input,
    IPartSubmitter partSubmitter
  )
  {
    partSubmitter.SubmitPart1(Polymerize(input.rules, input.template, 10));
    partSubmitter.SubmitPart2(Polymerize(input.rules, input.template, 40));
  }

  private static long Polymerize(Dictionary<string, char> rules, string polymer, int steps)
  {
    Dictionary<string, (string, string)> polymerizationResults = [];
    Dictionary<string, long> moleculeCounter = [];
    Dictionary<char, long> elementCounter = [];

    // Fill molecule and element counter and Setup polymerization results
    foreach (string molecule in rules.Keys)
    {
      moleculeCounter[molecule] = 0;
      elementCounter[molecule[0]] = 0;

      polymerizationResults[molecule] = (
        molecule[0].ToString() + rules[molecule],
        rules[molecule].ToString() + molecule[1]
      );
    }

    // Setup element counter
    foreach (char element in polymer)
    {
      elementCounter[element]++;
    }

    // Setup molecule counter
    for (int i = 0; i < polymer.Length - 1; i++)
    {
      moleculeCounter[polymer.Substring(i, 2)]++;
    }

    for (int i = 0; i < steps; i++)
    {
      Dictionary<string, long> tempMoleculeCounter = [];

      foreach (string molecule in rules.Keys)
      {
        if (!tempMoleculeCounter.ContainsKey(polymerizationResults[molecule].Item1))
        {
          tempMoleculeCounter[polymerizationResults[molecule].Item1] = 0;
        }

        if (!tempMoleculeCounter.ContainsKey(polymerizationResults[molecule].Item2))
        {
          tempMoleculeCounter[polymerizationResults[molecule].Item2] = 0;
        }

        tempMoleculeCounter[polymerizationResults[molecule].Item1] += moleculeCounter[molecule];
        tempMoleculeCounter[polymerizationResults[molecule].Item2] += moleculeCounter[molecule];

        elementCounter[rules[molecule]] += moleculeCounter[molecule];
      }

      foreach (string molecule in rules.Keys)
      {
        if (tempMoleculeCounter.TryGetValue(molecule, out long value))
        {
          moleculeCounter[molecule] = value;
        }
        else
        {
          moleculeCounter[molecule] = 0;
        }
      }
    }

    return elementCounter.Values.Max() - elementCounter.Values.Min();
  }
}

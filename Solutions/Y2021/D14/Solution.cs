namespace AdventOfCode.Solutions.Y2021.D14
{
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Common;

    internal class Solution : Solution<(Dictionary<string, char>, string)>
    {
        internal override (object, string) Puzzle1((Dictionary<string, char>, string) input)
        {
            long solution = this.Polimerize(input.Item1, input.Item2, 10);

            return (solution.ToString(), $"The solution is {solution}!");
        }

        internal override (object, string) Puzzle2((Dictionary<string, char>, string) input)
        {
            long solution = this.Polimerize(input.Item1, input.Item2, 40);
            SharpLog.Logging.LogDebug($"The solution is {solution}!");

            return (solution.ToString(), $"The solution is {solution}!");
        }

        private long Polimerize(Dictionary<string, char> rules, string polymer, int steps)
        {
            Dictionary<string, (string, string)> polymerizationResults = new Dictionary<string, (string, string)>();
            Dictionary<string, long> moleculeCounter = new Dictionary<string, long>();
            Dictionary<char, long> elementCounter = new Dictionary<char, long>();

            // Fill molecule and element counter and Setup polymerization results
            foreach (string molecule in rules.Keys)
            {
                moleculeCounter[molecule] = 0;
                elementCounter[molecule[0]] = 0;

                polymerizationResults[molecule] = (molecule[0].ToString() + rules[molecule], rules[molecule].ToString() + molecule[1]);
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
                Dictionary<string, long> tempMoleculeCounter = new Dictionary<string, long>();

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
                    if (tempMoleculeCounter.ContainsKey(molecule))
                    {
                        moleculeCounter[molecule] = tempMoleculeCounter[molecule];
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
}

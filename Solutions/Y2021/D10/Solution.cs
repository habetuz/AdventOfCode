namespace AdventOfCode.Solutions.Y2021.D10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<string[]>
    {
        private static readonly Dictionary<char, int> s_wrongCharacterScore = new Dictionary<char, int>()
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137},
        };

        private static readonly Dictionary<char, int> s_missingCharacterScore = new Dictionary<char, int>()
        {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4},
        };

        private static readonly Dictionary<char, char> s_bracketPairs = new Dictionary<char, char>()
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'},
        };

        internal override string Puzzle1(string[] input)
        {
            int errorScore = 0;

            foreach (string line in input)
            {
                try
                {
                    this.WrongSystanxAnalysis(line);
                }
                catch (WrongSyntaxExcpetion ex)
                {
                    errorScore += s_wrongCharacterScore[ex.Character];
                }
                catch (MissingSyntaxExcpetion)
                {
                }
            }

            SharpLog.Logging.LogDebug($"The total syntax error score is {errorScore}!");
            return errorScore.ToString();
        }

        internal override string Puzzle2(string[] input)
        {
            List<long> errorScores = new List<long>();

            foreach (string line in input)
            {
                try
                {
                    string missingString = this.MissingSyntaxAnalysis(line);
                    long score = 0;
                    foreach (char ch in missingString)
                    {
                        score *= 5;
                        score += s_missingCharacterScore[ch];
                    }

                    errorScores.Add(score);
                }
                catch (WrongSyntaxExcpetion)
                {
                }
            }

            errorScores.Sort();
            long middleScore = errorScores[errorScores.Count / 2];

            SharpLog.Logging.LogDebug($"The middle error score is {middleScore}!");
            return middleScore.ToString();
        }

        private void WrongSystanxAnalysis(string line)
        {
            this.WrongSyntaxAnalysis(line, 0);
        }

        private int WrongSyntaxAnalysis(string line, int index)
        {
            char openingBracket = line[index];
            char closingBracket = s_bracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (s_bracketPairs.ContainsKey(line[i]))
                {
                    i = this.WrongSyntaxAnalysis(line, i);
                }
                else if (line[i] == closingBracket)
                {
                    return i;
                }
                else
                {
                    throw new WrongSyntaxExcpetion(line[i]);
                }
            }

            throw new MissingSyntaxExcpetion(openingBracket);
        }

        private string MissingSyntaxAnalysis(string line)
        {
            string completionString = string.Empty;
            for (int i = 0; i < line.Length; i++)
            {
                (completionString, i) = this.MissingSyntaxAnalysis(line, string.Empty, i);
            }

            return completionString;
        }

        private (string, int) MissingSyntaxAnalysis(string line, string completionString, int index)
        {
            char openingBracket = line[index];
            char closingBracket = s_bracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (s_bracketPairs.ContainsKey(line[i]))
                {
                    (completionString, i) = this.MissingSyntaxAnalysis(line, completionString, i);
                }
                else if (line[i] == closingBracket)
                {
                    return (completionString, i);
                }
                else
                {
                    throw new WrongSyntaxExcpetion(line[i]);
                }
            }

            completionString += closingBracket;
            return (completionString, line.Length);
        }

        private class SyntaxExcpetion : Exception
        {
            public SyntaxExcpetion(char character)
            {
                this.Character = character;
            }

            public char Character { get; }
        }

        private class WrongSyntaxExcpetion : SyntaxExcpetion
        {
            public WrongSyntaxExcpetion(char character) : base(character)
            {
            }
        }

        private class MissingSyntaxExcpetion : SyntaxExcpetion
        {
            public MissingSyntaxExcpetion(char character) : base(character)
            {
            }
        }
    }
}

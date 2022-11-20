﻿namespace AdventOfCode.Solutions.Y2021.D10
{
    using AdventOfCode.Common;
    using System;
    using System.Collections.Generic;

    internal class Solution : Solution<string[]>
    {
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

        internal override (object, string) Puzzle1(string[] input)
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
                    errorScore += WrongCharacterScore[ex.Character];
                }
                catch (MissingSyntaxExcpetion)
                {
                }
            }

            return (errorScore.ToString(), $"The total syntax error score is {errorScore}!");
        }

        internal override (object, string) Puzzle2(string[] input)
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
                        score += MissingCharacterScore[ch];
                    }

                    errorScores.Add(score);
                }
                catch (WrongSyntaxExcpetion)
                {
                }
            }

            errorScores.Sort();
            long middleScore = errorScores[errorScores.Count / 2];

            return (middleScore.ToString(), $"The middle error score is {middleScore}!");
        }

        private void WrongSystanxAnalysis(string line)
        {
            this.WrongSyntaxAnalysis(line, 0);
        }

        private int WrongSyntaxAnalysis(string line, int index)
        {
            char openingBracket = line[index];
            char closingBracket = BracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (BracketPairs.ContainsKey(line[i]))
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
            char closingBracket = BracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (BracketPairs.ContainsKey(line[i]))
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
            public WrongSyntaxExcpetion(char character)
                : base(character)
            {
            }
        }

        private class MissingSyntaxExcpetion : SyntaxExcpetion
        {
            public MissingSyntaxExcpetion(char character)
                : base(character)
            {
            }
        }
    }
}

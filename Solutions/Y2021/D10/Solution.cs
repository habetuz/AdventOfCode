using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D10
{
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
                    WrongSystanxAnalysis(line);
                }
                catch (WrongSyntaxExcpetion ex)
                {
                    errorScore += s_wrongCharacterScore[ex.Character];
                }
                catch (MissingSyntaxExcpetion) { }
                
            }

            s_logger.Log($"The total syntax error score is {errorScore}!", LogType.Info);
            return errorScore.ToString();
        }

        internal override string Puzzle2(string[] input)
        {
            List<long> errorScores = new List<long>();

            foreach (string line in input)
            {
                try
                {
                    string missingString = MissingSyntaxAnalysis(line);
                    long score = 0;
                    foreach (char ch in missingString)
                    {
                        score *= 5;
                        score += s_missingCharacterScore[ch];
                    }
                    errorScores.Add(score);
                }
                catch (WrongSyntaxExcpetion) { }
                
                
            }

            errorScores.Sort();
            long middleScore = errorScores[errorScores.Count / 2];

            s_logger.Log($"The middle error score is {middleScore}!", LogType.Info);
            return middleScore.ToString();
        }

        private void WrongSystanxAnalysis(string line)
        {
            WrongSyntaxAnalysis(line, 0);
        }

        private int WrongSyntaxAnalysis(string line, int index)
        {
            char openingBracket = line[index];
            char closingBracket = s_bracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (s_bracketPairs.ContainsKey(line[i]))
                {
                    i = WrongSyntaxAnalysis(line, i);
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
                (completionString, i) = MissingSyntaxAnalysis(line, string.Empty, i);
            }
            return completionString;
        }

        private (string, int) MissingSyntaxAnalysis(string line,string completionString, int index)
        {
            char openingBracket = line[index];
            char closingBracket = s_bracketPairs[openingBracket];

            for (int i = index + 1; i < line.Length; i++)
            {
                if (s_bracketPairs.ContainsKey(line[i]))
                {
                    (completionString, i) =  MissingSyntaxAnalysis(line, completionString, i);
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
            public char Character { get; }

            public SyntaxExcpetion(char character)
            {
                Character = character;
            }
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

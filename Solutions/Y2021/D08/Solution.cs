using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D08
{
    internal class Solution : Solution<Display[]>
    {
        internal override string Puzzle1 (Display[] input)
        {
            int uniqueCounter = 0;

            foreach (Display display in input)
            {
                foreach (string digit in display.Outputs)
                {
                    if (
                        digit.Length == Display.s_digitSegments[1].Length || 
                        digit.Length == Display.s_digitSegments[4].Length || 
                        digit.Length == Display.s_digitSegments[7].Length || 
                        digit.Length == Display.s_digitSegments[8].Length)
                    {
                        uniqueCounter++;
                    }
                }

                
            }

            s_logger.Log($"There are {uniqueCounter} unique digits in the output!", LogType.Info);

            return uniqueCounter.ToString();
        }

        internal override string Puzzle2 (Display[] input)
        {
            int values = 0;

            foreach (Display display in input)
            {
                Rule1(display, 1);
                Rule1(display, 4);
                Rule1(display, 7);

                // Do rule 2 and 3 until they bove have nothing to change
                bool changed = false;
                do
                {
                    bool changed1 = Rule2(display);
                    bool changed2 = Rule3(display);
                    changed = changed1 || changed2;
                } while (changed);

                foreach (string test in display.Inputs)
                {
                    if (test.Length == 1 || test.Length == 4 || test.Length == 7 || test.Length == 8)
                    {
                        continue;
                    }

                    Rule4(display, test);
                    if (display.PossibleWiring.All(pair => { return pair.Value.Length == 1; }))
                    {
                        break;
                    }
                }

                values += display.Value;

                
            }

            s_logger.Log($"The displays add up to {values}!", LogType.Info);

            return values.ToString();
        }

        /// <summary>
        /// Digits 1, 4, 7 have a unique number of segments --> Can only have the connections of those numbers.
        /// </summary>
        private void Rule1 (Display display, short num)
        {
            foreach (string input in display.Inputs)
            {
                if (input.Length == Display.s_digitSegments[num].Length)
                {
                    foreach (char c in input)
                    {
                        // Intersection of possible wiring of each char and the segments that the unique num has.
                        display.PossibleWiring[c] = display.PossibleWiring[c].Intersect(Display.s_digitSegments[num]).ToArray();
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Two segment with the two same possible outcomes (a -> c,f and b -> c, f) must be the only ones with those two outcome.
        /// </summary>
        /// <returns>
        /// wether there were any pairs to apply.
        /// </returns>
        private bool Rule2 (Display display)
        {
            bool changed = false;
            List<char[]> charPairs = new List<char[]>();

            // Find pairs
            foreach (char[] wiring in display.PossibleWiring.Values)
            {
                if (wiring.Length == 2 && !charPairs.Any(pair => { return pair.SequenceEqual(wiring); }))
                {
                    charPairs.Add(wiring);
                }
            }

            // Apply pairs
            foreach (char[] pair in charPairs)
            {
                foreach (char key in display.PossibleWiring.Keys.ToArray() )
                {
                    List<char> values = display.PossibleWiring[key].ToList();
                    if (values.Count <= 2) continue;
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (pair.Contains(values[i]))
                        {
                            values.RemoveAt(i);
                            changed = true;
                            i--;
                        }
                    }
                    display.PossibleWiring[key] = values.ToArray();
                }
            }

            return changed;
        }

        /// <summary>
        /// One segment with only one outcome (a -> c) must be the only one with this outcome.
        /// </summary>
        /// <returns>
        /// wether there were singles to apply.
        /// </returns>
        private bool Rule3 (Display display)
        {
            bool changed = false;
            List<char> singles = new List<char>();

            // Find singles
            foreach (char[] wiring in display.PossibleWiring.Values)
            {
                if (wiring.Length == 1 && !singles.Contains(wiring[0]))
                {
                    singles.Add(wiring[0]);
                }
            }

            // Apply singles
            foreach (char single in singles)
            {
                foreach (char key in display.PossibleWiring.Keys.ToArray())
                {
                    List<char> values = display.PossibleWiring[key].ToList();
                    if (values.Count <= 1) continue;
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (values.Contains(single))
                        {
                            values.RemoveAt(i);
                            changed = true;
                            break;
                        }
                    }
                    display.PossibleWiring[key] = values.ToArray();
                }
            }

            return changed;
        }

        private void Rule4 (Display display, string test)
        {
            Dictionary<char, char[]> possibleWiring = new Dictionary<char, char[]> ();

            foreach (Dictionary<char, char> wiring in new WiringEnumerator(display.PossibleWiring))
            {
                if (Display.IsDigit(wiring, test))
                {
                    foreach (KeyValuePair<char, char> pair in wiring)
                    {
                        char[] configuration;
                        try
                        {
                            configuration = possibleWiring[pair.Key];
                            if (!configuration.Contains(pair.Value))
                            {
                                Array.Resize(ref configuration, configuration.Length + 1);
                                configuration[configuration.Length - 1] = pair.Value;
                            }
                        } catch (KeyNotFoundException)
                        {
                            configuration = new char[] { pair.Value };
                        }

                        possibleWiring[pair.Key] = configuration;
                    }
                }
            }

            display.PossibleWiring = possibleWiring;
        }

        private class WiringEnumerator : IEnumerable<Dictionary<char, char>>
        {
            private readonly Dictionary<char[], char[]> _configurations = new Dictionary<char[], char[]> ();
            private readonly Dictionary<char, char> _templateDictionary = new Dictionary<char, char> ();

            public WiringEnumerator(Dictionary<char, char[]> wiring)
            {
                List<char> skip = new List<char> ();
                for (char i = 'a'; i <= 'g'; i++)
                {
                    if (skip.Contains(i)) continue;
                    if (wiring[i].Length == 2)
                    {
                        for (char j = (char)(i + 1); j <= 'g'; j++)
                        {
                            if (skip.Contains(j)) continue;
                            if (wiring[j].All(c => wiring[i].Contains(c)))
                            {
                                _configurations.Add(wiring[i], new char[] { i, j });
                                skip.Add(j);
                                break;
                            }
                        }
                    } 
                    else if (wiring[i].Length == 1)
                    {
                        _templateDictionary.Add(i, wiring[i][0]);
                    }
                }
            }

            private WiringEnumerator(Dictionary<char, char[]> wiring, bool test)
            {
                List<char> skip = new List<char>();
                for (char i = 'a'; i <= 'f'; i++)
                {
                    if (skip.Contains(i)) continue;
                    if (wiring[i].Length == 2)
                    {
                        for (char j = (char)(i + 1); j <= 'g'; j++)
                        {
                            if (skip.Contains(j)) continue;
                            if (wiring[j].All(c => wiring[i].Contains(c)))
                            {
                                _configurations.Add(wiring[i], new char[] { i, j });
                                skip.Add(j);
                                break;
                            }
                        }
                    }
                    else if (wiring[i].Length == 1)
                    {
                        _templateDictionary.Add(i, wiring[i][0]);
                    }
                }
            }

            public IEnumerator<Dictionary<char, char>> GetEnumerator()
            {
                for (int i = 0; i < Math.Pow(2, _configurations.Count + 1); i++)
                {
                    string binaryString = Convert.ToString(i, 2);
                    binaryString = binaryString.PadLeft(_configurations.Count, '0');
                    List<KeyValuePair<char[], char[]>> configurations = _configurations.ToList();
                    for (int j = 0; j < _configurations.Count; j++)
                    {
                        _templateDictionary[configurations[j].Value[0]] = configurations[j].Key[int.Parse(binaryString[j].ToString())];
                        _templateDictionary[configurations[j].Value[1]] = configurations[j].Key[Math.Abs(int.Parse(binaryString[j].ToString()) - 1)];
                    }

                    yield return _templateDictionary;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}

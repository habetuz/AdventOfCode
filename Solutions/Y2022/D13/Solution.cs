namespace AdventOfCode.Solutions.Y2022.D13
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Solution : Solution<string[]>
    {
        internal override (object clipboard, string message) Puzzle1(string[] input)
        {
            int sum = 0;

            for (int i = 0; i < input.Length / 2; i++)
            {
                var left = (string)input[i * 2].Clone();
                var right = (string)input[(i * 2) + 1].Clone();

                for (int j = 0; j < left.Length; j++)
                {
                    if (left[j] == right[j])
                    {
                        continue;
                    }
                    else if (left[j] >= '0' && left[j] <= '9' && right[j] >= '0' && right[j] <= '9')
                    {
                        // Account for 10s
                        var leftNum = left[j + 1] == '0' ? 10 : left[j] - '0';
                        var rightNum = right[j + 1] == '0' ? 10 : right[j] - '0';
                        if (leftNum < rightNum)
                        {
                            sum += i + 1;
                        }

                        break;
                    }
                    else if (left[j] == ']')
                    {
                        sum += i + 1;
                        break;
                    }
                    else if (right[j] == ']')
                    {
                        break;
                    }
                    else if (left[j] == '[' && right[j] >= '0' && right[j] <= '9')
                    {
                        // Account for 10s
                        var num = right[j + 1] == '0' ? 10 : right[j] - '0';
                        right = right.Remove(j, num == 10 ? 2 : 1).Insert(j, $"[{num}]");
                        continue;
                    }
                    else if (right[j] == '[' && left[j] >= '0' && left[j] <= '9')
                    {
                        // Account for 10s
                        var num = left[j + 1] == '0' ? 10 : left[j] - '0';
                        left = left.Remove(j, num == 10 ? 2 : 1).Insert(j, $"[{num}]");
                        continue;
                    }
                }
            }

            return (sum, $"The sum of the indices of the correct pairs is [yellow]{sum}[/]!");
        }

        internal override (object clipboard, string message) Puzzle2(string[] input)
        {
            var signalSubsets = new List<List<string>>();

            for (int i = 0; i < input.Length; i++)
            {
                signalSubsets.Add(new List<string>() { input[i] });
            }

            signalSubsets.Add(new List<string>() { "[[2]]" });
            signalSubsets.Add(new List<string>() { "[[6]]" });

            // Lets do kind of merge sort!
            while (signalSubsets.Count > 1)
            {
                for (int i = 0; i < signalSubsets.Count; i++)
                {
                    var subsetA = signalSubsets[i];

                    // Search a group matching the most left or most right signal.
                    var leftA = subsetA[0];
                    var rightA = subsetA[subsetA.Count - 1];
                    for (int j = i + 1; j < signalSubsets.Count; j++)
                    {
                        var subsetB = signalSubsets[j];
                        var leftB = subsetB[0];
                        var rightB = subsetB[subsetB.Count - 1];

                        if (this.IsOrdered(leftA, rightB))
                        {
                            signalSubsets.RemoveAt(j);
                            signalSubsets.RemoveAt(i);
                            subsetB.AddRange(subsetA);
                            signalSubsets.Insert(0, subsetB);
                        }
                        else if (this.IsOrdered(leftB, rightA))
                        {
                            signalSubsets.RemoveAt(j);
                            signalSubsets.RemoveAt(i);
                            subsetA.AddRange(subsetB);
                            signalSubsets.Insert(0, subsetA);
                        }
                    }
                }
            }

            var signal = signalSubsets[0];
            int decoderKey = 1;

            for (int i = 0; i < signal.Count; i++)
            {
                if (signal[i] == "[[2]]" || signal[i] == "[[6]]")
                {
                    decoderKey *= i + 1;
                }
            }

            return (decoderKey, $"The decoder key is [yellow]{decoderKey}[/]!");
        }

        private bool IsOrdered(string left, string right)
        {
            left = (string)left.Clone();
            right = (string)right.Clone();

            for (int j = 0; j < left.Length; j++)
            {
                if (left[j] >= '0' && left[j] <= '9' && right[j] >= '0' && right[j] <= '9')
                {
                    // Account for 10s
                    var leftNum = left[j + 1] == '0' ? 10 : left[j] - '0';
                    var rightNum = right[j + 1] == '0' ? 10 : right[j] - '0';
                    if (leftNum < rightNum)
                    {
                        return true;
                    }

                    return false;
                }
                else if (left[j] == ']')
                {
                    return true;
                }
                else if (right[j] == ']')
                {
                    return false;
                }
                else if (left[j] == '[' && right[j] >= '0' && right[j] <= '9')
                {
                    // Account for 10s
                    var num = right[j + 1] == '0' ? 10 : right[j] - '0';
                    right = right.Remove(j, num == 10 ? 2 : 1).Insert(j, $"[{num}]");
                    j--;
                    continue;
                }
                else if (right[j] == '[' && left[j] >= '0' && left[j] <= '9')
                {
                    // Account for 10s
                    var num = left[j + 1] == '0' ? 10 : left[j] - '0';
                    left = left.Remove(j, num == 10 ? 2 : 1).Insert(j, $"[{num}]");
                    continue;
                }
            }

            throw new System.ArgumentException("The provided strings are invalide to compare!");
        }
    }
}

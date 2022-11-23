// <copyright file="Tools.cs" company="Marvin Fuchs">

namespace AdventOfCode.Common
{
    using SharpLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Tools
    {
        private static Dictionary<string, int> lineNums = new Dictionary<string, int>();

        internal static string Formatt<T>(T[,] array, string spacing = "", bool alignLeft = true)
        {
            var output = $">> Array with dimentions {array.GetLength(0)}x{array.GetLength(1)}\n";

            int maxLength = 0;
            foreach (var item in array)
            {
                if (item.ToString().Length > maxLength)
                {
                    maxLength = item.ToString().Length;
                }
            }

            for (int y = 0; y < array.GetLength(1); y++)
            {
                string line = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    line += alignLeft ? array[x, y].ToString().PadRight(maxLength) : array[x, y].ToString().PadLeft(maxLength);
                    line += spacing;
                }

                output += $"{line} - {y}\n";
            }

            return output;
        }

        internal static string Formatt(bool[,] array)
        {
            var output = $">> Array with dimentions {array.GetLength(0)}x{array.GetLength(1)}\n";

            for (int y = 0; y < array.GetLength(1); y++)
            {
                string line = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    line += array[x, y] ? "█" : "░";
                }

                output += $"{line} - {y}\n";
            }

            return output;
        }

        internal static string Formatt<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool alignKeyLeft = true, bool alignValueLeft = true)
        {
            var output = $">> Dictionary with {dictionary.Count} entries.\n";

            int maxLengthKey = 0;
            int maxLengthValue = 0;
            foreach (var item in dictionary)
            {
                if (item.Key.ToString().Length > maxLengthKey)
                {
                    maxLengthKey = item.Key.ToString().Length;
                }

                if (item.Value.ToString().Length > maxLengthValue)
                {
                    maxLengthValue = item.Value.ToString().Length;
                }
            }

            output += $"┌{new string('─', maxLengthKey + 2)}┬{new string('─', maxLengthValue + 2)}┐\n";

            var pairs = dictionary.ToArray();
            for (int i = 0; i < dictionary.Count; i++)
            {
                var item = pairs[i];
                output += $"│ {(alignKeyLeft ? item.Key.ToString().PadRight(maxLengthKey) : item.Key.ToString().PadLeft(maxLengthKey))} | {(alignValueLeft ? item.Value.ToString().PadRight(maxLengthValue) : item.Value.ToString().PadLeft(maxLengthValue))} |\n";
                if (i < dictionary.Count - 1)
                {
                    output += $"├{new string('─', maxLengthKey + 2)}┼{new string('─', maxLengthValue + 2)}┤\n";
                }
            }

            output += $"└{new string('─', maxLengthKey + 2)}┴{new string('─', maxLengthValue + 2)}┘\n";

            return output;
        }

        internal static int[] InvertBinary(int[] array)
        {
            array = (int[])array.Clone();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (array[i] - 1) * -1;
            }

            return array;
        }

        internal static int[][] InvertBinary(int[][] array)
        {
            array = (int[][])array.Clone();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = InvertBinary(array[i]);
            }

            return array;
        }

        internal static int BinaryIntArrayToDecInt(int[] array)
        {
            string binaryString = string.Empty;

            foreach (int bit in array)
            {
                if (bit == 0)
                {
                    binaryString += "0";
                }
                else
                {
                    binaryString += "1";
                }
            }

            return Convert.ToInt32(binaryString, 2);
        }

        internal static int Factorial(int num)
        {
            int value = 1;
            for (; num > 1; num--)
            {
                value *= num;
            }

            return value;
        }
        internal static void RewriteLine()
        {
            Console.CursorTop -= 1;
            Console.WriteLine(new string(' ', Console.BufferWidth));
            Console.CursorTop -= 1;
        }
    }
}

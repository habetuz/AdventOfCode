// <copyright file="Tools.cs" company="Marvin Fuchs">

namespace AdventOfCode.Common
{
    using System;
    using SharpLog;

    internal class Tools
    {
        internal static void Print2D(int[,] array)
        {
            Logging.LogDebug($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    str += array[x, y];
                }

                Logging.LogDebug(str + " - " + y);
            }
        }

        internal static void Print2D(bool[,] array)
        {
            Logging.LogDebug($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    if (array[x, y])
                    {
                        str += "#";
                    }
                    else
                    {
                        str += ".";
                    }
                }

                Logging.LogDebug(str + " - " + y);
            }
        }

        internal static void Print2D(char[,] array)
        {
            Logging.LogDebug($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    str += array[x, y];
                }

                Logging.LogDebug(str + " - " + y);
            }
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
    }
}

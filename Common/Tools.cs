using SharpLog;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Common
{
    internal class Tools
    {
        private static readonly Logger s_logger = new Logger()
        {
            Ident = "Tools",
            LogDebug = true,
        };

        internal static void Print2D(int[,] array)
        {
            s_logger.Log($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    str += array[x, y];
                }

                s_logger.Log(str + " - " + y);
            }
        }

        internal static void Print2D(bool[,] array)
        {
            s_logger.Log($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    if (array[x, y]) str += "#";
                    else             str += ".";
                }

                s_logger.Log(str + " - " + y);
            }
        }

        internal static void Print2D(char[,] array)
        {
            s_logger.Log($"----- Printing array with dimentions {array.GetLength(0)}x{array.GetLength(1)} -----");
            for (int y = 0; y < array.GetLength(1); y++)
            {
                string str = string.Empty;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    str += array[x, y];
                }

                s_logger.Log(str + " - " + y);
            }
        }

        internal static int[] InvertBinary(int[] array)
        {
            array = (int[]) array.Clone();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (array[i] - 1) * -1;
            }

            return array;
        }

        internal static int[][] InvertBinary(int[][] array)
        {
            array = (int[][]) array.Clone();

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
            for ( ; num > 1; num--)
            {
                value *= num;
            }
            return value;
        }
    }
}

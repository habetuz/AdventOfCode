using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D03;

public class Solver : ISolver<int[][]>
{
    public void Parse(string input, IPartSubmitter<int[][]> partSubmitter)
    {
        // Split file into lines
        string[] lines = input.Split('\n');

        // Parsing to integer
        List<int[]> inputArray = new List<int[]>();
        for (int i = 0; i < lines.Length - 1; i++)
        {
            int[] bits = new int[lines[0].Length];

            for (int bit = 0; bit < lines[0].Length; bit++)
            {
                bits[bit] = lines[i][bit] - '0';
                //Logging.LogDebug(string.Format("{2}:{3} | From {0} to {1}", lines[i][bit], bits[bit], i, bit));
            }

            inputArray.Add(bits);
        }

        partSubmitter.Submit(inputArray.ToArray());
    }

    public void Solve(int[][] input, IPartSubmitter partSubmitter)
    {
        string gammaRate = string.Empty;
        string epsilonRate = string.Empty;
        for (int x = 0; x < input[0].Length; x++)
        {
            int counter = 0;
            int counter0 = 0;
            for (int y = 0; y < input.Length; y++)
            {
                counter += input[y][x];
                counter0 += (input[y][x] - 1) * -1;
            }

            if (counter > input.Length / 2)
            {
                gammaRate += "1";
            }
            else
            {
                gammaRate += "0";
            }
        }

        int gammaRateDec = Convert.ToInt32(gammaRate, 2);
        int epsilonRateDec = (int)Math.Pow(2, input[0].Length) - 1 - gammaRateDec;

        partSubmitter.SubmitPart1(gammaRateDec * epsilonRateDec);

        int[] oxygenRate = this.RecursiveFilter(input, filterForMostCommon: true, 0);
        int oxygenRateDec = BinaryIntArrayToDecInt(oxygenRate);

        int[] co2Rate = this.RecursiveFilter(input, filterForMostCommon: false, 0);
        int co2RateDec = BinaryIntArrayToDecInt(co2Rate);

        partSubmitter.SubmitPart2(oxygenRateDec * co2RateDec);
    }

    private int[] RecursiveFilter(int[][] input, bool filterForMostCommon, int x)
    {
        if (input.Length == 1)
        {
            return input[0];
        }

        List<int[]> list1 = new List<int[]>();
        List<int[]> list0 = new List<int[]>();

        for (int y = 0; y < input.Length; y++)
        {
            if (input[y][x] == 0)
            {
                list0.Add(input[y]);
            }
            else
            {
                list1.Add(input[y]);
            }
        }

        if ((list1.Count >= list0.Count) == filterForMostCommon)
        {
            if (list1.Count == 1)
            {
                return list1[0];
            }

            return this.RecursiveFilter(list1.ToArray(), filterForMostCommon, x + 1);
        }
        else
        {
            if (list0.Count == 1)
            {
                return list0[0];
            }

            return this.RecursiveFilter(list0.ToArray(), filterForMostCommon, x + 1);
        }
    }

    private static int BinaryIntArrayToDecInt(int[] array)
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
}

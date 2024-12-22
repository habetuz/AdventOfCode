namespace AdventOfCode.Utils;

public static class Permutation
{
  public static IEnumerable<T[]> Permutate<T>(T[] values, int count)
  {
    int maxValue = (int)(Math.Pow(values.Length, count) - 1);
    for (int permutationIndex = 0; permutationIndex <= maxValue; permutationIndex++)
    {
      T[] permutation = Enumerable.Repeat(values[0], count).ToArray();
      int value = permutationIndex;
      int valueIndex = count;
      do
      {
        permutation[--valueIndex] = values[value % values.Length];
        value /= values.Length;
      } while (value > 0);
      yield return permutation;
    }
  }
}

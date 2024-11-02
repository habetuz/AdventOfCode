namespace AdventOfCode.Utils;

public static class AdventMath
{
    public static long lcm(int[] elements)
    {
        long lcmOfArrayElements = 1;
        int divisor = 2;

        while (true)
        {
            int counter = 0;
            bool divisible = false;
            for (int i = 0; i < elements.Length; i++)
            {
                // lcm_of_array_elements (n1, n2, ... 0) = 0.
                // For negative number we convert into
                // positive and calculate lcm_of_array_elements.
                if (elements[i] == 0)
                {
                    return 0;
                }
                else if (elements[i] < 0)
                {
                    elements[i] = elements[i] * (-1);
                }
                if (elements[i] == 1)
                {
                    counter++;
                }

                // Divide element_array by devisor if complete
                // division i.e. without remainder then replace
                // number with quotient; used for find next factor
                if (elements[i] % divisor == 0)
                {
                    divisible = true;
                    elements[i] = elements[i] / divisor;
                }
            }

            // If divisor able to completely divide any number
            // from array multiply with lcm_of_array_elements
            // and store into lcm_of_array_elements and continue
            // to same divisor for next factor finding.
            // else increment divisor
            if (divisible)
            {
                lcmOfArrayElements = lcmOfArrayElements * divisor;
            }
            else
            {
                divisor++;
            }

            // Check if all element_array is 1 indicate
            // we found all factors and terminate while loop.
            if (counter == elements.Length)
            {
                return lcmOfArrayElements;
            }
        }
    }
}

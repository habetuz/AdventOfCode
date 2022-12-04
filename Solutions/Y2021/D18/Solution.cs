namespace AdventOfCode.Solutions.Y2021.D18
{
    using AdventOfCode.Common;

    internal class Solution : Solution<string[]>
    {
        internal override (object clipboard, string message) Puzzle1(string[] input)
        {
            var snailfishNumber = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                snailfishNumber = this.Add(snailfishNumber, input[i]);
            }

            int magnitude = this.Magnitude(snailfishNumber);

            return (magnitude, $"The magnitude of the snailfish number is {magnitude}");
        }

        internal override (object clipboard, string message) Puzzle2(string[] input)
        {
            int largestMagnitude = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    int magnitude = this.Magnitude(this.Add(input[i], input[j]));
                    if (magnitude > largestMagnitude)
                    {
                        largestMagnitude = magnitude;
                    }

                    magnitude = this.Magnitude(this.Add(input[j], input[i]));
                    if (magnitude > largestMagnitude)
                    {
                        largestMagnitude = magnitude;
                    }
                }
            }

            return (largestMagnitude, $"The largest magnitude is {largestMagnitude}!");
        }

        private string Add(string left, string right)
        {
            string snailfishNumber = $"[{left},{right}]";

            bool firstRound = true;

            while (true)
            {
                // Explode
                int depth = 0;
                int indexNumber = -1;
                int lengthNumber = 0;
                int startNumber = 0;

                for (int index = 0; index < snailfishNumber.Length; index++)
                {
                    if (snailfishNumber[index] == '[')
                    {
                        depth++;

                        // [X..
                        //  ^ Number could start here.
                        startNumber = index + 1;

                        if (depth == 5)
                        {
                            // Explode
                            // Parse leftRemainder number
                            int additionLeft = snailfishNumber[index + 2] == ',' ?
                                snailfishNumber[index + 1] - '0' :
                                ((snailfishNumber[index + 1] - '0') * 10) + snailfishNumber[index + 2] - '0';
                            int start = index + 3 + (additionLeft >= 10 ? 1 : 0);
                            int additionRight = snailfishNumber[start + 1] == ']' ?
                                snailfishNumber[start] - '0' :
                                ((snailfishNumber[start] - '0') * 10) + snailfishNumber[start + 1] - '0';

                            // Add to leftRemainder number
                            if (indexNumber != -1)
                            {
                                int number = lengthNumber == 1 ?
                                    snailfishNumber[indexNumber] - '0' :
                                    ((snailfishNumber[indexNumber] - '0') * 10) + snailfishNumber[indexNumber + 1] - '0';

                                number += additionLeft;
                                snailfishNumber = snailfishNumber.Remove(indexNumber, lengthNumber).Insert(indexNumber, number.ToString());
                                if (number >= 10 && number - additionLeft < 10)
                                {
                                    index++;
                                }
                            }

                            // Find rightRemainder number
                            int originalIndex = index;
                            index += 6;
                            if (additionLeft >= 10)
                            {
                                index++;
                            }

                            if (additionRight >= 10)
                            {
                                index++;
                            }

                            indexNumber = -1;

                            for (; index < snailfishNumber.Length; index++)
                            {
                                if (snailfishNumber[index] >= '0' && snailfishNumber[index] <= '9')
                                {
                                    indexNumber = index;
                                    lengthNumber = snailfishNumber[index + 1] >= '0' && snailfishNumber[index + 1] <= '9' ?
                                        2 : 1;
                                    break;
                                }
                            }

                            index = originalIndex;

                            // Add to rightRemainder number
                            if (indexNumber != -1)
                            {
                                int number = lengthNumber == 1 ?
                                    snailfishNumber[indexNumber] - '0' :
                                    ((snailfishNumber[indexNumber] - '0') * 10) + snailfishNumber[indexNumber + 1] - '0';
                                number += additionRight;

                                snailfishNumber = snailfishNumber.Remove(indexNumber, lengthNumber).Insert(indexNumber, number.ToString());
                            }

                            // Replace exploded snailfish number
                            int explotionLength = 5;
                            if (additionLeft >= 10)
                            {
                                explotionLength++;
                            }

                            if (additionRight >= 10)
                            {
                                explotionLength++;
                            }

                            snailfishNumber = snailfishNumber.Remove(index, explotionLength).Insert(index, "0");

                            startNumber = index;
                            depth--;
                            if (!firstRound)
                            {
                                break;
                            }
                        }
                    }
                    else if (snailfishNumber[index] == ']')
                    {
                        depth--;

                        // X]
                        // ^ A number has to be there
                        if (startNumber != index)
                        {
                            indexNumber = startNumber;
                            lengthNumber = index - startNumber;
                        }

                        // ]] or ],
                        //  ^     ^ No number can be there, but otherwise the next interation would think that a number starts at the current index.
                        startNumber = index + 1;
                    }
                    else if (snailfishNumber[index] == ',')
                    {
                        // X,
                        // ^ A number has to be there
                        if (startNumber != index)
                        {
                            indexNumber = startNumber;
                            lengthNumber = index - startNumber;
                        }

                        // X,X or ],X
                        //   ^      ^ A number could start here.
                        startNumber = index + 1;
                    }
                }

                // Split
                bool splitted = false;
                for (int index = 0; index < snailfishNumber.Length; index++)
                {
                    if (snailfishNumber[index] >= '0' && snailfishNumber[index] <= '9' &&
                        snailfishNumber[index + 1] >= '0' && snailfishNumber[index + 1] <= '9')
                    {
                        int number = ((snailfishNumber[index] - '0') * 10) + snailfishNumber[index + 1] - '0';
                        int leftRemainder = number / 2;
                        int rightRemainder = number - leftRemainder;
                        snailfishNumber = snailfishNumber.Remove(index, 2).Insert(index, $"[{leftRemainder},{rightRemainder}]");
                        splitted = true;
                        break;
                    }
                }

                if (!splitted)
                {
                    break;
                }

                firstRound = false;
            }

            return snailfishNumber;
        }

        private int Magnitude(string snailfishNumber)
        {
            (_, int magnitude) = this.Magnitude(0, snailfishNumber);
            return magnitude;
        }

        private (int, int) Magnitude(int index, string snailfishNumber)
        {
            // [X
            //  ^ Go here
            index++;
            int leftMagnitude = snailfishNumber[index] - '0';

            if (leftMagnitude < 0 || leftMagnitude > 9)
            {
                (index, leftMagnitude) = this.Magnitude(index, snailfishNumber);
            }

            // X,X
            //   ^ Go here
            index += 2;

            int rightMagnitude = snailfishNumber[index] - '0';

            if (rightMagnitude < 0 || rightMagnitude > 9)
            {
                (index, rightMagnitude) = this.Magnitude(index, snailfishNumber);
            }

            // X]
            //  ^ Go here
            index++;

            return (index, (leftMagnitude * 3) + (rightMagnitude * 2));
        }
    }
}

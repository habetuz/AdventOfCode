namespace AdventOfCode.Solutions.Y2021.D18_old
{
    using System;
    using AdventOfCode.Common;

    internal class Solution : Solution<SnailfishNumber[]>
    {
        internal override (object, string) Puzzle1(SnailfishNumber[] input)
        {
            SnailfishNumber number = new Pair();
            ((Pair)number).Left = input[0].Copy();

            for (int i = 1; i < input.Length; i++)
            {
                ((Pair)number).Right = input[i].Copy();
                this.Reduce(ref number);
                Pair tmpNumber = (Pair)number;
                number = new Pair();
                ((Pair)number).Left = tmpNumber;
            }

            number = ((Pair)number).Left;

            int magnitude = this.Magnitude(number);
            return (magnitude.ToString(), $"The magnitude is {magnitude}!");
        }

        internal override (object, string) Puzzle2(SnailfishNumber[] input)
        {
            int highestMagnitude = 0;

            foreach (SnailfishNumber a in input)
            {
                foreach (SnailfishNumber b in input)
                {
                    if (a == b)
                    {
                        continue;
                    }

                    SnailfishNumber number = new Pair()
                    {
                        Left = a.Copy(),
                        Right = b.Copy(),
                    };

                    this.Reduce(ref number);

                    int magnitude = this.Magnitude(number);

                    if (magnitude > highestMagnitude)
                    {
                        highestMagnitude = magnitude;
                    }
                }
            }

            return (highestMagnitude.ToString(), $"The highest magnitude is {highestMagnitude}!");
        }

        private void Reduce(ref SnailfishNumber number)
        {
            while (true)
            {
                if (!this.Explode(ref number, null, null, 0) && !this.Split(ref number))
                {
                    return;
                }
            }
        }

        private bool Explode(ref SnailfishNumber number, LiteralNumber left, LiteralNumber right, int depth)
        {
            if (number.GetType() == typeof(LiteralNumber))
            {
                return false;
            }

            // Explode
            if (depth >= 4 && ((Pair)number).Left.GetType() == typeof(LiteralNumber) && ((Pair)number).Right.GetType() == typeof(LiteralNumber))
            {
                if (left != null)
                {
                    left.Value += ((LiteralNumber)((Pair)number).Left).Value;
                }

                if (right != null)
                {
                    right.Value += ((LiteralNumber)((Pair)number).Right).Value;
                }

                number = new LiteralNumber()
                {
                    Value = 0,
                };

                return true;
            }

            LiteralNumber leftOfRight;
            LiteralNumber rightOfLeft;

            if (((Pair)number).Left.GetType() == typeof(LiteralNumber))
            {
                leftOfRight = (LiteralNumber)((Pair)number).Left;
            }
            else
            {
                leftOfRight = this.GetMostRight((Pair)((Pair)number).Left);
            }

            if (((Pair)number).Right.GetType() == typeof(LiteralNumber))
            {
                rightOfLeft = (LiteralNumber)((Pair)number).Right;
            }
            else
            {
                rightOfLeft = this.GetMostLeft((Pair)((Pair)number).Right);
            }

            var refLeft = ((Pair)number).Left;
            var refRight = ((Pair)number).Right;

            if (this.Explode(ref refLeft, left, rightOfLeft, depth + 1))
            {
                return true;
            }

            if (this.Explode(ref refRight, leftOfRight, right, depth + 1))
            {
                return true;
            }

            ((Pair)number).Left = refLeft;
            ((Pair)number).Right = refRight;

            return false;
        }

        private LiteralNumber GetMostRight(Pair number)
        {
            if (number.Right.GetType() == typeof(LiteralNumber))
            {
                return (LiteralNumber)number.Right;
            }
            else
            {
                return this.GetMostRight((Pair)number.Right);
            }
        }

        private LiteralNumber GetMostLeft(Pair number)
        {
            if (number.Left.GetType() == typeof(LiteralNumber))
            {
                return (LiteralNumber)number.Left;
            }
            else
            {
                return this.GetMostLeft((Pair)number.Left);
            }
        }

        private bool Split(ref SnailfishNumber number)
        {
            if (number.GetType() == typeof(LiteralNumber))
            {
                int value = ((LiteralNumber)number).Value;

                // Split
                if (value >= 10)
                {
                    number = new Pair()
                    {
                        Left = new LiteralNumber()
                        {
                            Value = (int)Math.Floor((decimal)value / 2),
                        },
                        Right = new LiteralNumber()
                        {
                            Value = (int)Math.Ceiling((decimal)value / 2),
                        },
                    };

                    return true;
                }

                return false;
            }

            var refLeft = ((Pair)number).Left;
            var refRight = ((Pair)number).Right;

            if (this.Split(ref refLeft))
            {
                return true;
            }

            if (this.Split(ref refRight))
            {
                return true;
            }

            ((Pair)number).Left = refLeft;
            ((Pair)number).Right = refRight;

            return false;
        }

        private int Magnitude(SnailfishNumber number)
        {
            if (number.GetType() == typeof(LiteralNumber))
            {
                return ((LiteralNumber)number).Value;
            }

            return
                (this.Magnitude(((Pair)number).Left) * 3) +
                (this.Magnitude(((Pair)number).Right) * 2);
        }
    }
}

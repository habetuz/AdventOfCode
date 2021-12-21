using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D18
{
    internal class Solution : Solution<SnailfishNumber[]>
    {
        internal override string Puzzle1(SnailfishNumber[] input)
        { 
            SnailfishNumber number = new Pair();
            ((Pair)number).Left = input[0].Copy();

            for (int i = 1; i < input.Length; i++)
            {
                ((Pair)number).Right = input[i].Copy();
                Reduce(ref number);
                Pair tmpNumber = (Pair) number;
                number = new Pair();
                ((Pair)number).Left = tmpNumber;                
            }

            number = ((Pair)number).Left;

            int magnitude = Magnitude(number);
            s_logger.Log($"The magnitude is {magnitude}!", SharpLog.LogType.Info);
            return magnitude.ToString();
        }

        internal override string Puzzle2(SnailfishNumber[] input)
        {
            int highestMagnitude = 0;

            foreach (SnailfishNumber a in input)
            {
                foreach (SnailfishNumber b in input)
                {
                    if (a == b) continue;

                    SnailfishNumber number = new Pair()
                    {
                        Left = a.Copy(),
                        Right = b.Copy(),
                    };

                    Reduce(ref number);

                    int magnitude = Magnitude(number);

                    if (magnitude > highestMagnitude) highestMagnitude = magnitude;
                }
            }

            s_logger.Log($"The highest magnitude is {highestMagnitude}!", SharpLog.LogType.Info);
            return highestMagnitude.ToString();
        }

        private void Reduce(ref SnailfishNumber number)
        {
            while (true)
            {
                if (!Explode(ref number, null, null, 0) && !Split(ref number)) return;
            };
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
                if (left != null) left.Value += ((LiteralNumber)((Pair)number).Left).Value;
                if (right != null) right.Value += ((LiteralNumber)((Pair)number).Right).Value;
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
                leftOfRight = GetMostRight((Pair)((Pair)number).Left);
            }
            if (((Pair)number).Right.GetType() == typeof(LiteralNumber))
            {
                rightOfLeft = (LiteralNumber)((Pair)number).Right;
            }
            else
            {
                rightOfLeft = GetMostLeft((Pair)((Pair)number).Right);
            }

            if (Explode(ref ((Pair)number).Left, left, rightOfLeft, depth + 1)) return true;

            if (Explode(ref ((Pair)number).Right, leftOfRight, right, depth + 1)) return true;
            return false;
        }

        private LiteralNumber GetMostRight(Pair number)
        {
            if (number.Right.GetType() == typeof(LiteralNumber)) return (LiteralNumber) number.Right;
            else return GetMostRight((Pair) number.Right);
        }

        private LiteralNumber GetMostLeft(Pair number)
        {
            if (number.Left.GetType() == typeof(LiteralNumber)) return (LiteralNumber)number.Left;
            else return GetMostLeft((Pair)number.Left);
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
                            Value = (int)Math.Floor(((decimal)value / 2))
                        },
                        Right = new LiteralNumber()
                        {
                            Value = (int)Math.Ceiling(((decimal)value / 2))
                        }
                    };

                    return true;
                }

                return false;
            }
            if (Split(ref ((Pair)number).Left)) return true;
            if (Split(ref ((Pair)number).Right)) return true;
            return false;
        }

        private int Magnitude(SnailfishNumber number)
        {
            if (number.GetType() == typeof(LiteralNumber))
            {
                return ((LiteralNumber) number).Value;
            }

            return
                Magnitude(((Pair)number).Left)  * 3 +
                Magnitude(((Pair)number).Right) * 2;
        }
    }
}

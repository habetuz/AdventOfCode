namespace AdventOfCode.Solutions.Y2021.D23
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<(char[,], char[,])>
    {
        private static readonly Dictionary<char, byte> GoalPos = new Dictionary<char, byte>
        {
            { 'A', 3 },
            { 'B', 5 },
            { 'C', 7 },
            { 'D', 9 },
        };

        private static readonly Dictionary<char, ushort> MoveCost = new Dictionary<char, ushort>
        {
            { 'A', 1 },
            { 'B', 10 },
            { 'C', 100 },
            { 'D', 1000 },
        };

        private readonly (byte, byte)[] positions = new (byte, byte)[]
        {
            (1, 1),
            (2, 1),
            (4, 1),
            (6, 1),
            (8, 1),
            (10, 1),
            (11, 1),
        };

        internal override (object, string) Puzzle1((char[,], char[,]) input)
        {
            Logging.LogDebug(Tools.Formatt(input.Item1));
            (_, _) = new Parser().Parse("#############\n#...........#\n###A#B#C#D###\n  #A#B#C#D#\n  #########");

            var neededCost = this.Step(input.Item1, 0, int.MaxValue, new Dictionary<string, int>());

            return (neededCost.ToString(), $"{neededCost} energy is needed to sort the amphipods!");
        }

        internal override (object, string) Puzzle2((char[,], char[,]) input)
        {
            Logging.LogDebug(Tools.Formatt(input.Item2));

            var neededCost = this.Step(input.Item2, 0, int.MaxValue, new Dictionary<string, int>());

            return (neededCost.ToString(), $"{neededCost} energy is needed to sort the amphipods!");
        }

        private int Step(char[,] burrow, int score, int bestScore, Dictionary<string, int> discovered)
        {
            // SharpLog.Logging.LogDebug($"Best score:    {bestScore}");
            // SharpLog.Logging.LogDebug($"Current score: {score}");
            // Tools.Print2D(burrow)
            bool isFinished = true;

            List<(int, char[,])> valideMoves = new List<(int, char[,])>();

            for (byte y = 1; y < burrow.GetLength(1) - 1; y++)
            {
                for (byte x = 1; x < burrow.GetLength(0) - 1; x++)
                {
                    if (burrow[x, y] > 'D' || burrow[x, y] < 'A')
                    {
                        continue;
                    }

                    var amphipod = burrow[x, y];

                    var goalPos = GoalPos[amphipod];

                    // Check if amphipod is already at it's goal
                    if (this.IsAtGoal((x, y), burrow))
                    {
                        continue;
                    }

                    isFinished = false;

                    // Check if amphipod is on the hallway and could walk to its goal
                    if (y == 1)
                    {
                        if (!this.IsValideMove(x, goalPos, burrow))
                        {
                            continue;
                        }

                        for (byte i = 2; i < burrow.GetLength(1) - 1; i++)
                        {
                            if (burrow[goalPos, i] == '.')
                            {
                                if (i == burrow.GetLength(1) - 2)
                                {
                                    this.MoveIfPossible((x, y), ((byte, byte))(goalPos, burrow.GetLength(1) - 2), burrow, score, bestScore, valideMoves, discovered);
                                }

                                continue;
                            }
                            else if (burrow[goalPos, i] == amphipod &&
                                this.IsAtGoal((goalPos, i), burrow))
                            {
                                this.MoveIfPossible((x, y), ((byte, byte))(goalPos, i - 1), burrow, score, bestScore, valideMoves, discovered);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }

                        continue;
                    }

                    // Check if amphipod is valide to move out of a goal onto the hallway
                    bool valide = true;

                    for (byte i = (byte)(y - 1); i > 1; i--)
                    {
                        if (burrow[x, i] != '.')
                        {
                            valide = false;
                        }
                    }

                    if (!valide)
                    {
                        continue;
                    }

                    foreach (var position in this.positions)
                    {
                        // Check if move is valide
                        if (position != (x, y) && this.IsValideMove(x, position.Item1, burrow))
                        {
                            this.MoveIfPossible((x, y), position, burrow, score, bestScore, valideMoves, discovered);
                        }
                    }
                }
            }

            if (isFinished)
            {
                SharpLog.Logging.LogDebug($"Finished with {score} energy needed!");

                return score;
            }

            foreach (var move in valideMoves)
            {
                // Tools.Print2D(move.Item2);
                var stepScore = this.Step(move.Item2, move.Item1, bestScore, discovered);

                if (stepScore < bestScore)
                {
                    bestScore = stepScore;
                }
            }

            return bestScore;
        }

        private bool ValidateBurrow(char[,] burrow)
        {
            byte counter = 0;

            foreach (char c in burrow)
            {
                if (c == '.')
                {
                    counter++;
                }
            }

            return counter == 11;
        }

        private bool IsAtGoal((byte, byte) amphipod, char[,] burrow)
        {
            if (amphipod.Item1 == GoalPos[burrow[amphipod.Item1, amphipod.Item2]])
            {
                // Check if all the spaces bellow the amphipod are filled with the right amphipods.
                for (int i = burrow.GetLength(1) - 2; i > amphipod.Item2; i--)
                {
                    if (burrow[amphipod.Item1, i] != burrow[amphipod.Item1, amphipod.Item2])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void MoveIfPossible((byte, byte) from, (byte, byte) to, char[,] burrow, int score, int bestScore, List<(int, char[,])> valideMoves, Dictionary<string, int> discovered)
        {
            (var newBurrow, var moveCost) = this.MoveAmphipod(from, to, burrow);

            /*
            if (!ValidateBurrow(newBurrow))
            {
                SharpLog.Logging.LogDebug("Invalide burrow!");
                Tools.Print2D(burrow);
                Tools.Print2D(newBurrow);
            }
            */

            // Tools.Print2D(newBurrow);
            var str = this.AsString(newBurrow);

            if (score + moveCost < bestScore && (!discovered.ContainsKey(str) || score + moveCost < discovered[str]))
            {
                // if (str == "###    #.#    #.######.BDDA##.######.CCBD##.######.BBAC##.######..ACA##.######D#    ###    ")
                // {
                //    Tools.Print2D(burrow);
                //    Tools.Print2D(newBurrow);
                // }
                discovered[str] = score + moveCost;
                valideMoves.Add((score + moveCost, newBurrow));
            }
        }

        private string AsString(char[,] burrow)
        {
            string str = string.Empty;

            foreach (char c in burrow)
            {
                str += c;
            }

            return str;
        }

        private bool IsValideMove(byte fromX, byte toX, char[,] burrow)
        {
            bool valide = true;

            if (toX < fromX)
            {
                for (byte i = toX; i < fromX; i++)
                {
                    if (burrow[i, 1] != '.')
                    {
                        valide = false;
                    }
                }
            }
            else
            {
                for (byte i = toX; i > fromX; i--)
                {
                    if (burrow[i, 1] != '.')
                    {
                        valide = false;
                    }
                }
            }

            return valide;
        }

        private (char[,], int) MoveAmphipod((byte, byte) from, (byte, byte) to, char[,] burrow)
        {
            char[,] newBurrow = burrow.Clone() as char[,];

            newBurrow[to.Item1, to.Item2] = newBurrow[from.Item1, from.Item2];
            newBurrow[from.Item1, from.Item2] = '.';

            return (
                newBurrow,
                (Math.Abs(from.Item1 - to.Item1) + Math.Abs(from.Item2 - to.Item2)) * MoveCost[newBurrow[to.Item1, to.Item2]]);
        }
    }
}

using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D21
{
    internal class Solution : Solution<(int, int)> 
    {
        private bool _winner = false;
        private int _startingPosition = 0;

        private Dictionary<int, long> _wins = new Dictionary<int, long>();

        internal override string Puzzle1((int, int) input)
        {
            Player player1 = new Player(input.Item1, 1000);
            Player player2 = new Player(input.Item2, 1000);
            player1.WinEvent += PlayerWins;
            player2.WinEvent += PlayerWins;

            for (int i = 1; !_winner; i += 3)
            {
                int value = (i - 1) % 100 + 1;
                player1.RoleCount += 3;
                player2.RoleCount += 3;

                if (value % 2 == 1)
                {
                    player1.Position += 3 * value + 3;
                }
                else
                {
                    player2.Position += 3 * value + 3;
                }
            }

            int solution = 0;
            if (player1.Score < 1000)
            {
                solution = player1.Score * player1.RoleCount;
            }
            else
            {
                solution = player2.Score * player2.RoleCount;
            }

            s_logger.Log($"The solution is {solution}!", SharpLog.LogType.Info);
            return solution.ToString();
        }

        internal override string Puzzle2((int, int) input)
        {
            _wins.Clear();
            Player player1 = new Player(input.Item1, 21);
            Player player2 = new Player(input.Item2, 21);
            player1.WinEvent += PlayerWins;
            player2.WinEvent += PlayerWins;

            RecursiveDice(player1, player2, true);

            long[] wins = _wins.Values.ToArray();

            Array.Sort(wins);

            s_logger.Log($"The most wins are {wins[0]}!", SharpLog.LogType.Info);
            return wins[0].ToString();
        }

        private void PlayerWins(int startingPosition, int dimentions = 1)
        {
            _winner = true;
            _startingPosition = startingPosition;

            _wins[startingPosition] = _wins.ContainsKey(startingPosition)? _wins[startingPosition] + dimentions : dimentions;
        }

        private void RecursiveDice(Player player1, Player player2, bool turnPlayer1)
        {
            if (player1.Won || player2.Won) return;

            if (turnPlayer1)
            {
                Player player1_3 = player1.Clone();
                Player player1_4 = player1.Clone();
                Player player1_5 = player1.Clone();
                Player player1_6 = player1.Clone();
                Player player1_7 = player1.Clone();
                Player player1_8 = player1.Clone();
                Player player1_9 = player1.Clone();

                player1_4.RepresentingDimentions *= 3;

                player1_5.RepresentingDimentions *= 6;

                player1_6.RepresentingDimentions *= 7;

                player1_7.RepresentingDimentions *= 6;

                player1_8.RepresentingDimentions *= 3;

                player1_3.Position += 3;
                player1_4.Position += 4;
                player1_5.Position += 5;
                player1_6.Position += 6;
                player1_7.Position += 7;
                player1_8.Position += 8;
                player1_9.Position += 9;

                RecursiveDice(player1_3, player2, false);
                RecursiveDice(player1_4, player2, false);
                RecursiveDice(player1_5, player2, false);
                RecursiveDice(player1_6, player2, false);
                RecursiveDice(player1_7, player2, false);
                RecursiveDice(player1_8, player2, false);
                RecursiveDice(player1_9, player2, false);
            }
            else
            {
                Player player2_3 = player2.Clone();
                Player player2_4 = player2.Clone();
                Player player2_5 = player2.Clone();
                Player player2_6 = player2.Clone();
                Player player2_7 = player2.Clone();
                Player player2_8 = player2.Clone();
                Player player2_9 = player2.Clone();

                player2_4.RepresentingDimentions *= 3;

                player2_5.RepresentingDimentions *= 6;

                player2_6.RepresentingDimentions *= 7;

                player2_7.RepresentingDimentions *= 6;

                player2_8.RepresentingDimentions *= 3;

                player2_3.Position += 3;
                player2_4.Position += 4;
                player2_5.Position += 5;
                player2_6.Position += 6;
                player2_7.Position += 7;
                player2_8.Position += 8;
                player2_9.Position += 9;

                RecursiveDice(player1, player2_3, true);
                RecursiveDice(player1, player2_4, true);
                RecursiveDice(player1, player2_5, true);
                RecursiveDice(player1, player2_6, true);
                RecursiveDice(player1, player2_7, true);
                RecursiveDice(player1, player2_8, true);
                RecursiveDice(player1, player2_9, true);
            }
        }
    }
}

using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Y2021.D21
{
    internal class Solution : Solution<(int, int)>
    {
        private bool _winner = false;

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

        private void PlayerWins(int none, int none2)
        {
            _winner = true;
        }

        override internal string Puzzle2((int, int) input)
        {
            byte neededScore = 21;

            (long winsPlayer1, long winsPlayer2) = Turn(
                player: 1,
                steps: 3,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 1);

            (long tmpWinsPlayer1, long tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 4,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 3);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 5,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 6);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 6,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 7);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 7,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 6);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 8,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 3);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(
                player: 1,
                steps: 9,
                positionPlayer1: (byte)input.Item1,
                positionPlayer2: (byte)input.Item2,
                scorePlayer1: 0,
                scorePlayer2: 0,
                neededScore: neededScore,
                dimetions: 1);

            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            s_logger.Log($"Player 1 won {winsPlayer1} times, player 2 won {winsPlayer2} times!", SharpLog.LogType.Info);

            if (winsPlayer1 < winsPlayer2) return winsPlayer2.ToString();
            else return winsPlayer1.ToString();
        }

        private (long, long) Turn(byte player, byte steps, byte positionPlayer1, byte positionPlayer2, long scorePlayer1, long scorePlayer2, byte neededScore, long dimetions)
        {
            if (player == 1)
            {
                positionPlayer1 = (byte)((positionPlayer1 + steps - 1) % 10 + 1);
                scorePlayer1 += positionPlayer1;
                if (scorePlayer1 >= neededScore)
                {
                    return (dimetions, 0);
                }

                player = 0;
            }
            else
            {
                positionPlayer2 = (byte)((positionPlayer2 + steps - 1) % 10 + 1);
                scorePlayer2 += positionPlayer2;
                if (scorePlayer2 >= neededScore)
                {
                    return (0, dimetions);
                }

                player = 1;
            }

            (long winsPlayer1, long winsPlayer2) = Turn(player, steps: 3, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions);

            (long tmpWinsPlayer1, long tmpWinsPlayer2) = Turn(player, steps: 4, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions * 3);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(player, 5, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions * 6);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(player, 6, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions * 7);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(player, 7, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions * 6);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(player, 8, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions * 3);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            (tmpWinsPlayer1, tmpWinsPlayer2) = Turn(player, 9, positionPlayer1, positionPlayer2, scorePlayer1, scorePlayer2, neededScore, dimetions);
            winsPlayer1 += tmpWinsPlayer1;
            winsPlayer2 += tmpWinsPlayer2;

            return (winsPlayer1, winsPlayer2);
        }
    }
}

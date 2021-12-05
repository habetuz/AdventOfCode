using AdventOfCode.Common;
using SharpLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D04
{
    internal class Parser : Parser<Tuple<int[], Board[]>>
    {
        internal override Tuple<int[], Board[]> Parse(string input)
        {
            string[] lines = input.Split('\n');

            s_progressTracker = new ProgressTracker(lines.Length -2, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            string[] drawsString = lines[0].Split(',');
            int[] draws = new int[drawsString.Length];
            for(int i = 0; i < drawsString.Length; i++)
            {
                draws[i] = int.Parse(drawsString[i]);
            }

            List<Board> boards = new List<Board>();

            for (int boardIndex = 2; boardIndex < lines.Length; boardIndex+=6)
            {
                int[,] boardValues = new int[5,5];

                for (int y = 0; y < 5; y++)
                {

                    string[] line = lines[boardIndex + y].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int x = 0; x < 5; x++)
                    {
                        boardValues[x, y] = int.Parse(line[x]);
                    }

                    s_progressTracker.CurrentStep = boardIndex + y;
                }

                boards.Add(new Board(boardValues));
            }

            return new Tuple<int[], Board[]>(draws, boards.ToArray());
        }
    }
}

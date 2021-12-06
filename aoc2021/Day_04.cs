using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_04 : BetterBaseDay
    {
        public override string P1()
        {
            int[] pool = Input[0].Split(',').Select(s => s.AsInt()).ToArray();

            List<Matrix<int>> boards = new();

            for (int index = 2; index < Input.Length; index += 6)
            {
                List<int[]> rows = new();
                for (int r = 0; r < 5; ++r)
                {
                    rows.Add(Input[index + r]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.AsInt())
                        .ToArray());
                }
                boards.Add(new Matrix<int>(rows));
            }

            string res = "no";
            for (int index = 0; index < pool.Length; ++index)
            {
                int val = pool[index];

                boards.ForEachBreakable(board =>
                {
                    board.ForEachCoord((x, y) =>
                    {
                        if (board.Data[x, y] == val)
                            board.Data[x, y] += 1000;
                    });

                    if (IsDone(board))
                    {
                        res = (SumPos(board) * val).ToString();
                        index = pool.Length;
                        return false;
                    }

                    return true;
                });
            }

            return res;
        }

        private static bool IsDone(Matrix<int> board)
        {
            for (int i = 0; i < 5; ++i)
            {
                if (board.GetCol(i).Where(v => v >= 1000).Count() == 5)
                    return true;

                if (board.GetRow(i).Where(v => v >= 1000).Count() == 5)
                    return true;
            }

            return false;
        }

        private static int SumPos(Matrix<int> board)
        {
            int sum = 0;

            board.ForEachCoord((x, y) =>
            {
                if (board.Data[x, y] < 1000)
                    sum += board.Data[x, y];
            });

            return sum;
        }

        public override string P2()
        {
            int[] pool = Input[0].Split(',').Select(s => s.AsInt()).ToArray();

            List<Matrix<int>> boards = new();

            for (int index = 2; index < Input.Length; index += 6)
            {
                List<int[]> rows = new();
                for (int r = 0; r < 5; ++r)
                {
                    rows.Add(Input[index + r]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.AsInt())
                        .ToArray());
                }
                boards.Add(new Matrix<int>(rows));
            }

            string res = "no";
            for (int index = 0; index < pool.Length; ++index)
            {
                int val = pool[index];

                boards.ForEach(board =>
                {
                    board.ForEachCoord((x, y) =>
                    {
                        if (board.Data[x, y] == val)
                            board.Data[x, y] += 1000;
                    });
                });

                if (boards.Count == 1 && IsDone(boards[0]))
                {
                    return (SumPos(boards[0]) * val).ToString();
                }

                boards = boards.Where(b => !IsDone(b)).ToList();
            }

            return res;
        }
    }
}

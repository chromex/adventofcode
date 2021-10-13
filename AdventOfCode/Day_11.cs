using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_11 : BetterBaseDay
    {
        private List<char[]> baseState;

        public Day_11()
        {
            baseState = Input.Select(str => $".{str}.".ToArray()).ToList();
            baseState.Insert(0, new string('.', baseState[0].Length).ToArray());
            baseState.Add(new string('.', baseState[0].Length).ToArray());
        }

        private List<char[]> Duplichar(List<char[]> orig)
        {
            return orig.Select(o => new string(o).ToArray()).ToList();
        }

        public override string Solve_1()
        {
            return Compute(4, Occupied1);
        }

        private static bool Occupied1(List<char[]> input, int x, int y, int dx, int dy)
        {
            return input[y + dy][x + dx] == '#';
        }

        public override string Solve_2()
        {
            return Compute(5, Occupied2);
        }

        private static bool Occupied2(List<char[]> input, int x, int y, int dx, int dy)
        {
            while (true)
            {
                x += dx;
                y += dy;

                if (x < 0 || y < 0 || x >= input[0].Length || y >= input.Count || input[y][x] == 'L') return false;
                if (input[y][x] == '#') return true;
            }
        }

        private string Compute(int maxNeighbors, Func<List<char[]>, int, int, int, int, bool> check)
        {
            List<char[]> last, state = Duplichar(baseState);

            do
            {
                last = Duplichar(state);
                state = Advance(state, maxNeighbors, check);
            } while (last.Select(s => new string(s)).ToList().Except(state.Select(s => new string(s))).Count() != 0);

            return state.Select(str => str.Where(s => s == '#').Count()).Sum().ToString();
        }

        private List<char[]> Advance(List<char[]> input, int maxN, Func<List<char[]>, int, int, int, int, bool> check)
        {
            List<char[]> result = Duplichar(input);

            for (int y = 1; y < input.Count - 1; ++y)
            {
                for (int x = 1; x < input[0].Length - 1; ++x)
                {
                    char current = input[y][x];
                    if (current == '.') continue;

                    int nNeighbors = 0;

                    for (int dy = -1; dy < 2; ++dy)
                    {
                        for (int dx = -1; dx < 2; ++dx)
                        {
                            if (dy == 0 && dx == 0) continue;

                            if (check(input, x, y, dx, dy))
                                ++nNeighbors;
                        }
                    }

                    if (current == '#' && nNeighbors >= maxN)
                        result[y][x] = 'L';

                    if (current == 'L' && nNeighbors == 0)
                        result[y][x] = '#';
                }
            }

            return result;
        }
    }
}

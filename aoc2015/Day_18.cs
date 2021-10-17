using AoCUtil;
using System.Linq;

namespace aoc2015
{
    class Day_18 : BetterBaseDay
    {
        private Matrix<char> GetMap() => new(Input.Select(line => line.ToCharArray()));

        private int CountNeighbor(int x, int y, Matrix<char> map) => map.LoseGet(x, y) == '#' ? 1 : 0;

        private int SumNeighbors(int x, int y, Matrix<char> map)
        {
            return
                CountNeighbor(x - 1, y - 1, map) +
                CountNeighbor(x, y - 1, map) +
                CountNeighbor(x + 1, y - 1, map) +
                CountNeighbor(x - 1, y, map) +
                CountNeighbor(x + 1, y, map) +
                CountNeighbor(x - 1, y + 1, map) +
                CountNeighbor(x, y + 1, map) +
                CountNeighbor(x + 1, y + 1, map);
        }

        private void Rev(Matrix<char> map, bool special)
        {
            Matrix<char> copy = new(map);

            map.ForEachCoord((x, y) =>
            {
                if (special && ((x == 0 && y == 0) || (x == 0 && y == map.Height - 1) || (x == map.Width - 1 && y == 0) || (x == map.Width - 1 && y == map.Height - 1)))
                {
                    return;
                }

                int sum = SumNeighbors(x, y, copy);
                if (map.Data[x, y] == '#')
                {
                    if (sum != 2 && sum != 3)
                    {
                        map.Data[x, y] = '.';
                    }
                }
                else if (sum == 3)
                {
                    map.Data[x, y] = '#';
                }
            });
        }

        public override string Solve_1()
        {
            Matrix<char> map = GetMap();

            for (int count = 0; count < 100; ++count)
            {
                Rev(map, false);
            }

            int sum = 0;
            map.ForEachCoord(
                (x, y) =>
                {
                    if (map.Data[x, y] == '#') ++sum;
                });
            return sum.ToString();
        }

        public override string Solve_2()
        {
            Matrix<char> map = GetMap();
            map.Data[0, 0] = '#';
            map.Data[map.Width - 1, 0] = '#';
            map.Data[map.Width - 1, map.Height - 1] = '#';
            map.Data[0, map.Height - 1] = '#';

            for (int count = 0; count < 100; ++count)
            {
                Rev(map, true);
            }

            int sum = 0;
            map.ForEachCoord(
                (x, y) =>
                {
                    if (map.Data[x, y] == '#') ++sum;
                });
            return sum.ToString();
        }
    }
}

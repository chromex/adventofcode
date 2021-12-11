using AoCUtil;
using System;
using System.Linq;

namespace aoc2021
{
    class Day_11 : BetterBaseDay
    {
        private static void Poke(Matrix<int> map, int x, int y)
        {
            int val = 0;
            if (map.TryGet(x, y, out val) && !map.IsMarked(x, y))
            {
                ++map.Data[x, y];

                if (map.Data[x, y] > 9)
                {
                    map.Mark(x, y);

                    Poke(map, x - 1, y - 1);
                    Poke(map, x - 1, y);
                    Poke(map, x - 1, y + 1);
                    Poke(map, x, y - 1);
                    Poke(map, x, y + 1);
                    Poke(map, x + 1, y - 1);
                    Poke(map, x + 1, y);
                    Poke(map, x + 1, y + 1);
                }
            }
        }

        public override string P1()
        {
            Matrix<int> map = new Matrix<int>(Input.Select(line => line.Select(c => int.Parse($"{c}")).ToArray()));

            int flashes = 0;

            for (int step = 0; step < 10000; ++step)
            {
                map.ForEachCoord((x, y) =>
                {
                    ++map.Data[x, y];
                });

                map.ForEachCoord((x, y) =>
                {
                    if (map.Data[x, y] > 9)
                    {
                        Poke(map, x, y);
                    }
                });

                int s = 0;
                map.ForEachCoord((x, y) =>
                {
                    if (map.IsMarked(x, y))
                    {
                        map.Data[x, y] = 0;
                        map.ResetMark(x, y);
                        ++flashes;
                        ++s;
                    }
                });

                // For part 2
                if (s == 100)
                {
                    return step.ToString();
                }
            }

            return flashes.ToString();
        }

        public override string P2()
        {
            return "no";
        }
    }
}

using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_09 : BetterBaseDay
    {
        private List<Vec2> lows = new();

        public override string P1()
        {
            Matrix<int> map = new Matrix<int>(Input.Select(line => line.Select(c => int.Parse($"{c}")).ToArray()));

            int sum = 0;

            map.ForEachCoord((x, y) =>
            {
                int height = map.Data[x, y];

                int neighbor = 0;
                if (map.TryGet(x + 1, y, out neighbor) && neighbor <= height) return;
                if (map.TryGet(x - 1, y, out neighbor) && neighbor <= height) return;
                if (map.TryGet(x, y + 1, out neighbor) && neighbor <= height) return;
                if (map.TryGet(x, y - 1, out neighbor) && neighbor <= height) return;

                lows.Add(new(x, y));

                sum += (height + 1);
            });

            return sum.ToString();
        }

        private static int GetBasinSize(Matrix<int> map, int x, int y)
        {
            int cur;
            if (!map.TryGet(x, y, out cur) || map.IsMarked(x, y) || cur >= 9)
            {
                return 0;
            }

            map.Mark(x, y);

            return 1 +
                GetBasinSize(map, x + 1, y) +
                GetBasinSize(map, x - 1, y) +
                GetBasinSize(map, x, y + 1) +
                GetBasinSize(map, x, y - 1);

        }

        public override string P2()
        {
            Matrix<int> map = new Matrix<int>(Input.Select(line => line.Select(c => int.Parse($"{c}")).ToArray()));
            List<int> sizes = new();

            lows.ForEach(v => sizes.Add(GetBasinSize(map, v.X, v.Y)));

            Console.WriteLine(map);

            int res = 1;
            sizes.OrderByDescending(s => s).Take(3).ForEach(s => res *= s);

            return res.ToString();
        }
    }
}

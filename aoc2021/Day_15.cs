using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_15 : BetterBaseDay
    {
        public Matrix<int> Load() => new Matrix<int>(Input.Select(s => s.Select(c => int.Parse($"{c}")).ToArray()));

        private static bool Check(Matrix<int> map, Matrix<int> costs, int x, int y, int neighborCost)
        {
            int cost;
            if (map.TryGet(x, y, out cost))
            {
                int jump = neighborCost + cost;
                if (jump < costs.Data[x, y])
                {
                    costs.Data[x, y] = jump;
                    return true;
                }
            }

            return false;
        }

        private static void Flood(Matrix<int> map, Matrix<int> costs)
        {
            costs.Data[0, 0] = 0;
            List<Vec2> toCheck = new() { new Vec2(0, 0) };

            while (toCheck.Count > 0)
            {
                List<Vec2> nextCheck = new();

                toCheck.ForEach(v =>
                {
                    int local = costs.Data[v.X, v.Y];

                    if (Check(map, costs, v.X + 1, v.Y, local))
                        nextCheck.Add(new(v.X + 1, v.Y));
                    if (Check(map, costs, v.X - 1, v.Y, local))
                        nextCheck.Add(new(v.X - 1, v.Y));
                    if (Check(map, costs, v.X, v.Y + 1, local))
                        nextCheck.Add(new(v.X, v.Y + 1));
                    if (Check(map, costs, v.X, v.Y - 1, local))
                        nextCheck.Add(new(v.X, v.Y - 1));
                });

                toCheck = nextCheck;
            }
        }

        public override string P1()
        {
            Matrix<int> map = Load();
            Matrix<int> costs = new(map.Width, map.Height, int.MaxValue);

            Flood(map, costs);

            return costs.Data[costs.Width - 1, costs.Height - 1].ToString();
        }

        private static Matrix<int> Expand(Matrix<int> map)
        {
            Matrix<int> result = new(map.Width * 5, map.Height * 5);

            result.ForEachCoord((x, y) =>
            {
                int bx = x % map.Width;
                int by = y % map.Height;
                int mx = x / map.Width;
                int my = y / map.Height;
                int mod = mx + my;

                result.Data[x, y] = ((map.Data[bx, by] + mod - 1) % 9) + 1;
            });

            return result;
        }

        public override string P2()
        {
            Matrix<int> map = Load();
            map = Expand(map);

            Matrix<int> costs = new(map.Width, map.Height, int.MaxValue);

            Flood(map, costs);

            return costs.Data[costs.Width - 1, costs.Height - 1].ToString();
        }
    }
}

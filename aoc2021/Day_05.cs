using AoCUtil;
using System;

namespace aoc2021
{
    class Day_05 : BetterBaseDay
    {
        public override string Solve_1()
        {
            Matrix<int> map = new(1000, 1000);

            Input.Split(' ').ForEach(vals =>
            {
                Vec2 v0 = new(vals[0]);
                Vec2 v1 = new(vals[2]);

                if (v0.X == v1.X)
                {
                    int sy = Math.Min(v0.Y, v1.Y);
                    int ey = Math.Max(v0.Y, v1.Y);
                    for (int y = sy; y <= ey; ++y)
                    {
                        ++map.Data[v0.X, y];
                    }
                }
                else if (v0.Y == v1.Y)
                {
                    int sx = Math.Min(v0.X, v1.X);
                    int ex = Math.Max(v0.X, v1.X);
                    for (int x = sx; x <= ex; ++x)
                    {
                        ++map.Data[x, v0.Y];
                    }
                }
            });

            int sum = 0;
            map.ForEachCoord((x, y) =>
            {
                if (map.Data[x, y] >= 2)
                    ++sum;
            });
            return sum.ToString();
        }

        public override string Solve_2()
        {
            Matrix<int> map = new(1000, 1000);

            Input.Split(' ').ForEach(vals =>
            {
                Vec2 v0 = new(vals[0]);
                Vec2 v1 = new(vals[2]);
                Vec2 delta;

                if (v0.X > v1.X) 
                    Util.Swap(ref v0, ref v1);

                if (v0.X == v1.X)
                {
                    if (v0.Y < v1.Y) delta = new(0, 1);
                    else delta = new(0, -1);
                }
                else if (v0.Y == v1.Y)
                {
                    delta = new(1, 0);
                }
                else
                {
                    if (v1.Y > v0.Y) delta = new(1, 1);
                    else delta = new(1, -1);
                }

                while (v0 != v1)
                {
                    ++map.Data[v0.X, v0.Y];
                    v0 += delta;
                }

                ++map.Data[v0.X, v0.Y];
            });

            int sum = 0;
            map.ForEachCoord((x, y) =>
            {
                if (map.Data[x, y] >= 2)
                    ++sum;
            });
            return sum.ToString();
        }
    }
}

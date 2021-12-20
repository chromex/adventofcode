using AoCUtil;
using System;

namespace aoc2021
{
    class Day_17 : BetterBaseDay
    {
        private int maxHeight = 0;

        private bool Hits(Vec2 vel, Vec2 xRange, Vec2 yRange)
        {
            Vec2 pos = new(0, 0);

            int localMax = 0;

            while (pos.X <= xRange.Y && pos.Y >= yRange.X)
            {
                pos += vel;

                localMax = Math.Max(localMax, pos.Y);

                if (pos.X >= xRange.X && pos.X <= xRange.Y &&
                    pos.Y >= yRange.X && pos.Y <= yRange.Y)
                {
                    maxHeight = Math.Max(localMax, maxHeight);
                    return true;
                }

                if (vel.X > 0) --vel.X;
                if (vel.X < 0) ++vel.X;
                vel.Y -= 1;
            }

            return false;
        }

        public override string P1()
        {
            Vec2 xRange = new(138, 184);
            Vec2 yRange = new(-125, -71);

            for (int x = 0; x < 200; ++x)
            {
                for (int y = 0; y < 200; ++y)
                {
                    Hits(new Vec2(x, y), xRange, yRange);
                }
            }

            return maxHeight.ToString();
        }

        public override string P2()
        {
            Vec2 xRange = new(138, 184);
            Vec2 yRange = new(-125, -71);

            int sum = 0;
            for (int x = 0; x < 200; ++x)
            {
                for (int y = -200; y < 200; ++y)
                {
                    if (Hits(new Vec2(x, y), xRange, yRange)) ++sum;
                }
            }

            return sum.ToString();
        }
    }
}

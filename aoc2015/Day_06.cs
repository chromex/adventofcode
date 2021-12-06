using AoCUtil;
using System;
using System.Linq;

namespace aoc2015
{
    class Day_06 : BetterBaseDay
    {
        private static readonly int Size = 1000;
        private bool[] fastMap = new bool[Size * Size];
        private int[] fastMap2 = new int[Size * Size];

        private void Inc(Vec2 min, Vec2 max)
        {
            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    int i = y * Size + x;
                    fastMap2[i] = fastMap2[i] + 1;
                }
            }
        }

        private void Dec(Vec2 min, Vec2 max)
        {
            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    int i = y * Size + x;
                    fastMap2[i] = Math.Max(fastMap2[i] - 1, 0);
                }
            }
        }

        private void FastTurnOn(Vec2 min, Vec2 max)
        {
            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    int i = y * Size + x;
                    fastMap[i] = true;
                }
            }
        }

        private void FastTurnOff(Vec2 min, Vec2 max)
        {
            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    int i = y * Size + x;
                    fastMap[i] = false;
                }
            }
        }

        private void FastToggle(Vec2 min, Vec2 max)
        {
            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    int i = y * Size + x;
                    fastMap[i] = !fastMap[i];
                }
            }
        }

        private void Do(string str)
        {
            string[] spl = str.Split(" ");

            Vec2 min = new(spl[spl.Length - 3]);
            Vec2 max = new(spl[spl.Length - 1]);

            if (spl.Length == 5)
            {
                if (spl[1] == "on") FastTurnOn(min, max);
                else FastTurnOff(min, max);
            }
            else
            {
                FastToggle(min, max);
            }
        }

        public override string P1()
        {
            Input.ForEach(line => Do(line));

            return fastMap.Count(b => b).ToString();
        }

        private void Do2(string str)
        {
            string[] spl = str.Split(" ");

            Vec2 min = new(spl[spl.Length - 3]);
            Vec2 max = new(spl[spl.Length - 1]);

            if (spl.Length == 5)
            {
                if (spl[1] == "on") Inc(min, max);
                else Dec(min, max);
            }
            else
            {
                Inc(min, max);
                Inc(min, max);
            }
        }

        public override string P2()
        {
            Input.ForEach(line => Do2(line));

            return fastMap2.Sum().ToString();
        }
    }
}

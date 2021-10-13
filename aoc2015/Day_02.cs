using AoCUtil;
using System;
using System.Linq;

namespace aoc2015
{
    class Day_02 : BetterBaseDay
    {
        private int GetSqft(string line)
        {
            int[] sides = line.Split("x").Select(v => int.Parse(v)).ToArray();
            int s1 = sides[0] * sides[1];
            int s2 = sides[0] * sides[2];
            int s3 = sides[1] * sides[2];
            int smallest = Math.Min(s1, Math.Min(s2, s3));

            return 2 * s1 + 2 * s2 + 2 * s3 + smallest;
        }

        public override string Solve_1()
        {
            return Input.Select(line => GetSqft(line)).Sum().ToString();
        }

        private int GetRibbon(string line)
        {
            int[] sides = line.Split("x").Select(v => int.Parse(v)).ToArray();
            int p1 = sides[0] * 2 + sides[1] * 2;
            int p2 = sides[0] * 2 + sides[2] * 2;
            int p3 = sides[1] * 2 + sides[2] * 2;
            int smallest = Math.Min(p1, Math.Min(p2, p3));

            return smallest + sides[0] * sides[1] * sides[2];
        }

        public override string Solve_2()
        {
            return Input.Select(line => GetRibbon(line)).Sum().ToString();
        }
    }
}

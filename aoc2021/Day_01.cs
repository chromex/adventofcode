using AoCUtil;
using System.Linq;

namespace aoc2021
{
    class Day_01 : BetterBaseDay
    {
        public override string Solve_1()
        {
            int[] depths = Input.Select(s => int.Parse(s)).ToArray();
            int sum = 0;
            for (int i = 0; i < depths.Length - 1; ++i)
            {
                if (depths[i] < depths[i + 1]) ++sum;
            }
            return sum.ToString();
        }

        private static int Sum(int[] depths, int idx) => depths[idx] + depths[idx + 1] + depths[idx + 2];

        public override string Solve_2()
        {
            int[] depths = Input.Select(s => int.Parse(s)).ToArray();
            int sum = 0;
            for (int i = 0; i < depths.Length - 3; ++i)
            {
                if (Sum(depths, i) < Sum(depths, i + 1)) ++sum;
            }
            return sum.ToString();
        }
    }
}

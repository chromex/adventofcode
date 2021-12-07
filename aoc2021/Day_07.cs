using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_07 : BetterBaseDay
    {
        public override string P1()
        {
            int[] positions = Input[0].Split(',').Select(s => s.AsInt()).ToArray();

            int min = positions.Min();
            int max = positions.Max();

            return Enumerable.Range(min, max - min + 1)
                .Select(p => positions.Sum(crab => Math.Abs(crab - p)))
                .Min()
                .ToString();
        }

        public override string P2()
        {
            int[] positions = Input[0].Split(',').Select(s => s.AsInt()).ToArray();

            int min = positions.Min();
            int max = positions.Max();

            List<int> costs = new() { 0 };
            for (int i = 1; i <= (max - min); ++i)
                costs.Add(costs[i - 1] + i);

            return Enumerable.Range(min, max - min + 1)
                .Select(p => positions.Sum(crab => costs[Math.Abs(crab - p)]))
                .Min()
                .ToString();
        }
    }
}

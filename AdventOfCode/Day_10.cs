using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_10 : BetterBaseDay
    {
        private List<int> deltas = new List<int>();

        public Day_10()
        {
            List<int> jolts = Input.Select(line => int.Parse(line)).OrderBy(val => val).ToList();
            jolts.Insert(0, 0);
            jolts.Add(jolts.Last() + 3);

            for (int index = 0; index < jolts.Count - 1; ++index)
            {
                deltas.Add(jolts[index + 1] - jolts[index]);
            }
        }

        public override string Solve_1()
        {
            return $"{deltas.Where(val => val == 1).Count() * deltas.Where(val => val == 3).Count()}";
        }

        public override string Solve_2()
        {
            List<int> sumRuns = new List<int>();

            for (int index = 0; index < deltas.Count; ++index)
            {
                if (deltas[index] == 1)
                {
                    int endIndex = deltas.FindIndex(index + 1, val => val == 3);
                    if (endIndex != -1)
                    {
                        sumRuns.Add(endIndex - index);
                        index = endIndex;
                    }
                    else
                    {
                        sumRuns.Add(deltas.Count - index);
                        break;
                    }
                }
            }

            long permutations = 1;
            for (int index = 0; index < sumRuns.Count; ++index)
            {
                long n = (long)Math.Pow(2.0, (double)(sumRuns[index] - 1));
                if (sumRuns[index] > 3)
                {
                    n -= (long)Math.Pow(2.0, (double)(sumRuns[index] - 4));
                }

                permutations *= n;
            }

            return permutations.ToString();
        }
    }
}

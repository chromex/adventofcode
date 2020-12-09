using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_09 : BetterBaseDay
    {
        private List<long> numbers = new List<long>();
        const int prevCount = 25;

        public Day_09()
        {
            numbers = Input.Select(str => long.Parse(str)).ToList();
        }

        public override string Solve_1()
        {
            List<long> set = numbers.GetRange(0, prevCount).ToList();

            return $"{FindBadSum(set)}";
        }

        public override string Solve_2()
        {
            List<long> set = numbers.GetRange(0, prevCount).ToList();

            return $"{FindWeakness(numbers, FindBadSum(set))}";
        }

        private long FindBadSum(List<long> set)
        {
            int readIndex = set.Count;

            while (readIndex < numbers.Count)
            {
                long current = numbers[readIndex];
                if (!FindSum(set, current))
                {
                    return current;
                }

                set.RemoveAt(0);
                set.Add(current);

                ++readIndex;
            }

            return -1;
        }

        private bool FindSum(List<long> set, long current)
        {
            foreach (long left in set)
            {
                foreach (long right in set)
                {
                    if (left != right && (left + right) == current)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private long FindWeakness(List<long> set, long current)
        {
            for (int left = 0; left < set.Count - 1; ++left)
            {
                for (int right = left + 1; right < set.Count; ++right)
                {
                    var range = set.GetRange(left, (right - left));
                    long sum = range.Sum();

                    if (sum == current)
                    {
                        return range.Min() + range.Max();
                    }
                    else if (sum > current)
                    {
                        break;
                    }
                }
            }

            return -1;
        }
    }
}

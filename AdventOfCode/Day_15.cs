using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_15 : BetterBaseDay
    {
        public override string Solve_1()
        {
            List<long> numbers = Input[0].Split(",").Select(str => long.Parse(str)).ToList();

            while (numbers.Count < 2020)
            {
                long last = numbers.Last();
                int prevIndex = numbers.FindLastIndex(numbers.Count - 2, num => num == last);
                if (prevIndex == -1)
                {
                    numbers.Add(0);
                }
                else
                {
                    numbers.Add(numbers.Count - prevIndex - 1);
                }
            }

            return numbers.Last().ToString();
        }

        public override string Solve_2()
        {
            Dictionary<long, Tuple<long, long>> history = new Dictionary<long, Tuple<long, long>>();

            long prev = 0;
            long index = 0;
            foreach (long number in Input[0].Split(",").Select(str => long.Parse(str)))
            {
                history[number] = Tuple.Create(index++, -1L);
                prev = number;
            }

            while (index < 30000000)
            {
                if (history[prev].Item2 < 0)
                {
                    prev = 0;
                }
                else
                {
                    prev = history[prev].Item2;
                }

                long delta = -1;
                if (history.ContainsKey(prev))
                {
                    delta = index - history[prev].Item1;
                }

                history[prev] = Tuple.Create(index++, delta);
            }

            return prev.ToString();
        }
    }
}

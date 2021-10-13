using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_13 : BetterBaseDay
    {
        public Day_13()
        { }

        public override string Solve_1()
        {
            int target = int.Parse(Input[0]);
            List<long> ids = Input[1].Split(",").Where(str => !str.Equals("x")).Select(str => long.Parse(str)).ToList();

            long minArrival = int.MaxValue;
            long bestId = 0;

            foreach (long id in ids)
            {
                long firstArrivalAfter = ((target / id) + 1) * id;
                if (firstArrivalAfter < minArrival)
                {
                    minArrival = firstArrivalAfter;
                    bestId = id;
                }
            }

            return $"{(minArrival - target) * bestId}";
        }

        public override string Solve_2()
        {
            List<long> ids = Input[1].Split(",").Select(str => str == "x" ? 0 : long.Parse(str)).ToList();
            Tuple<long, long>[] primes = ids.Where(id => id != 0).Select(id => Tuple.Create<long, long>(id, ids.FindIndex(subId => id == subId))).OrderByDescending(i => i.Item1).ToArray();

            long mult = 1;
            long multInc = 1;
            int index = 1;
            while (true)
            {
                long test = primes[0].Item1 * mult - primes[0].Item2;

                if ((test + primes[index].Item2) % primes[index].Item1 != 0)
                {
                    mult += multInc;
                }
                else
                {
                    while (index < primes.Length && (test + primes[index].Item2) % primes[index].Item1 == 0)
                    {
                        ++index;
                    }

                    if (index < primes.Length)
                    {
                        multInc *= primes[index - 1].Item1;
                        mult += multInc;
                    }
                    else
                    {
                        return test.ToString();
                    }
                }
            }
        }
    }
}

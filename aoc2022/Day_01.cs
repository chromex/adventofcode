using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022
{
    class Day_01 : BetterBaseDay
    {
        private List<ulong> elves = new();

        public override string P1()
        {
            ulong cur = 0;
            Input.ForEach(s =>
            {
                ulong val = 0;
                if (ulong.TryParse(s, out val))
                {
                    cur += val;
                }
                else
                {
                    elves.Add(cur);
                    cur = 0;
                }
            });

            if (cur != 0)
                elves.Add(cur);

            return elves.Max().ToString();
        }

        public override string P2()
        {
            var s = elves.OrderByDescending(i => i).Take(3).ToArray();

            return (s[0] + s[1] + s[2]).ToString();
        }
    }
}

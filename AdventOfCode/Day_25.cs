using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day_25 : BetterBaseDay
    {
        public override string Solve_1()
        {
            long doorPubKey = long.Parse(Input[0]);
            long cardPubKey = long.Parse(Input[1]);

            long doorCount = FindCount(doorPubKey);
            long cardCount = FindCount(cardPubKey);

            long val = 1;
            for (long count = 0; count < cardCount; ++count)
                val = Transform(val, doorPubKey);

            long val2 = 1;
            for (long count = 0; count < doorCount; ++count)
                val2 = Transform(val2, cardPubKey);

            return val.ToString();
        }

        public override string Solve_2()
        {
            return "err";
        }

        private long FindCount(long pubkey)
        {
            long val = 1, count;
            for (count = 0; val != pubkey; ++count)
            {
                val = Transform(val, 7);
            }
            return count;
        }

        private long Transform(long val, long subjectNumber)
        {
            return (val * subjectNumber) % 20201227;
        }
    }
}

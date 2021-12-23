using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2021
{
    class Day_21 : BetterBaseDay
    {
        class Die
        {
            private int val = 1;
            public int Roll
            {
                get
                {
                    ++nRolls;
                    int ret = val++;
                    if (val > 100) val = 1;
                    return ret;
                }
            }

            public int nRolls;
        }

        private static bool Move(ref int pos, ref int score, int roll)
        {
            pos += roll;
            while (pos > 10) pos -= 10;
            score += pos;

            return score >= 1000;
        }

        public override string P1()
        {
            int p1 = 1, p2 = 6;
            int p1s = 0, p2s = 0;
            Die die = new();

            //Move(ref p1, ref p1s, 5);

            while (true)
            {
                if (Move(ref p1, ref p1s, die.Roll + die.Roll + die.Roll)) break;
                Console.WriteLine($"P1: {p1} - {p1s}");
                //if (Move(ref p1, ref p1s, die.Roll)) break;
                //if (Move(ref p1, ref p1s, die.Roll)) break;
                if (Move(ref p2, ref p2s, die.Roll + die.Roll + die.Roll)) break;
                Console.WriteLine($"P2: {p2} - {p2s}");
                //if (Move(ref p2, ref p2s, die.Roll)) break;
                //if (Move(ref p2, ref p2s, die.Roll)) break;
            }

            return (Math.Min(p1s, p2s) * die.nRolls).ToString();

            //return "no";
        }

        public override string P2()
        {
            int p1 = 4, p2 = 8;
            int p1s = 0, p2s = 0;

            return "no";
        }
    }
}

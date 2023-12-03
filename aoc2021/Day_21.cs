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

            while (true)
            {
                if (Move(ref p1, ref p1s, die.Roll + die.Roll + die.Roll)) break;
                //Console.WriteLine($"P1: {p1} - {p1s}");

                if (Move(ref p2, ref p2s, die.Roll + die.Roll + die.Roll)) break;
                //Console.WriteLine($"P2: {p2} - {p2s}");
            }

            return (Math.Min(p1s, p2s) * die.nRolls).ToString();
        }

        private static int GetScore(int start, int[] rolls)
        {
            int score = 0;

            rolls.ForEach(r =>
            {
                start = ((start + r - 1) % 10) + 1;
                score += start;
            });

            return score;
        }

        private static ulong SumWins1(int[] p1rolls, int[] p2rolls)
        {
            ulong wins = 1;

            for (int index = 0; index < p1rolls.Length; ++index)
            {
                wins *= Probs[p1rolls[index]];

                if (index < p1rolls.Length - 1)
                    wins *= Probs[p2rolls[index]];
            }

            return wins;
        }

        private static ulong SumWins2(int[] p1rolls, int[] p2rolls)
        {
            return SumWins1(p1rolls, p2rolls) * Probs[p2rolls.Last()];
        }

        private static int[] Rolls = new[] { 3, 4, 5, 6, 7, 8, 9 };
        private static ulong[] Probs = new ulong[] { 0, 0, 0, 1, 3, 6, 7, 6, 3, 1 };

        private static void Play(int p1, int p2, int[] p1rolls, int[] p2rolls, ref ulong p1wins, ref ulong p2wins)
        {
            if (GetScore(p1, p1rolls) >= 21)
            {
                p1wins += SumWins1(p1rolls, p2rolls);
                return;
            }

            if (GetScore(p2, p2rolls) >= 21)
            {
                p2wins += SumWins2(p1rolls, p2rolls);
                return;
            }

            foreach(int r1 in Rolls)
            {
                foreach (int r2 in Rolls)
                {
                    Play(p1, p2, p1rolls.Append(r1), p2rolls.Append(r2), ref p1wins, ref p2wins);
                }
            }
        }

        public override string P2()
        {
            int p1 = 1, p2 = 6;
            ulong p1wins = 0, p2wins = 0;

            //Console.WriteLine($"Test: {SumWins2(new int[] { 3, 6 }, new int[] { 4, 8 })}");

            Play(p1, p2, new int[] { }, new int[] { }, ref p1wins, ref p2wins);

            p1wins /= 7;

            //Console.WriteLine($"{ulong.MaxValue}");
            Console.WriteLine($"P1: {p1wins}, P2: {p2wins}");

            return Math.Max(p1wins, p2wins).ToString();
        }
    }
}

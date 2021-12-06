using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_13 : BetterBaseDay
    {
        private int[] fastHappy = new int[100];
        private List<string> people;
        private int maxd = int.MinValue;

        private int GetHappiness(int i, int j) => fastHappy[i * 10 + j];

        private static int GetIndex(string name)
        {
            switch (name)
            {
                case "Alice": return 0;
                case "Bob": return 1;
                case "Carol": return 2;
                case "David": return 3;
                case "Eric": return 4;
                case "Frank": return 5;
                case "George": return 6;
                case "Mallory": return 7;
                case "Me": return 8;
            }

            return -99;
        }

        private static int GetKey(string p1, string p2) => GetIndex(p1) * 10 + GetIndex(p2);

        private void Load()
        {
            HashSet<string> p = new();
            Input.ForEach(line =>
            {
                string[] spl = line.Split(" ");
                string p1 = spl[0];
                string p2 = spl[10].Substring(0, spl[10].Length - 1);
                int val = int.Parse(spl[3]);
                if (spl[2] == "lose")
                    val *= -1;
                p.Add(p1);
                p.Add(p2);

                fastHappy[GetKey(p1, p2)] = fastHappy[GetKey(p1, p2)] + val;
                fastHappy[GetKey(p2, p1)] = fastHappy[GetKey(p2, p1)] + val;
            });
            people = p.ToList();
        }

        private int Compute(string s)
        {
            int dist = 0;

            for (int i = 0; i < s.Length - 1; ++i)
            {
                dist += GetHappiness(s[i] - '0', s[i + 1] - '0');
            }

            dist += GetHappiness(s[0] - '0', s[s.Length - 1] - '0');

            return dist;
        }

        private void Permute(int max, string s, char maxC)
        {
            if (s.Length == max)
            {
                int d = Compute(s);
                maxd = Math.Max(d, maxd);
                return;
            }

            for (char c = '0'; c <= maxC; ++c)
            {
                if (!s.Contains(c))
                {
                    Permute(max, s + c, maxC);
                }
            }
        }

        public override string P1()
        {
            Load();

            Permute(8, "", '7');

            return maxd.ToString();
        }

        public override string P2()
        {
            maxd = int.MinValue;

            people.ForEach((p) =>
            {
                fastHappy[GetKey(p, "Me")] = 0;
                fastHappy[GetKey("Me", p)] = 0;
            });

            Permute(9, "", '8');

            return maxd.ToString();
        }
    }
}

using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_13 : BetterBaseDay
    {
        private Dictionary<string, int> netHappy = new();
        private List<string> people;
        private int maxd = int.MinValue;

        private static string GetKey(string i, string j)
        {
            if (i.CompareTo(j) < 0)
            {
                return $"{i}{j}";
            }
            else
            {
                return $"{j}{i}";
            }
        }

        private int GetHappiness(string i, string j) => netHappy[GetKey(i, j)];

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
                netHappy[GetKey(p1, p2)] = netHappy.GetValueOrDefault(GetKey(p1, p2)) + val;
            });
            people = p.ToList();
        }

        private int Compute(string s)
        {
            int dist = 0;

            for (int i = 0; i < s.Length - 1; ++i)
            {
                dist += GetHappiness(people[s[i] - '0'], people[s[i + 1] - '0']);
            }

            dist += GetHappiness(people[s[0] - '0'], people[s[s.Length - 1] - '0']);

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

        public override string Solve_1()
        {
            Load();

            Permute(8, "", '7');

            return maxd.ToString();
        }

        public override string Solve_2()
        {
            maxd = int.MinValue;

            people.ForEach(p => netHappy[GetKey(p, "me")] = 0);
            people.Add("me");

            Permute(9, "", '8');

            return maxd.ToString();
        }
    }
}

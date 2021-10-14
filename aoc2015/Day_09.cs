using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_09 : BetterBaseDay
    {
        private Dictionary<string, int> distances = new();
        private string[] places;
        private int min = int.MaxValue;
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

        private void Load()
        {
            HashSet<string> p = new();
            Input.ForEach(line =>
            {
                string[] spl = line.Split(" ");
                distances[GetKey(spl[0], spl[2])] = int.Parse(spl[4]);
                p.Add(spl[0]);
                p.Add(spl[2]);
            });
            places = p.ToArray();
        }

        private int GetDistance(string i, string j) => distances[GetKey(i, j)];

        private int Compute(string s)
        {
            int dist = 0;

            for (int i = 0; i < s.Length - 1; ++i)
            {
                dist += GetDistance(places[s[i] - '0'], places[s[i + 1] - '0']);
            }

            return dist;
        }

        private void Permute(int max, string s)
        {
            if (s.Length == max)
            {
                int d = Compute(s);
                min = Math.Min(d, min);
                maxd = Math.Max(d, maxd);
                return;
            }

            for (char c = '0'; c <= '7'; ++c)
            {
                if (!s.Contains(c))
                {
                    Permute(max, s + c);
                }
            }
        }

        public override string Solve_1()
        {
            Load();

            Permute(8, "");

            return min.ToString();
        }

        public override string Solve_2()
        {
            return maxd.ToString();
        }
    }
}

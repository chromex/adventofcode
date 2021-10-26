using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    class Day_04 : BetterBaseDay
    {
        private static string Checksum(Dictionary<char, int> counts)
        {
            int max = counts.Values.Max();

            string check = "";

            while (check.Length < 5)
            {
                foreach (var entry in counts)
                {
                    if (entry.Value == max)
                    {
                        check += entry.Key;
                    }
                }

                max = Math.Max(0, max - 1);
            }

            return check.Substring(0, 5);
        }

        private static bool IsReal(string str, out int sector)
        {
            string front = str.Substring(0, str.Length - 7);
            string check = str.Substring(str.Length - 6, 5);

            Dictionary<char, int> counts = new();
            for (char c = 'a'; c <= 'z'; ++c)
            {
                counts[c] = 0;
            }

            front.ForEach(ch =>
            {
                if (char.IsLetter(ch)) { ++counts[ch]; }
            });

            sector = int.Parse(front.Substring(front.LastIndexOf('-') + 1));

            return check == Checksum(counts);
        }

        public override string Solve_1()
        {
            ulong sum = 0;
            int sector = 0;
            Input.ForEach(line =>
            {
                if (IsReal(line, out sector))
                {
                    sum += (ulong)sector;
                }
            });
            return sum.ToString();
        }

        private static char Shift(char c, int count)
        {
            if (c == '-') return ' ';

            int v = c - 'a';
            v = (v + count) % 26;
            return (char)('a' + v);
        }

        private static string GetRealName(string str, int sector)
        {
            string front = str.Substring(0, str.LastIndexOf("-"));
            return new string(front.Select(ch => Shift(ch, sector)).ToArray());
        }

        public override string Solve_2()
        {
            int result = 0;

            Input.ForEach(line =>
            {
                int sector = 0;
                if (IsReal(line, out sector))
                {
                    Console.WriteLine(GetRealName(line, sector));
                    if (GetRealName(line, sector).Contains("north"))
                    {
                        result = sector;
                    }
                }
            });

            return result.ToString();
        }
    }
}

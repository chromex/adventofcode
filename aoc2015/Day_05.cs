using AoCUtil;
using System.Linq;

namespace aoc2015
{
    class Day_05 : BetterBaseDay
    {
        private bool Nice(string str)
        {
            int vowels = str.Sum(c => "aeiou".Contains(c) ? 1 : 0);
            bool dupe = false;

            for (int idx = 0; idx < str.Length - 1; ++idx)
            {
                string sub = str.Substring(idx, 2);
                if (sub[0] == sub[1]) dupe = true;
                switch (sub)
                {
                    case "ab": return false;
                    case "cd": return false;
                    case "pq": return false;
                    case "xy": return false;
                }
            }

            return dupe && vowels >= 3;
        }

        public override string Solve_1()
        {
            return Input.Where(s => Nice(s)).Count().ToString();
        }

        private bool Nicer(string str)
        {
            bool repeat = false;

            for (int idx = 0; idx < str.Length - 3; ++idx)
            {
                string sub = str.Substring(idx, 2);
                if (str.Substring(idx + 2).Contains(sub))
                {
                    repeat = true;
                    break;
                }
            }

            for (int idx = 0; idx < str.Length - 2; ++idx)
            {
                if (str[idx] == str[idx + 2]) return repeat;
            }

            return false;
        }

        public override string Solve_2()
        {
            return Input.Where(s => Nicer(s)).Count().ToString();
        }
    }
}

using AoCUtil;
using System;
using System.Text;

namespace aoc2015
{
    class Day_10 : BetterBaseDay
    {
        private string LookAndSay(int count, string s)
        {
            if (count == 0)
            {
                return s;
            }

            StringBuilder sb = new();
            for (int index = 0; index < s.Length; )
            {
                char c = s[index];

                int next = index + 1;
                while (next < s.Length && s[next] == c) ++next;

                sb.Append((char)('0' + (next - index)));
                sb.Append(c);

                index = next;
            }

            return LookAndSay(count - 1, sb.ToString());
        }

        public override string P1()
        {
            string s = LookAndSay(40, "1113222113");
            return s.Length.ToString();
        }

        public override string P2()
        {
            string s = LookAndSay(50, "1113222113");
            return s.Length.ToString();
        }
    }
}

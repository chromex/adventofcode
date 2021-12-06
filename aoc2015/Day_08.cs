using AoCUtil;
using System.Linq;

namespace aoc2015
{
    class Day_08 : BetterBaseDay
    {
        private int SumChars(string s)
        {
            int sum = 0;

            for (int index = 1; index < s.Length - 1; ++index)
            {
                if (s[index] == '\\')
                {
                    if (s[index+1] == 'x')
                    {
                        index += 3;
                    }
                    else
                    {
                        index += 1;
                    }
                }

                ++sum;
            }

            return sum;
        }

        public override string P1()
        {
            return Input.Sum(l => l.Length - SumChars(l)).ToString();
        }

        private int SumEncoded(string s)
        {
            int sum = 2;

            for (int index = 0; index < s.Length; ++index)
            {
                if (s[index] == '"' || s[index] == '\\')
                {
                    ++sum;
                }

                ++sum;
            }

            return sum;
        }

        public override string P2()
        {
            return Input.Sum(l => SumEncoded(l) - l.Length).ToString();
        }
    }
}

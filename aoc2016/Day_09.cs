using AoCUtil;
using System.Linq;
using System.Text;

namespace aoc2016
{
    class Day_09 : BetterBaseDay
    {
        private static Vec2 ParseMarker(string str) => new Vec2(str.Split('x'));

        private static string Decomp(string str)
        {
            StringBuilder sb = new();

            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == '(')
                {
                    int endMark = str.IndexOf(')', i);
                    Vec2 marker = ParseMarker(str.Substring(i + 1, endMark - i - 1));
                    i = endMark + marker.X;

                    while (marker.Y-- > 0)
                        sb.Append(str.Substring(endMark + 1, marker.X));
                }
                else
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }

        private static ulong DecompMark(StringView str, ref int i)
        {
            int endMark = str.IndexOf(')', i);
            Vec2 marker = ParseMarker(str.Substring(i + 1, endMark - i - 1).GetString());
            i = endMark + marker.X;
            return Decomp2(str.Substring(endMark + 1, marker.X)) * (ulong)marker.Y;
        }

        private static ulong Decomp2(StringView str)
        {
            ulong count = 0; 

            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] != '(')
                {
                    ++count;
                }
                else
                {
                    count += DecompMark(str, ref i);
                }
            }

            return count;
        }

        public override string Solve_1()
        {
            return Decomp(Input[0]).Count(c => !char.IsWhiteSpace(c)).ToString();
        }

        public override string Solve_2()
        {
            return Decomp2(new StringView(Input[0])).ToString();
        }
    }
}

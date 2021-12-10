using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2021
{
    class Day_10 : BetterBaseDay
    {
        private static bool IsEndChar(char n)
        {
            return n == ')' || n == ']' || n == '}' || n == '>';
        }

        private static bool IsMatch(char c, char n)
        {
            return (c == '(' && n == ')') ||
                (c == '[' && n == ']') ||
                (c == '{' && n == '}') ||
                (c == '<' && n == '>');
        }

        private static ulong CompScore(char c)
        {
            switch (c)
            {
                case '(': return 1;
                case '[': return 2;
                case '{': return 3;
                case '<': return 4;
            }

            return 0;
        }

        private static int Parse(string line, int idx, ref ulong retScore)
        {
            char c = line[idx];

            if ((idx + 1) >= line.Length)
            {
                retScore = (retScore * 5) + CompScore(c);
                return idx + 1;
            }

            char n = line[idx + 1];

            if (IsMatch(c, n))
            {
                return idx + 2;
            }
            else if (IsEndChar(n))
            {
                throw new Exception($"{c}");
            }

            int curIdx = idx + 1;
            while (!IsEndChar(n))
            {
                curIdx = Parse(line, curIdx, ref retScore);

                if (curIdx >= line.Length)
                {
                    retScore = (retScore * 5) + CompScore(c);
                    return curIdx;
                }

                n = line[curIdx];
            }

            if (IsMatch(c, n))
            {
                return curIdx + 1;
            }

            throw new Exception($"{n}");
        }

        private static int Score(string line)
        {
            try
            {
                ulong s = 0;
                int idx = 0;
                do
                {
                    idx = Parse(line, idx, ref s);
                } while (idx < line.Length);
            }
            catch (Exception ex)
            {
                switch (ex.Message[0])
                {
                    case ')': return 3;
                    case ']': return 57;
                    case '}': return 1197;
                    case '>': return 25137;
                }
            }

            return 0;
        }

        public override string P1()
        {
            return Input.Select(line => Score(line)).Sum().ToString();
        }

        public override string P2()
        {
            List<ulong> sums = new();

            Input.ForEach(line =>
            {
                try
                {
                    ulong score = 0;
                    int idx = 0;
                    do
                    {
                        idx = Parse(line, idx, ref score);
                    } while (idx < line.Length);
                    sums.Add(score);
                }
                catch (Exception)
                { }
            });

            return sums.OrderBy(v => v).ToList()[sums.Count / 2].ToString();
        }
    }
}

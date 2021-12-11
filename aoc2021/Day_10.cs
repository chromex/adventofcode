using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private static ulong Process(string line)
        {
            List<char> stack = new();

            line.ForEach(c =>
            {
                if (!IsEndChar(c))
                {
                    stack.Push(c);
                }    
                else
                {
                    char top = stack.Pop();

                    if (!IsMatch(top, c))
                    {
                        throw new Exception($"{c}");
                    }
                }
            });

            ulong score = 0;
            while (stack.Count > 0)
            {
                score = (score * 5) + CompScore(stack.Pop());
            }
            return score;
        }

        private static int Score(string line)
        {
            try
            {
                Process(line);
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
                    sums.Add(Process(line));
                }
                catch (Exception)
                { }
            });

            return sums.OrderBy(v => v).ToList()[sums.Count / 2].ToString();
        }
    }
}

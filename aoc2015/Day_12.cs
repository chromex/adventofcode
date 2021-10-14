using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_12 : BetterBaseDay
    {
        public override string Solve_1()
        {
            Parser p = new(Input[0]);

            int sum = 0;
            int val;
            while (!p.Done())
            {
                if (p.AcceptNumber(out val))
                {
                    sum += val;
                }
                else
                {
                    p.Burn();
                }
            }

            return sum.ToString();
        }

        private static int Parse(Parser p)
        {
            Symbol endSym = Symbol.RSqBracket;
            if (p.Accept(Symbol.LBracket))
            {
                endSym = Symbol.RBracket;
            }
            else
            {
                p.Expect(Symbol.LSqBracket);
            }

            List<int> numbers = new();
            bool redFound = false;

            while (!p.Done() && p.PeekSymbol() != endSym)
            {
                switch (p.PeekSymbol())
                {
                    case Symbol.LBracket:
                        numbers.Add(Parse(p));
                        break;

                    case Symbol.LSqBracket:
                        numbers.Add(Parse(p));
                        break;

                    case Symbol.Ident:
                        {
                            int val;
                            if (p.Accept("red"))
                            {
                                redFound = endSym == Symbol.RBracket;
                            }
                            else if (p.AcceptNumber(out val))
                            {
                                numbers.Add(val);
                            }
                            else
                            {
                                p.Burn();
                            }
                        }
                        break;

                    default:
                        p.Burn();
                        break;
                }
            }

            p.Burn();

            return redFound ? 0 : numbers.Sum();
        }

        public override string Solve_2()
        {
            return Parse(new Parser(Input[0])).ToString();
        }
    }
}

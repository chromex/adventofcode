using AoCUtil;
using System.Linq;

namespace AdventOfCode
{
    public class Day_18 : BetterBaseDay
    {
        private long Comp(Parser p, bool bestPlus)
        {
            long result = 0;

            if (p.PeekSymbol() == Symbol.Ident)
            {
                result = p.GetNumber();
            }
            else if (p.Accept(Symbol.LParen))
            {
                result = Comp(p, bestPlus);
                p.Accept(Symbol.RParen);
            }

            while (!p.Done() && p.PeekSymbol() != Symbol.RParen)
            {
                Symbol sym = p.PeekSymbol();
                p.Burn();

                long rVal = 0;

                if (!bestPlus || sym == Symbol.Plus)
                {
                    if (p.PeekSymbol() == Symbol.Ident)
                    {
                        rVal = p.GetNumber();
                    }
                    else if (p.Accept(Symbol.LParen))
                    {
                        rVal = Comp(p, bestPlus);
                        p.Accept(Symbol.RParen);
                    }
                }
                else
                {
                    rVal = Comp(p, bestPlus);
                }

                if (sym == Symbol.Plus)
                    result += rVal;
                else
                    result *= rVal;
            }

            return result;
        }

        public override string P1()
        {
            return Input.Select(line => Comp(new Parser(line), false)).Sum().ToString();
        }

        public override string P2()
        {
            return Input.Select(line => Comp(new Parser(line), true)).Sum().ToString();
        }
    }
}

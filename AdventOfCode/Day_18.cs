using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_18 : BetterBaseDay
    {
        private long ComputeRPN(List<Tuple<Symbol, long>> commands)
        {
            Stack<long> stack = new Stack<long>();

            foreach (var tup in commands)
            {
                switch (tup.Item1)
                {
                    case Symbol.Ident: stack.Push(tup.Item2); break;
                    case Symbol.Plus: stack.Push(stack.Pop() + stack.Pop()); break;
                    case Symbol.Mult: stack.Push(stack.Pop() * stack.Pop()); break;
                }
            }

            return stack.Pop();
        }

        private List<Tuple<Symbol, long>> GetRPN(Parser p, bool bestPlus)
        {
            List<Tuple<Symbol, long>> commands = new List<Tuple<Symbol, long>>();

            if (p.PeekSymbol() == Symbol.Ident)
            {
                commands.Add(Tuple.Create(Symbol.Ident, (long)p.GetNumber()));
            }
            else if (p.Accept(Symbol.LParen))
            {
                commands.AddRange(GetRPN(p, bestPlus));
                p.Accept(Symbol.RParen);
            }

            while (!p.Done() && p.PeekSymbol() != Symbol.RParen)
            {
                Symbol sym = p.PeekSymbol();
                p.Burn();

                if (!bestPlus || sym == Symbol.Plus)
                {
                    if (p.PeekSymbol() == Symbol.Ident)
                    {
                        commands.Add(Tuple.Create(Symbol.Ident, (long)p.GetNumber()));
                    }
                    else if (p.Accept(Symbol.LParen))
                    {
                        commands.AddRange(GetRPN(p, bestPlus));
                        p.Accept(Symbol.RParen);
                    }
                }
                else
                {
                    commands.AddRange(GetRPN(p, bestPlus));
                }

                commands.Add(Tuple.Create(sym, 0L));
            }

            return commands;
        }

        public override string Solve_1()
        {
            return Input.Select(line => ComputeRPN(GetRPN(new Parser(line), false))).Sum().ToString();
        }

        public override string Solve_2()
        {
            return Input.Select(line => ComputeRPN(GetRPN(new Parser(line), true))).Sum().ToString();
        }
    }
}

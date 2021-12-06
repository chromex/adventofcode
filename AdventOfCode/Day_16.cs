using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_16 : BetterBaseDay
    {
        List<Rule> rules = new List<Rule>();
        List<List<int>> columns;

        public class Rule
        {
            public string name;
            public int r0, r1, r2, r3;

            public bool Contains(int n)
            {
                return (n >= r0 && n <= r1) || (n >= r2 && n <= r3);
            }
        }

        public override string P1()
        {
            int index = 0;
            while (!string.IsNullOrEmpty(Input[index]))
            {
                rules.Add(ParseRule(new Parser(Input[index++])));
            }

            index += 2;
            columns = Input[index].Split(",").Select(str => new List<int>() { int.Parse(str) }).ToList();

            int sum = 0;
            for (index += 3; index < Input.Length; ++index)
            {
                int[] numbers = Input[index].Split(",").Select(str => int.Parse(str)).ToArray();
                int[] bad = numbers.Where(num => rules.Where(r => r.Contains(num)).Count() == 0).ToArray();

                if (bad.Length > 0)
                {
                    sum += bad.Sum();
                }
                else
                {
                    for (int col = 0; col < columns.Count; ++col)
                        columns[col].Add(numbers[col]);
                }
            }

            return sum.ToString();
        }

        public override string P2()
        {
            long sum = 1;
            while (rules.Count > 0)
            {
                for (int col = 0; col < columns.Count; ++col)
                {
                    Rule[] possibleRules = rules.Where(r => columns[col].Where(n => !r.Contains(n)).Count() == 0).ToArray();
                    if (possibleRules.Length == 1)
                    {
                        if (possibleRules[0].name.Contains("departure")) 
                            sum *= columns[col][0];
                        columns.RemoveAt(col);
                        rules.Remove(possibleRules[0]);
                    }
                }
            }

            return sum.ToString();
        }

        private Rule ParseRule(Parser p)
        {
            Rule r = new Rule() { name = p.GetIdent() };
            while (p.PeekSymbol() == Symbol.Ident)
            {
                r.name = $"{r.name} {p.GetIdent()}";
            }

            p.Accept(Symbol.Colon);

            r.r0 = p.GetNumber();
            p.Burn();
            r.r1 = p.GetNumber();
            p.Burn();
            r.r2 = p.GetNumber();
            p.Burn();
            r.r3 = p.GetNumber();

            return r;
        }
    }
}

using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_16 : BetterBaseDay
    {
        class Sue
        {
            private Dictionary<string, int> props = new();

            public Sue(string line)
            {
                Parser p = new(line);

                p.Burn();
                Name = p.GetIdent();
                p.Burn();

                while (!p.Done())
                {
                    string key = p.GetIdent();
                    p.Burn();
                    int val = p.GetNumber();

                    p.Accept(Symbol.Comma);
                    props[key] = val;
                }
            }

            public bool Check(string key, int value)
            {
                if (!props.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return props[key] == value;
                }
            }

            public bool Greater(string key, int value)
            {
                if (!props.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return props[key] > value;
                }
            }

            public bool Lesser(string key, int value)
            {
                if (!props.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return props[key] < value;
                }
            }

            public string Name { get; }
        }

        public override string P1()
        {
            Sue[] sues = Input.Select(line => new Sue(line)).ToArray();

            List<Sue> check = sues
                .Where(s => s.Check("children", 3))
                .Where(s => s.Check("cats", 7))
                .Where(s => s.Check("samoyeds", 2))
                .Where(s => s.Check("pomeranians", 3))
                .Where(s => s.Check("akitas", 0))
                .Where(s => s.Check("vizslas", 0))
                .Where(s => s.Check("goldfish", 5))
                .Where(s => s.Check("trees", 3))
                .Where(s => s.Check("cars", 2))
                .Where(s => s.Check("perfumes", 1))
                .ToList() ; 

            return check[0].Name;
        }

        public override string P2()
        {
            Sue[] sues = Input.Select(line => new Sue(line)).ToArray();

            List<Sue> check = sues
                .Where(s => s.Check("children", 3))
                .Where(s => s.Greater("cats", 7))
                .Where(s => s.Check("samoyeds", 2))
                .Where(s => s.Lesser("pomeranians", 3))
                .Where(s => s.Check("akitas", 0))
                .Where(s => s.Check("vizslas", 0))
                .Where(s => s.Lesser("goldfish", 5))
                .Where(s => s.Greater("trees", 3))
                .Where(s => s.Check("cars", 2))
                .Where(s => s.Check("perfumes", 1))
                .ToList();

            return check[0].Name;
        }
    }
}

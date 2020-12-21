using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day_21 :BetterBaseDay
    {
        List<List<string>> foods = new List<List<string>>();
        Dictionary<string, Tuple<string, List<string>>> af = new Dictionary<string, Tuple<string, List<string>>>();

        public override string Solve_1()
        {
            foreach (string line in Input)
            {
                List<string> f = new List<string>();
                Parser p = new Parser(line);
                while (p.PeekSymbol() == Symbol.Ident)
                {
                    f.Add(p.GetIdent());
                }

                p.Burn(2);
                while (p.PeekSymbol() == Symbol.Ident)
                {
                    string aller = p.GetIdent();
                    if (af.ContainsKey(aller))
                        af[aller] = Tuple.Create((string)null, af[aller].Item2.Intersect(f).ToList());
                    else
                        af[aller] = Tuple.Create((string)null, f.ToList());
                    p.Burn();
                }

                foods.Add(f);
            }

            while (af.Values.Where(tup => string.IsNullOrEmpty(tup.Item1)).Count() > 0)
            {
                foreach (var entry in af)
                {
                    if (entry.Value.Item2?.Count == 1)
                    {
                        af[entry.Key] = Tuple.Create(entry.Value.Item2[0], (List<string>)null);
                        foods.ForEach(f => f.Remove(entry.Value.Item2[0]));
                        af.ForEach(a => a.Value.Item2?.Remove(entry.Value.Item2[0]));
                    }
                }
            }

            return foods.Select(f => f.Count()).Sum().ToString();
        }

        public override string Solve_2()
        {
            StringBuilder sb = new StringBuilder();

            af.Keys.OrderBy(str => str).ForEach(aller => sb.Append($"{af[aller].Item1},"));

            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }
    }
}

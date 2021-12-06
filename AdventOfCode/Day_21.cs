using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_21 : BetterBaseDay
    {
        List<List<string>> foods = new List<List<string>>();
        Dictionary<string, Tuple<string, List<string>>> allergenData = new Dictionary<string, Tuple<string, List<string>>>();

        public override string P1()
        {
            InputParsers.ForEach(parser =>
            {
                foods.Add(parser.AcceptIdents());
                parser.Burn(2);

                foreach (string allergen in parser.AcceptIdents(Symbol.Comma))
                {
                    if (allergenData.ContainsKey(allergen))
                        allergenData[allergen] = Tuple.Create((string)null, allergenData[allergen].Item2.Intersect(foods.Last()).ToList());
                    else
                        allergenData[allergen] = Tuple.Create((string)null, foods.Last().ToList());
                }
            });

            while (allergenData.Values.Where(tup => string.IsNullOrEmpty(tup.Item1)).Count() > 0)
            {
                foreach (var entry in allergenData)
                {
                    if (entry.Value.Item2?.Count == 1)
                    {
                        allergenData[entry.Key] = Tuple.Create(entry.Value.Item2[0], (List<string>)null);
                        foods.ForEach(f => f.Remove(entry.Value.Item2[0]));
                        allergenData.ForEach(a => a.Value.Item2?.Remove(entry.Value.Item2[0]));
                    }
                }
            }

            return foods.Select(f => f.Count()).Sum().ToString();
        }

        public override string P2()
        {
            return string.Join(",", allergenData.Keys.OrderBy(str => str).Select(allergen => allergenData[allergen].Item1));
        }
    }
}

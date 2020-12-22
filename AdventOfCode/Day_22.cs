using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_22 : BetterBaseDay
    {
        List<int> base1 = new List<int>();
        List<int> base2 = new List<int>();

        public override string Solve_1()
        {
            int nCards = (Input.Length - 3) / 2;
            base1 = Input.ToList().GetRange(1, nCards).Select(str => int.Parse(str)).ToList();
            base2 = Input.ToList().GetRange(Input.Length - nCards, nCards).Select(str => int.Parse(str)).ToList();

            var result = Combat(base1.ToList(), base2.ToList(), false);
            return Math.Max(result.Item1, result.Item2).ToString();
        }

        public override string Solve_2()
        {
            var result = Combat(base1.ToList(), base2.ToList(), true);
            return Math.Max(result.Item1, result.Item2).ToString();
        }

        private Tuple<long, long> Combat(List<int> deck1, List<int> deck2, bool recurse)
        {
            HashSet<string> previousDecks = new HashSet<string>();

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                string roundId = string.Join(",", deck1.ToArray()) + "=" + string.Join(",", deck2.ToArray());

                if (previousDecks.Contains(roundId))
                {
                    return Tuple.Create(1L, 0L);
                }

                previousDecks.Add(roundId);

                int c1 = deck1.First(); deck1.Remove(c1);
                int c2 = deck2.First(); deck2.Remove(c2);

                if (recurse && c1 <= deck1.Count && c2 <= deck2.Count)
                {
                    var result = Combat(deck1.GetRange(0, c1).ToList(), deck2.GetRange(0, c2).ToList(), recurse);

                    if (result.Item1 > result.Item2)
                    {
                        deck1.Add(c1);
                        deck1.Add(c2);
                    }
                    else
                    {
                        deck2.Add(c2);
                        deck2.Add(c1);
                    }
                }
                else if (c1 > c2)
                {
                    deck1.Add(c1);
                    deck1.Add(c2);
                }
                else
                {
                    deck2.Add(c2);
                    deck2.Add(c1);
                }
            }

            long mult1 = deck1.Count;
            long mult2 = deck2.Count;
            return Tuple.Create(deck1.Select(val => val * mult1--).Sum(), deck2.Select(val => val * mult2--).Sum());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AoCUtil;

namespace aoc2021
{
    public class Day_22 : BetterBaseDay
    {
        private static IntRange ParseRange(string str) => new IntRange(str.Split(".."));

        private class Rule
        {
            public IntRange x, y, z;
            public bool on;

            public Rule()
            { }

            public Rule(Rule other)
            {
                on = other.on;
                x = new(other.x);
                y = new(other.y);
                z = new(other.z);
            }

            public override string ToString()
            {
                return $"{x} -- {y} -- {z} -- {on}";
            }
        }

        private static Rule[] ParseRules(string[] input)
        {
            return input.Split(' ').Select(line =>
            {
                bool val = line[0] == "on";

                string[] parts = line[1].Split(',');
                IntRange xr = ParseRange(parts[0].Substring(2));
                IntRange yr = ParseRange(parts[1].Substring(2));
                IntRange zr = ParseRange(parts[2].Substring(2));

                return new Rule() { on = val, x = xr, y = yr, z = zr };
            }).ToArray();
        }

        public override string P1()
        {
            return "no";

            //Dictionary<string, bool> map = new();

            //ParseRules(Input).ForEach(r =>
            //{
            //    for (int x = r.x.Start; x <= r.x.End; ++x)
            //    {
            //        for (int y = r.y.Start; y <= r.y.End; ++y)
            //        {
            //            for (int z = r.z.Start; z <= r.z.End; ++z)
            //            {
            //                map[$"{x},{y},{z}"] = r.on;
            //            }
            //        }
            //    }
            //});

            //int sum = 0;
            //map.ForEach(v =>
            //{
            //    if (v.Value)
            //        ++sum;
            //});

            //return sum.ToString();
        }

        private static bool Contains(Rule left, Rule right)
        {
            return left.x.ContainsInclusive(right.x) &&
                left.y.ContainsInclusive(right.y) &&
                left.z.ContainsInclusive(right.z);
        }

        private static bool HasOverlap(Rule left, Rule right)
        {
            return left.x.Overlap(right.x) &&
                left.y.Overlap(right.y) &&
                left.z.Overlap(right.z);
        }

        private static List<Rule> Subdivide(Rule newRule, Rule oldRule)
        {
            Rule split = new(newRule);

            if (!oldRule.x.ContainsInclusive(newRule.x))
            {
                // Split x

                if (newRule.x.Start < oldRule.x.Start)
                {
                    newRule.x.Start = oldRule.x.Start;
                    split.x.End = newRule.x.Start - 1;
                }
                else
                {
                    newRule.x.End = oldRule.x.End;
                    split.x.Start = newRule.x.End + 1;
                }
            }
            else if (!oldRule.y.ContainsInclusive(newRule.y))
            {
                // Split y

                if (newRule.y.Start < oldRule.y.Start)
                {
                    newRule.y.Start = oldRule.y.Start;
                    split.y.End = newRule.y.Start - 1;
                }
                else
                {
                    newRule.y.End = oldRule.y.End;
                    split.y.Start = newRule.y.End + 1;
                }
            }
            else
            {
                // Split z

                if (newRule.z.Start < oldRule.z.Start)
                {
                    newRule.z.Start = oldRule.z.Start;
                    split.z.End = newRule.z.Start - 1;
                }
                else
                {
                    newRule.z.End = oldRule.z.End;
                    split.z.Start = newRule.z.End + 1;
                }
            }

            return new() { newRule, split };
        }

        private static bool Filter(List<Rule> newRules, List<Rule> rules)
        {
            for (int index = 0; index < newRules.Count; )
            {
                Rule nr = newRules[index];

                bool found = false;

                rules.ForEachBreakable(r =>
                {
                    if (HasOverlap(nr, r))
                    {
                        found = true;

                        newRules.Remove(nr);

                        if (!Contains(r, nr))
                        {
                            newRules.AddRange(Subdivide(nr, r).Where(ret => !Contains(r, ret)));
                        }

                        return false;
                    }

                    return true;
                });

                if (!found)
                    ++index;
            }

            return false;
        }

        private static ulong Count(Rule r)
        {
            if (!r.on) return 0;

            return (ulong)r.x.Length * (ulong)r.y.Length * (ulong)r.z.Length;
        }

        public override string P2()
        {
            Rule[] inputRules = ParseRules(Input).Reverse().ToArray();

            List<Rule> rules = new();

            inputRules.ForEach(next =>
            {
                List<Rule> set = new() { next };

                while (true)
                {
                    if (!Filter(set, rules))
                        break;
                }

                rules.AddRange(set);
            });

            ulong sum = 0;
            rules.ForEach(r => sum += Count(r));
            return sum.ToString();
        }
    }
}

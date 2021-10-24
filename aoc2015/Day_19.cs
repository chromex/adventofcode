using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_19 : BetterBaseDay
    {
        private List<Tuple<string, string>> GetSubs()
        {
            List<Tuple<string, string>> res = new();

            Input.Where(line => line.Contains("=>")).OrderByDescending(line => line.Length).ForEach(line =>
            {
                string[] spl = line.Split(" ");
                res.Add(new(spl[0], spl[2]));
            });

            return res;
        }

        private string Molecule => Input[Input.Length - 1];

        private static string ReplaceAt(string molecule, string sub, int index, int len)
        {
            string before = molecule.Substring(0, index);
            string after = molecule.Substring(index + len);
            return $"{before}{sub}{after}";
        }

        public override string Solve_1()
        {
            HashSet<string> variants = new();

            foreach (var entry in GetSubs())
            {
                for (int index = 0; index < Molecule.Length; )
                {
                    int pos = Molecule.IndexOf(entry.Item1, index);

                    if (pos >= 0)
                    {
                        variants.Add(ReplaceAt(Molecule, entry.Item2, pos, entry.Item1.Length));
                        index = pos + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return variants.Count.ToString();
        }

        private int Quest(string current, int depth, List<Tuple<string, string>> subs)
        {
            if (current == "e")
            {
                return depth;
            }

            foreach (var entry in subs)
            {
                for (int index = 0; index < current.Length;)
                {
                    int pos = current.IndexOf(entry.Item2, index);

                    if (pos >= 0)
                    {
                        int res = Quest(ReplaceAt(current, entry.Item1, pos, entry.Item2.Length), depth + 1, subs);
                        if (res >= 0) return res;

                        index = pos + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return -1;
        }

        public override string Solve_2()
        {
            return Quest(Molecule, 0, GetSubs()).ToString();
        }
    }
}

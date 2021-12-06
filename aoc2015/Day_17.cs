using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_17 : BetterBaseDay
    {
        private int[] GetContainers() => Input.Select(line => int.Parse(line)).OrderBy(v => v).ToArray();

        private Dictionary<string, int> Combos = new();

        private void Scan(int[] containers, int baseIndex, List<int> combo)
        {
            for (int index = baseIndex; index < containers.Length; ++index)
            {
                combo.Add(containers[index]);

                int sum = combo.Sum();
                if (sum == 150)
                {
                    Combos[string.Join("-", combo)] = Combos.GetValueOrDefault(string.Join("-", combo)) + 1;
                }
                else if (sum > 150)
                {
                    combo.RemoveAt(combo.Count - 1);
                    break;
                }
                else
                {
                    Scan(containers, index + 1, combo);
                }

                combo.RemoveAt(combo.Count - 1);
            }
        }

        public override string P1()
        {
            Scan(GetContainers(), 0, new List<int>());

            return Combos.Values.Sum().ToString();
        }

        public override string P2()
        {
            int shortest = int.MaxValue;
            Combos.ForEach(kvp => shortest = Math.Min(kvp.Key.Split("-").Count(), shortest));

            int sum = 0;
            Combos.ForEach(kvp =>
            {
                if (shortest == kvp.Key.Split("-").Count())
                {
                    sum += kvp.Value;
                }
            });

            return sum.ToString();
        }
    }
}

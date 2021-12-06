using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_24 : BetterBaseDay
    {
        private static int Sum(List<int> packages) 
        {
            int sum = 0;
            for (int index = 0; index < packages.Count; ++index)
            {
                sum += packages[index];
            }
            return sum;
        }

        private bool SearchGroup2(int[] packages, int target, List<int> groupTwo)
        {
            for (int index = 0; index < packages.Length; ++index)
            {
                int package = packages[index];

                if (groupTwo.Contains(package)) continue;

                groupTwo.Add(package);

                int sum = Sum(groupTwo);
                if (sum == target)
                {
                    return true;
                }
                else if (sum < target)
                {
                    if (SearchGroup2(packages, target, groupTwo))
                    {
                        return true;
                    }
                }

                groupTwo.Remove(package);
            }

            return false;
        }

        private int _minP = int.MaxValue;
        private ulong _minQE = int.MaxValue;

        private void Search(int[] packages, int target, List<int> groupOne)
        {
            if (groupOne.Count > (_minP - 1)) return;

            for (int index = groupOne.Count; index < packages.Length; ++index)
            {
                int package = packages[index];

                if (groupOne.Contains(package)) continue;

                groupOne.Add(package);

                int sum = Sum(groupOne);
                if (sum == target)
                {
                    int[] remainder = packages.Where(p => !groupOne.Contains(p)).ToArray();

                    if (SearchGroup2(remainder, target, new()))
                    {
                        ulong qe = 1;
                        groupOne.ForEach(p => qe *= (ulong)p);
                        _minQE = Math.Min(qe, _minQE);

                        if (groupOne.Count < _minP)
                        {
                            _minP = groupOne.Count;
                            _minQE = qe;
                        }
                        else if (qe < _minQE)
                        {
                            _minQE = qe;
                        }
                    }
                }
                else if (sum < target)
                {
                    Search(packages, target, groupOne);
                }

                groupOne.Remove(package);
            }
        }

        public override string P1()
        {
            int[] packages = Input.Select(line => int.Parse(line)).OrderByDescending(i => i).ToArray();

            int target = packages.Sum() / 3;

            Search(packages, target, new());

            return _minQE.ToString();
        }

        private static void CollectSets(FastList<int> packages, int target, List<int> group, List<int[]> collection)
        {
            FastList<int> workingSet = new(packages);

            packages.ForEach(package =>
            {
                workingSet = workingSet.Without(package);

                group.Add(package);

                int sum = Sum(group);
                if (sum == target)
                {
                    collection.Add(group.ToArray());
                }
                else if (sum < target)
                {
                    CollectSets(workingSet, target, group, collection);
                }

                group.Remove(package);
            });
        }

        private static ulong Product(int[] vals)
        {
            ulong sum = 1;
            for (int i = 0; i < vals.Length; ++i)
            {
                sum *= (ulong)vals[i];
            }
            return sum;
        }

        private static bool Overlap(int[] p, int[] j)
        {
            for (int ip = 0; ip < p.Length; ++ip)
            {
                for (int ij = 0; ij < j.Length; ++ij)
                {
                    if (p[ip] == j[ij])
                        return true;
                }
            }

            return false;
        }

        public override string P2()
        {
            FastList<int> packages = new(Input.Select(line => int.Parse(line)).OrderByDescending(i => i).ToArray());

            int target = packages.Sum() / 4;

            List<int[]> sets = new();
            CollectSets(packages, target, new(), sets);
            FastList<int[]> allSets = new(sets.OrderBy(s => s.Length).ToArray());

            int shortestFound = int.MaxValue;
            ulong minQE = int.MaxValue;

            allSets.ForEachBreakable(group1 =>
            {
                if (group1.Length > shortestFound)
                {
                    return false;
                }

                // Remove all sets that overlap
                FastList<int[]> subset1 = allSets.Without(set => Overlap(group1, set));

                bool searching = true;

                // For each sets in the non-overlapping group... (group2)
                subset1.ForEachBreakable(group2 =>
                {
                    // Remove all sets that overlap
                    FastList<int[]> subset2 = subset1.Without(set => Overlap(group2, set));

                    subset2.ForEachBreakable(group3 =>
                    {
                        FastList<int[]> final = subset2.Without(set => Overlap(group3, set));

                        if (final.Length > 0)
                        {
                            //Console.WriteLine($"{string.Join(" ", group1)} = {Product(group1)}");
                            shortestFound = group1.Length;
                            minQE = Math.Min(minQE, Product(group1));
                            searching = false;
                        }

                        return searching;
                    });

                    return searching;
                });

                return true;
            });

            return minQE.ToString();
        }
    }
}

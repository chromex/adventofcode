using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_19 : BetterBaseDay
    {
        class Scanner
        {
            public int id;
            public List<Vec3> beacons = new();
            public bool marked;
            public Vec3 position = new();

            public void RotateX() => beacons.ForEach(v => v.RotateAroundX());
            public void RotateY() => beacons.ForEach(v => v.RotateAroundY());
            public void RotateZ() => beacons.ForEach(v => v.RotateAroundZ());
        }

        private static List<Scanner> Load(string[] input)
        {
            List<Scanner> res = new();

            Scanner current = null;
            input.ForEach(line =>
            {
                if (current == null)
                {
                    current = new();
                    res.Add(current);
                    string[] spl = line.Split(' ');
                    current.id = spl[2].AsInt();
                }
                else if (line.Contains(','))
                {
                    current.beacons.Add(new(line));
                }
                else
                {
                    current = null;
                }
            });

            return res;
        }

        private static bool Intersect(List<Vec3> leftBeacons, List<Vec3> rightBeacons, Vec3 offset)
        {
            int sum = 0;

            for (int i = 0; i < leftBeacons.Count; ++i)
            {
                // Early out if it is impossible to find enough beacons matching
                if (leftBeacons.Count - i + sum < 12)
                    return false;

                Vec3 l = leftBeacons[i];

                for (int j = 0; j < rightBeacons.Count; ++j)
                {
                    Vec3 r = rightBeacons[j];

                    if (l.X == (r.X + offset.X) &&
                        l.Y == (r.Y + offset.Y) &&
                        l.Z == (r.Z + offset.Z))
                    {
                        ++sum;
                        break;
                    }
                }
            }

            return sum >= 12;
        }

        private static bool Overlap(Scanner left, Scanner right, out Vec3 offset)
        {
            offset = new();

            for (int i = 0; i < left.beacons.Count; ++i)
            {
                Vec3 l = left.beacons[i];

                for (int j = 0; j < right.beacons.Count; ++j)
                {
                    Vec3 r = right.beacons[j];
                    offset = l - r;

                    if (Intersect(left.beacons, right.beacons, offset))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool FindOverlap(Scanner left, Scanner right, HashSet<Vec3> uniques)
        {
            Vec3 cardinal = new(7, 15, 19);
            HashSet<Vec3> visited = new();

            for (int x = 0; x < 4; ++x)
            {
                for (int y = 0; y < 4; ++y)
                {
                    for (int z = 0; z < 4; ++z)
                    {
                        if (visited.Contains(cardinal))
                            continue;

                        visited.Add(new Vec3(cardinal));

                        Vec3 offset;
                        if (Overlap(left, right, out offset))
                        {
                            Console.Write(".");
                            right.beacons = right.beacons.Select(p => p + offset).ToList();
                            right.beacons.ForEach(p => uniques.Add(p));
                            right.marked = true;
                            right.position = offset;
                            return true;
                        }

                        right.RotateZ();
                        cardinal.RotateAroundZ();
                    }

                    right.RotateY();
                    cardinal.RotateAroundY();
                }

                right.RotateX();
                cardinal.RotateAroundX();
            }

            return false;
        }

        List<Scanner> scanners;

        public override string P1()
        {
            scanners = Load(Input);

            HashSet<Vec3> uniques = new();
            scanners[0].beacons.ForEach(p => uniques.Add(p));
            scanners[0].marked = true;

            while (scanners.Any(s => !s.marked))
            {
                foreach (Scanner right in scanners.Where(s => !s.marked))
                {
                    foreach (Scanner left in scanners.Where(s => s.marked))
                    {
                        if (FindOverlap(left, right, uniques))
                        {
                            Console.WriteLine($"Overlap: {left.id} {right.id}");
                            break;
                        }
                    }
                }
            }

            return uniques.Count.ToString();
        }

        public override string P2()
        {
            int max = 0;

            for (int i = 0; i < scanners.Count - 1; ++i)
            {
                for (int j = i + 1; j < scanners.Count; ++j)
                {
                    max = Math.Max(max, (scanners[i].position - scanners[j].position).Manhattan);
                }
            }

            return max.ToString();
        }
    }
}

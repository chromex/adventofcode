using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_17 : BetterBaseDay
    {
        const long size = 20;
        Dictionary<long, bool> space = new Dictionary<long, bool>();

        private long GetIndex(int x, int y, int z, int w)
        {
            return (w * size * size * size) + (z * size * size) + (y * size) + x;
        }

        private void Set(Dictionary<long, bool> sp, int x, int y, int z, int w, bool val)
        {
            sp[GetIndex(x, y, z, w)] = val;
        }

        private bool Test(int x, int y, int z, int w)
        {
            long index = GetIndex(x, y, z, w);
            return space.ContainsKey(index) ? space[index] : false;
        }

        public override string Solve_1()
        {
            for (int y = 0; y < Input.Length; ++y)
                for (int x = 0; x < Input[y].Length; ++x)
                    if (Input[y][x] == '#')
                        Set(space, 10 + x, 10 + y, 10, 10, true);

            IEnumerable<int> mapRange = Enumerable.Range(0, (int)size);
            IEnumerable<int> deltaRange = Enumerable.Range(-1, 3);

            for (int count = 0; count < 6; ++count)
            {
                Dictionary<long, bool> copy = space.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                foreach (int w in mapRange) foreach (int z in mapRange) foreach (int y in mapRange) foreach (int x in mapRange)
                {
                    int sum = 0;

                    foreach (int dw in deltaRange) foreach (int dz in deltaRange) foreach (int dy in deltaRange) foreach (int dx in deltaRange) 
                    {
                        if (dx == 0 && dy == 0 && dz == 0 && dw == 0) continue;

                        if (Test(x + dx, y + dy, z + dz, w + dw))
                        {
                            ++sum;
                        }
                    }

                    if (Test(x, y, z, w))
                        Set(copy, x, y, z, w, sum == 2 || sum == 3);
                    else if (sum == 3)
                        Set(copy, x, y, z, w, true);
                }

                space = copy;
            }

            return space.Where(kvp => kvp.Value).Count().ToString();
        }

        public override string Solve_2()
        {
            // Note: just updated Solve_1 to do part 2. Didn't want to mess with supporting 3 and 4 dimensions.
            return "err";
        }
    }
}

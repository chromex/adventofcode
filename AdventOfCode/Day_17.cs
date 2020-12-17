using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_17 : BetterBaseDay
    {
        const int size = 20;
        BitArray space = new BitArray(size * size * size * size);
        IntRange xDim = new IntRange(size / 2, size / 2), yDim = new IntRange(size / 2, size / 2), zDim = new IntRange(size / 2, size / 2), wDim = new IntRange(size / 2, size / 2);

        private int GetIndex(int x, int y, int z, int w)
        {
            return (w * size * size * size) + (z * size * size) + (y * size) + x;
        }

        private void Set(BitArray sp, int x, int y, int z, int w, bool val)
        {
            sp[GetIndex(x, y, z, w)] = val;
            xDim.Expand(x);
            yDim.Expand(y);
            zDim.Expand(z);
            wDim.Expand(w);
        }

        private bool Test(int x, int y, int z, int w)
        {
            return space[GetIndex(x, y, z, w)];
        }

        public override string Solve_1()
        {
            for (int y = 0; y < Input.Length; ++y)
                for (int x = 0; x < Input[y].Length; ++x)
                    if (Input[y][x] == '#')
                        Set(space, 6 + x, 6 + y, 10, 10, true);

            IEnumerable<int> deltaRange = Enumerable.Range(-1, 3);

            for (int count = 0; count < 6; ++count)
            {
                BitArray copy = new BitArray(space);

                xDim.Expand(xDim.Start - 1);
                xDim.Expand(xDim.End + 1);
                yDim.Expand(yDim.Start - 1);
                yDim.Expand(yDim.End + 1);
                zDim.Expand(zDim.Start - 1);
                zDim.Expand(zDim.End + 1);
                wDim.Expand(wDim.Start - 1);
                wDim.Expand(wDim.End + 1);

                foreach (int w in wDim.Range()) foreach (int z in zDim.Range()) foreach (int y in yDim.Range()) foreach (int x in xDim.Range())
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

            int total = 0;
            foreach (var bit in space)
            {
                if ((bool)bit) ++total;
            }
            return total.ToString();
        }

        public override string Solve_2()
        {
            // Note: just updated Solve_1 to do part 2. Didn't want to mess with supporting 3 and 4 dimensions.
            return "err";
        }
    }
}

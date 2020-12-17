using System.Collections;

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

                for (int w = wDim.Start; w <= wDim.End; ++w) for (int z = zDim.Start; z <= zDim.End; ++z) for (int y = yDim.Start; y <= yDim.End; ++y) for (int x = xDim.Start; x <= xDim.End; ++x)
                {
                    int sum = 0;

                    for (int dw = w - 1; dw < w + 2; ++dw) for (int dz = z - 1; dz < z + 2; ++dz) for (int dy = y - 1; dy < y + 2; ++dy) for (int dx = x - 1; dx < x + 2; ++dx)
                    {
                        if (!(dx == x && dy == y && dz == z && dw == w) && Test(dx, dy, dz, dw))
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

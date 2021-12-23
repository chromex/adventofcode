using AoCUtil;
using System;
using System.Text;

namespace aoc2021
{
    class Day_20 : BetterBaseDay
    {
        private static Matrix<char> Apply(Matrix<char> map, string conversion)
        {
            Matrix<char> ret = new(map.Width, map.Height, '.');

            char def = map.Data[0, 0] == '#' ? '.' : '#';

            ret.ForEachCoord((x, y) =>
            {
                if (x == 0 || x == map.Width - 1 || y == 0 || y == map.Height - 1)
                {
                    ret.Data[x, y] = def;
                    return;
                }

                StringBuilder binary = new();
                binary.Append(map.Data[x - 1, y - 1] == '#' ? 1 : 0);
                binary.Append(map.Data[x, y - 1] == '#' ? 1 : 0);
                binary.Append(map.Data[x + 1, y - 1] == '#' ? 1 : 0);

                binary.Append(map.Data[x - 1, y] == '#' ? 1 : 0);
                binary.Append(map.Data[x, y] == '#' ? 1 : 0);
                binary.Append(map.Data[x + 1, y] == '#' ? 1 : 0);

                binary.Append(map.Data[x - 1, y + 1] == '#' ? 1 : 0);
                binary.Append(map.Data[x, y + 1] == '#' ? 1 : 0);
                binary.Append(map.Data[x + 1, y + 1] == '#' ? 1 : 0);

                ret.Data[x, y] = conversion[(int)Util.ParseBinary(binary.ToString())];
            });

            return ret;
        }

        public override string P1()
        {
            string conversion = Input[0];
            Matrix<char> map = new(120, 120, '.');

            for (int l = 2; l < Input.Length; ++l)
            {
                for (int ch = 0; ch < Input[l].Length; ++ch)
                {
                    map.Data[10 + ch, 8 + l] = Input[l][ch];
                }
            }

            map = Apply(map, conversion);
            map = Apply(map, conversion);

            int sum = 0;
            for (int x = 3; x < map.Width - 3; ++x)
            {
                for (int y = 3; y < map.Height - 3; ++y)
                {
                    if (map.Data[x, y] == '#') ++sum;
                }
            }

            return sum.ToString();
        }

        public override string P2()
        {
            string conversion = Input[0];
            Matrix<char> map = new(250, 250, '.');

            for (int l = 2; l < Input.Length; ++l)
            {
                for (int ch = 0; ch < Input[l].Length; ++ch)
                {
                    map.Data[75 + ch, 73 + l] = Input[l][ch];
                }
            }

            for (int i = 0; i < 50; ++i) map = Apply(map, conversion);

            int sum = 0;
            for (int x = 3; x < map.Width - 3; ++x)
            {
                for (int y = 3; y < map.Height - 3; ++y)
                {
                    if (map.Data[x, y] == '#') ++sum;
                }
            }

            return sum.ToString();
        }
    }
}

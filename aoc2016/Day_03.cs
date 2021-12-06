using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    class Day_03 : BetterBaseDay
    {
        public override string P1()
        {
            int n = 0;
            Input.Select(line => line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)).ForEach(spl =>
            {
                int[] sides = spl.Select(ent => int.Parse(ent)).OrderBy(v => v).ToArray();

                if (sides[0] + sides[1] > sides[2])
                    ++n;
            });
            return n.ToString();
        }

        private static bool Valid(string[][] input, int row, int col)
        {
            List<int> set = new();
            set.Add(int.Parse(input[row][col]));
            set.Add(int.Parse(input[row + 1][col]));
            set.Add(int.Parse(input[row + 2][col]));
            set = set.OrderBy(v => v).ToList();

            return set[0] + set[1] > set[2];
        }

        public override string P2()
        {
            string[][] input = Input.Select(line => line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)).ToArray();

            int n = 0;
            for (int row = 0; row < Input.Length; row += 3)
            {
                if (Valid(input, row, 0)) ++n;
                if (Valid(input, row, 1)) ++n;
                if (Valid(input, row, 2)) ++n;
            }
            return n.ToString();
        }
    }
}

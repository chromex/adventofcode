using AoCHelper;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private Map map;

        public Day_03()
        {
            map = new Map(InputFilePath);
        }

        public override string Solve_1()
        {
            return $"{SumTrees(map, 3, 1)}";
        }

        public override string Solve_2()
        {
            int v1 = SumTrees(map, 1, 1);
            int v2 = SumTrees(map, 3, 1);
            int v3 = SumTrees(map, 5, 1);
            int v4 = SumTrees(map, 7, 1);
            int v5 = SumTrees(map, 1, 2);

            return $"{v1 * v2 * v3 * v4 * v5}";
        }

        private int SumTrees(Map m, int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int sumTrees = 0;

            while (!m.IsLocationOffMap(y))
            {
                if (!m.IsLocationClear(x, y))
                {
                    ++sumTrees;
                }

                x += dx;
                y += dy;
            }

            return sumTrees;
        }

        private class Map
        {
            private string[] rows;

            public Map(string path)
            {
                rows = System.IO.File.ReadAllLines(path).Where(line => !string.IsNullOrEmpty(line)).ToArray();
            }

            public bool IsLocationClear(int x, int y)
            {
                Debug.Assert(!IsLocationOffMap(y));

                string row = rows[y];
                char val = row[x % row.Length];

                return val == '.';
            }

            public bool IsLocationOffMap(int y)
            {
                return y >= rows.Length;
            }
        }
    }
}
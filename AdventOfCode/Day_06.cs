using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private string[] lines;

        public Day_06()
        {
            lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            List<string> records = Util.ParseRecords(lines, string.Empty);

            return $"{records.Select(str => str.Distinct().Count()).Sum()}";
        }

        public override string Solve_2()
        {
            var sets = Util.ParseSets(lines);

            int sum = 0;
            foreach (List<string> set in sets)
            {
                string ans = set[0];

                foreach (string s in set)
                {
                    ans = new string(ans.Intersect(s).ToArray());
                }

                sum += ans.Length;
            }

            return $"{sum}";
        }
    }
}

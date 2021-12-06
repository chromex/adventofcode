using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_06 : BetterBaseDay
    {
        public Day_06()
        {}

        public override string P1()
        {
            List<string> records = Util.ParseRecords(Input, string.Empty);

            return $"{records.Select(str => str.Distinct().Count()).Sum()}";
        }

        public override string P2()
        {
            var sets = Util.ParseSets(Input);

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

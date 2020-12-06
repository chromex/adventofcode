using AoCHelper;
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
            int sum = 0;
            string ans = string.Empty;

            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    sum += ans.Distinct().Count();
                    ans = string.Empty;
                }
                else
                {
                    ans = ans + line;
                }
            }

            sum += ans.Distinct().Count();

            return $"{sum}";
        }

        public override string Solve_2()
        {
            int sum = 0;
            string ans = string.Empty;
            bool fresh = true;

            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    sum += ans.Length;
                    ans = string.Empty;
                    fresh = true;
                }
                else
                {
                    if (fresh)
                    {
                        ans = line;
                        fresh = false;
                    }
                    else
                    {
                        ans = new string(ans.Intersect(line).ToArray());
                    }
                }
            }

            sum += ans.Length;

            return $"{sum}";
        }
    }
}

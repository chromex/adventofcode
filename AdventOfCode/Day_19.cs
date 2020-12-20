using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_19 : BetterBaseDay
    {
        private string[] rules;

        public override string Solve_1()
        {
            rules = Input
                .Where(line => line.Contains(":"))
                .Select(line =>
                {
                    int idx = line.IndexOf(":");
                    return Tuple.Create(int.Parse(line.Substring(0, idx)), line.Substring(idx + 2));
                })
                .OrderBy(rule => rule.Item1)
                .Select(rule => rule.Item2)
                .ToArray();

            return Input.Where(line => !line.Contains(":") && !string.IsNullOrEmpty(line) && Check(line, rules[0]) == line.Length).Count().ToString();
        }

        private int Check(string line, string rule)
        {
            int pipeIndex = rule.IndexOf("|");

            if (pipeIndex >= 0)
            {
                int left = Check(line, rule.Substring(0, pipeIndex - 1));
                if (left > 0)
                    return left;
                else
                    return Check(line, rule.Substring(pipeIndex + 2, rule.Length - pipeIndex - 2));
            }
            else
            {
                string[] split = rule.Split(" ");

                if (split.Length == 1)
                {
                    if (split[0][0] == '"')
                    {
                        return line[0] == split[0][1] ? 1 : 0;
                    }
                    else
                    {
                        return Check(line, rules[int.Parse(split[0])]);
                    }
                }
                else
                {
                    int amt = 0;
                    for (int idx = 0; idx < split.Length; ++idx)
                    {
                        if (amt >= line.Length)
                            return 0;

                        int c = Check(line.Substring(amt), split[idx]);
                        if (c == 0)
                            return 0;
                        else
                            amt += c;
                    }
                    return amt;
                }
            }
        }

        private int Override(string line)
        {
            Dictionary<int, int> count = new Dictionary<int, int>() { { 42, 0 }, { 31, 0 } };
            int rule = 42;
            int offset = 0;

            while (offset < line.Length)
            {
                int r = Check(line.Substring(offset), rules[rule]);
                if (r > 0)
                {
                    ++count[rule];
                    offset += r;
                }
                else if (rule == 42)
                {
                    rule = 31;
                }
                else
                {
                    break;
                }
            }

            if (count[31] > 0 && count[42] > 1 && count[31] < count[42])
            {
                return offset;
            }

            return 0;
        }

        public override string Solve_2()
        {
            return Input.Where(line => !line.Contains(":") && !string.IsNullOrEmpty(line) && Override(line) == line.Length).Count().ToString();
        }
    }
}

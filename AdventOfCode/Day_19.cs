using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_19 : BetterBaseDay
    {
        private Dictionary<int, string> rules;

        public override string P1()
        {
            rules = Input
                .Where(line => line.Contains(":"))
                .ToDictionary(
                    line => int.Parse(line.Substring(0, line.IndexOf(":"))), 
                    line => line.Substring(line.IndexOf(":") + 2));

            return Input.Where(line => !line.Contains(":") && !string.IsNullOrEmpty(line) && Check(line, rules[0]) == line.Length).Count().ToString();
        }

        private int Check(string line, string rule)
        {
            int pipeIndex = rule.IndexOf("|");

            if (pipeIndex >= 0)
            {
                return Math.Max(
                    Check(line, rule.Substring(0, pipeIndex - 1)), 
                    Check(line, rule.Substring(pipeIndex + 2, rule.Length - pipeIndex - 2)));
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

                    return Check(line, rules[int.Parse(split[0])]);
                }
                else
                {
                    int amt = 0, c;
                    for (int idx = 0; idx < split.Length; ++idx)
                    {
                        if (amt >= line.Length)
                            return 0;

                        amt += c = Check(line.Substring(amt), split[idx]);

                        if (c == 0)
                            return 0;
                    }
                    return amt;
                }
            }
        }

        private int Override(string line)
        {
            Dictionary<int, int> count = new Dictionary<int, int>() { { 42, 0 }, { 31, 0 } };
            int rule = 42, offset = 0, r;

            while (offset < line.Length)
            {
                offset += r = Check(line.Substring(offset), rules[rule]);
                if (r > 0)
                {
                    ++count[rule];
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

            return (count[31] > 0 && count[31] < count[42]) ? offset : 0;
        }

        public override string P2()
        {
            return Input.Where(line => !line.Contains(":") && !string.IsNullOrEmpty(line) && Override(line) == line.Length).Count().ToString();
        }
    }
}

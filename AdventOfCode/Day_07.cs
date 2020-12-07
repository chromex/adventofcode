using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_07 : BetterBaseDay
    {
        Dictionary<string, Rule> rules = new Dictionary<string, Rule>();

        public Day_07()
        {
            foreach (string line in Input)
            {
                Rule r = ParseRule(line);
                rules.Add(r.bagColor, r);
            }
        }

        private Rule ParseRule(string line)
        {
            Parser p = new Parser(line);
            Rule rule = new Rule();

            rule.bagColor = $"{p.GetIdent()} {p.GetIdent()}";
            p.Burn(2);

            while (!p.Done() && p.PeekIdent() != "no")
            {
                rule.countColors.Add(p.GetNumber());
                rule.contentColors.Add($"{p.GetIdent()} {p.GetIdent()}");
                p.Burn(2);
            }

            return rule;
        }

        public override string Solve_1()
        {
            int sum = 0;

            foreach (string key in rules.Keys)
            {
                if (key == "shiny gold")
                    continue;

                if (RuleContainsBag(rules[key], "shiny gold"))
                    ++sum;
            }

            return $"{sum}";
        }

        private bool RuleContainsBag(Rule r, string name)
        {
            if (r.contentColors.Contains(name))
                return true;

            foreach (string content in r.contentColors)
            {
                if (RuleContainsBag(rules[content], name))
                    return true;
            }

            return false;
        }

        public override string Solve_2()
        {
            int sum = SumBags(rules["shiny gold"]);
            return $"{sum - 1}";
        }

        private int SumBags(Rule r)
        {
            int sum = 1;

            for(int index = 0; index < r.contentColors.Count; ++index)
            {
                sum += r.countColors[index] * SumBags(rules[r.contentColors[index]]);
            }

            return sum;
        }

        class Rule
        {
            public string bagColor;

            public List<string> contentColors  = new List<string>();
            public List<int> countColors = new List<int>();
        }
    }
}

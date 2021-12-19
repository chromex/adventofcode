using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_12 : BetterBaseDay
    {
        private static Dictionary<string, List<string>> Load(string[] input)
        {
            Dictionary<string, List<string>> map = new();

            input.Split('-').ForEach(parts =>
            {
                if (!map.ContainsKey(parts[0])) 
                    map[parts[0]] = new();
                if (!map.ContainsKey(parts[1]))
                    map[parts[1]] = new();

                map[parts[0]].Add(parts[1]);
                map[parts[1]].Add(parts[0]);
            });

            return map;
        }

        private static bool IsSmall(string cave) => !char.IsUpper(cave[0]);
        private static bool HasVisited(string route, string cave) => route.Split(',').Contains(cave);

        private static bool CanVisit(string route, string cave)
        {
            return !IsSmall(cave) || !HasVisited(route, cave);
        }

        private static bool CanVisit2(string route, string cave)
        {
            if (!IsSmall(cave))
                return true;

            if (cave == "start")
                return false;

            bool hasDouble = route.Split(',').Where(c => IsSmall(c)).GroupBy(c => c).Where(g => g.Count() > 1).Any();

            if (!hasDouble) 
                return true;

            return !HasVisited(route, cave);
        }

        private static void Navigate(Dictionary<string, List<string>> map, string current, HashSet<string> routes, Func<string, string, bool> pred)
        {
            string c = current.Split(',').Last();

            if (c == "end")
            {
                routes.Add(current);
                return;
            }

            List<string> exits = map[c];

            foreach (string e in exits)
            {
                if (!pred(current, e))
                    continue;

                Navigate(map, $"{current},{e}", routes, pred);
            }
        }

        public override string P1()
        {
            Dictionary<string, List<string>> map = Load(Input);

            HashSet<string> routes = new();

            Navigate(map, "start", routes, CanVisit);

            return routes.Count.ToString();
        }

        public override string P2()
        {
            Dictionary<string, List<string>> map = Load(Input);

            HashSet<string> routes = new();

            Navigate(map, "start", routes, CanVisit2);

            return routes.Count.ToString();
        }
    }
}

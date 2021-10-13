using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_06 : BetterBaseDay
    {
        private Dictionary<string, bool> map = new();
        private Dictionary<string, int> map2 = new();

        private void Apply(Action<Vec2> f, Vec2 min, Vec2 max)
        {
            Vec2 v = new();

            for (int x = min.X; x <= max.X; ++x)
            {
                for (int y = min.Y; y <= max.Y; ++y)
                {
                    v.X = x;
                    v.Y = y;
                    f(v);
                }
            }
        }

        private void Inc(Vec2 min, Vec2 max) => Apply(v => map2[v.ToString()] = map2.GetValueOrDefault(v.ToString()) + 1, min, max);
        private void Dec(Vec2 min, Vec2 max) => Apply(v => map2[v.ToString()] = Math.Max(map2.GetValueOrDefault(v.ToString()) - 1, 0), min, max);

        private void TurnOn(Vec2 min, Vec2 max) => Apply(v => map[v.ToString()] = true, min, max);
        private void TurnOff(Vec2 min, Vec2 max) => Apply(v => map[v.ToString()] = false, min, max);
        private void Toggle(Vec2 min, Vec2 max) => Apply(v => map[v.ToString()] = !map.GetValueOrDefault(v.ToString()), min, max);

        private void Do(string str)
        {
            string[] spl = str.Split(" ");

            Vec2 min = new(spl[spl.Length - 3]);
            Vec2 max = new(spl[spl.Length - 1]);

            if (spl.Length == 5)
            {
                if (spl[1] == "on") TurnOn(min, max);
                else TurnOff(min, max);
            }
            else
            {
                Toggle(min, max);
            }
        }

        public override string Solve_1()
        {
            Input.ForEach(line => Do(line));

            return map.Values.Count(b => b).ToString();
        }

        private void Do2(string str)
        {
            string[] spl = str.Split(" ");

            Vec2 min = new(spl[spl.Length - 3]);
            Vec2 max = new(spl[spl.Length - 1]);

            if (spl.Length == 5)
            {
                if (spl[1] == "on") Inc(min, max);
                else Dec(min, max);
            }
            else
            {
                Inc(min, max);
                Inc(min, max);
            }
        }

        public override string Solve_2()
        {
            Input.ForEach(line => Do2(line));

            return map2.Values.Sum().ToString();
        }
    }
}

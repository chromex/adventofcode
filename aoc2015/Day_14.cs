using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_14 : BetterBaseDay
    {
        private record Deer(string Name, int Speed, int Move, int Rest)
        {
            public int Dist { get; set; } = 0;

            public bool Flying { get; set; } = true;

            public int NextState { get; set; } = Move;

            public int Points { get; set; }
        }

        private static void Step(Deer d)
        {
            if (d.Flying)
            {
                d.Dist += d.Speed;
            }

            --d.NextState;
            if (d.NextState == 0)
            {
                d.Flying = !d.Flying;
                if (d.Flying)
                {
                    d.NextState = d.Move;
                }
                else
                {
                    d.NextState = d.Rest;
                }
            }
        }

        private static int Simulate(Deer d, int seconds)
        {
            while (seconds > 0)
            {
                Step(d);
                --seconds;
            }

            return d.Dist;
        }

        public override string Solve_1()
        {
            int max = int.MinValue;
            Input.ForEach(line =>
            {
                string[] spl = line.Split(" ");
                Deer d = new(spl[0], int.Parse(spl[3]), int.Parse(spl[6]), int.Parse(spl[13]));
                max = Math.Max(max, Simulate(d, 2503));
            });

            return max.ToString();
        }

        public override string Solve_2()
        {
            List<Deer> deers = new();
            Input.ForEach(line =>
            {
                string[] spl = line.Split(" ");
                deers.Add(new(spl[0], int.Parse(spl[3]), int.Parse(spl[6]), int.Parse(spl[13])));
            });

            for (int seconds = 2503; seconds > 0; --seconds)
            {
                deers.ForEach(d => Step(d));
                int front = deers.Select(d => d.Dist).Max();
                deers.Where(d => d.Dist == front).ForEach(d => d.Points += 1);
            }

            return deers.Select(d => d.Points).Max().ToString();
        }
    }
}

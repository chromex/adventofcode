using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
{
    class Day_10 : BetterBaseDay
    {
        private record Bot(int id, int lowOut, int highOut)
        {
            public int v1 { get; set; } = -1;
            public bool TheOne { get; set; } = false;
        }

        private static Dictionary<int, Bot> GetBots(string[] lines)
        {
            Dictionary<int, Bot> bots = new();
            lines.Where(line => line.StartsWith("bot")).Split(' ').ForEach(line =>
            {
                int low = line[6].AsInt();
                int high = line[11].AsInt();
                if (line[5] == "output") low = (low * -1) - 1;
                if (line[10] == "output") high = (high * -1) - 1;

                bots[line[1].AsInt()] = new Bot(line[1].AsInt(), low, high);
            });
            return bots;
        }

        private static void Give(Dictionary<int, Bot> bots, Bot bot, int val)
        {
            if (bot.v1 < 0)
            {
                bot.v1 = val;
            }
            else 
            {
                int low = Math.Min(bot.v1, val);
                int high = Math.Max(bot.v1, val);

                bot.TheOne = (low == 17 && high == 61);

                if (bot.lowOut >= 0)
                    Give(bots, bots[bot.lowOut], low);
                else
                    bots[bot.lowOut] = new Bot(0, 0, 0) { v1 = low };

                if (bot.highOut >= 0)
                    Give(bots, bots[bot.highOut], high);
                else
                    bots[bot.highOut] = new Bot(0, 0, 0) { v1 = high };

                bot.v1 = -1;
            }
        }

        private Dictionary<int, Bot> _bots;

        public override string Solve_1()
        {
            _bots = GetBots(Input);

            Input.Where(line => line.StartsWith("value")).Split(' ').ForEach(line =>
            {
                Give(_bots, _bots[line[5].AsInt()], line[1].AsInt());
            });

            return _bots.Values.First(b => b.TheOne).id.ToString();
        }

        public override string Solve_2()
        {
            return (_bots[-1].v1 * _bots[-2].v1 * _bots[-3].v1).ToString();
        }
    }
}

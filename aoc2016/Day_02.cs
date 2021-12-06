using AoCUtil;
using System;

namespace aoc2016
{
    class Day_02 : BetterBaseDay
    {
        private static char Code(int x, int y)
        {
            switch ($"{x}{y}")
            {
                case "00": return '1';
                case "10": return '2';
                case "20": return '3';
                case "01": return '4';
                case "11": return '5';
                case "21": return '6';
                case "02": return '7';
                case "12": return '8';
                case "22": return '9';
            }

            return 'c';
        }

        public override string P1()
        {
            int x = 1, y = 1;

            string code = "";

            Input.ForEach(line =>
            {
                line.ForEach(inst =>
                {
                    switch (inst)
                    {
                        case 'U': y = Math.Max(y - 1, 0); break;
                        case 'D': y = Math.Min(y + 1, 2); break;
                        case 'L': x = Math.Max(x - 1, 0); break;
                        case 'R': x = Math.Min(x + 1, 2); break;
                    }
                });

                code += Code(x, y);
            });

            return code;
        }

        private static readonly string[] Map = {
            "00100",
            "02340",
            "56789",
            "0ABC0",
            "00D00"};

        private static bool Legal(int x, int y)
        {
            if (x < 0 || x > 4) return false;
            if (y < 0 || y > 4) return false;

            return Map[y][x] != '0';
        }

        public override string P2()
        {
            int x = 0, y = 2;
            string code = "";

            Input.ForEach(line =>
            {
                line.ForEach(inst =>
                {
                    int dx = 0, dy = 0;

                    switch (inst)
                    {
                        case 'U': dy = -1; break;
                        case 'D': dy = 1; break;
                        case 'L': dx = -1; break;
                        case 'R': dx = 1; break;
                    }

                    if (Legal(x + dx, y + dy))
                    {
                        x += dx;
                        y += dy;
                    }
                });

                code += Map[y][x];
            });

            return code;
        }
    }
}

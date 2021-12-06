using AoCUtil;
using System.Linq;

namespace aoc2021
{
    class Day_02 : BetterBaseDay
    {
        public override string P1()
        {
            Vec2 pos = new();

            Input.Split(' ').ForEach(i =>
            {
                switch (i[0])
                {
                    case "forward": pos.X += int.Parse(i[1]); break;
                    case "up": pos.Y -= int.Parse(i[1]); break;
                    case "down": pos.Y += int.Parse(i[1]); break;
                };
            });

            return (pos.X * pos.Y).ToString();
        }

        public override string P2()
        {
            Vec2 pos = new();
            int aim = 0;

            Input.Split(' ').ForEach(i =>
            {
                int arg = int.Parse(i[1]);
                switch (i[0])
                {
                    case "forward": 
                        pos.X += arg;
                        pos.Y += aim * arg; 
                        break;
                    case "up": aim -= arg; break;
                    case "down": aim += arg; break;
                };
            });

            return (pos.X * pos.Y).ToString();
        }
    }
}

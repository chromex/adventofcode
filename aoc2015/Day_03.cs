using AoCUtil;
using System.Collections.Generic;

namespace aoc2015
{
    class Day_03 : BetterBaseDay
    {
        public override string P1()
        {
            Vec2 pos = new();
            HashSet<string> map = new();

            map.Add(pos.ToString());

            Input[0].ForEach(c =>
            {
                Move(pos, c);
                map.Add(pos.ToString());
            });

            return map.Count.ToString();
        }

        private void Move(Vec2 pos, char c)
        {
            switch (c)
            {
                case '<': pos.X -= 1; break;
                case '>': pos.X += 1; break;
                case '^': pos.Y += 1; break;
                case 'v': pos.Y -= 1; break;
            }
        }

        public override string P2()
        {
            Vec2 p1 = new(), p2 = new();
            HashSet<string> map = new();

            map.Add(p1.ToString());
            bool flip = false;

            Input[0].ForEach(c =>
            {
                if (flip)
                {
                    Move(p1, c);
                    map.Add(p1.ToString());
                }
                else
                {
                    Move(p2, c);
                    map.Add(p2.ToString());
                }

                flip = !flip;
            });

            return map.Count.ToString();
        }
    }
}

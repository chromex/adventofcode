using AoCUtil;

namespace aoc2015
{
    class Day_25 : BetterBaseDay
    {
        private static void Move(Vec2 vec)
        {
            if (vec.Y > 1)
            {
                vec.X += 1;
                vec.Y -= 1;
            }
            else
            {
                vec.Y = vec.X + 1;
                vec.X = 1;
            }
        }

        private static ulong Search(Vec2 target)
        {
            Vec2 pos = new(1, 1);
            ulong val = 20151125;

            while (pos != target)
            {
                Move(pos);
                val = (val * 252533ul) % 33554393ul;
            }

            return val;
        }

        public override string P1()
        {
            return Search(new Vec2(3029, 2947)).ToString();
        }

        public override string P2()
        {
            return "free";
        }
    }
}

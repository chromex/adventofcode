using AoCUtil;
using System.Collections.Generic;

namespace aoc2016
{
    class Day_01 : BetterBaseDay
    {
        private string _second;
        private HashSet<string> visits = new();

        private void Move(ref Vec2 pos, ref Vec2 heading, int n)
        {
            while (n-- > 0)
            {
                if (!visits.Contains(pos.ToString()))
                {
                    visits.Add(pos.ToString());
                }
                else if (_second == null)
                {
                    _second = pos.Manhattan.ToString();
                }

                pos += heading;
            }
        }

        public override string Solve_1()
        {
            Vec2 pos = new();
            Vec2 heading = new(0, 1);

            Input[0].Split(" ").ForEach(inst =>
            {
                if (inst[0] == 'L')
                {
                    heading.RotateLeft90();
                }
                else
                {
                    heading.RotateRight90();
                }

                Move(ref pos, ref heading, int.Parse(inst.Substring(1)));
            });

            return pos.Manhattan.ToString();
        }

        public override string Solve_2()
        {
            return _second;
        }
    }
}

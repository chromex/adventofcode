using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_12 : BetterBaseDay
    {
        List<Inst> instructions = new List<Inst>();

        public Day_12()
        {
            instructions = Input.Select(str => new Inst() { Move = str[0].ToString(), Amount = int.Parse(str.Substring(1)) }).ToList();
        }

        public override string Solve_1()
        {
            Vec2 pos = new Vec2();
            Vec2 face = new Vec2(1, 0);

            foreach (Inst i in instructions)
            {
                switch (i.Move)
                {
                    case "N": pos.Y += i.Amount; break;
                    case "S": pos.Y -= i.Amount; break;
                    case "E": pos.X += i.Amount; break;
                    case "W": pos.X -= i.Amount; break;
                    case "L": Left(face, i.Amount / 90); break;
                    case "R": Right(face, i.Amount / 90); break;
                    case "F": pos = pos + face * i.Amount; break;
                }
            }

            return $"{pos.Manhattan}";
        }

        public override string Solve_2()
        {
            Vec2 pos = new Vec2();
            Vec2 waypoint = new Vec2(10, 1);

            foreach (Inst i in instructions)
            {
                switch (i.Move)
                {
                    case "N": waypoint.Y += i.Amount; break;
                    case "S": waypoint.Y -= i.Amount; break;
                    case "E": waypoint.X += i.Amount; break;
                    case "W": waypoint.X -= i.Amount; break;
                    case "L": Left(waypoint, i.Amount / 90); break;
                    case "R": Right(waypoint, i.Amount / 90); break;
                    case "F": pos += waypoint * i.Amount; break;
                }
            }

            return $"{pos.Manhattan}";
        }

        private void Left(Vec2 v, int n)
        {
            while (n-- > 0)
            {
                int t = v.X;
                v.X = -v.Y;
                v.Y = t;
            }
        }

        private void Right(Vec2 v, int n)
        {
            while (n-- > 0)
            {
                int t = v.X;
                v.X = v.Y;
                v.Y = -t;
            }
        }

        private class Inst
        {
            public string Move;
            public int Amount;
        }
    }
}

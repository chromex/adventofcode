using AoCUtil;
using System;
using System.Linq;
using System.Text;

namespace aoc2016
{
    class Day_08 : BetterBaseDay
    {
        private static Vec2 GetCoords(string str)
        {
            string[] spl = str.Substring(str.IndexOf("=") + 1).Split(" ").ToArray();
            return new Vec2(spl[0], spl[2]);
        }

        private static void Apply(Matrix<bool> screen, string command)
        {
            string[] spl = command.Split(" ");

            if (spl[0] == "rect")
            {
                int[] coords = spl[1].Split("x").Select(v => int.Parse(v)).ToArray();
                for (int x = 0; x < coords[0]; ++x)
                    for (int y = 0; y < coords[1]; ++y)
                        screen.Data[x, y] = true;
            }
            else if (spl[1] == "column")
            {
                Vec2 coord = GetCoords(command);
                bool[] col = screen.GetCol(coord.X);
                Util.RightShiftArray(col, coord.Y);
                screen.SetCol(coord.X, col);
            }
            else
            {
                Vec2 coord = GetCoords(command);
                bool[] row = screen.GetRow(coord.X);
                Util.RightShiftArray(row, coord.Y);
                screen.SetRow(coord.X, row);
            }
        }

        public override string Solve_1()
        {
            Matrix<bool> screen = new(50, 6);

            Input.ForEach(line => Apply(screen, line));

            StringBuilder sb = new();
            screen.Rows().ForEach(row =>
            {
                row.ForEach(c => sb.Append(c ? "#" : "."));
                sb.AppendLine();
            });
            Console.WriteLine(sb.ToString());

            return screen.Rows().Sum(row => row.Count(e => e)).ToString();
        }

        public override string Solve_2()
        {
            // To get this, break on the return of Solve_1 to see the code printed to console
            return "no";
        }
    }
}
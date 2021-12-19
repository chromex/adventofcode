using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2021
{
    class Day_13 : BetterBaseDay
    {
        private Matrix<int> Load()
        {
            List<Tuple<int, int>> coords = new();
            Input.Split(',').Where(s => s.Length > 1).ForEach(s => coords.Add(new(s[0].AsInt(), s[1].AsInt())));

            int width = coords.Select(t => t.Item1).Max();
            int height = coords.Select(t => t.Item2).Max();

            Matrix<int> map = new(width + 1, height + 1);

            coords.ForEach(t => map.Mark(t.Item1, t.Item2));

            return map;
        }

        private List<string> Folds() => Input.Where(s => s.Contains("fold")).Split(' ').Select(s => s.Last()).ToList();

        private Matrix<int> Fold(Matrix<int> map, string fold)
        {
            Matrix<int> result;

            string[] f = fold.Split('=');
            if (f[0] == "y")
            {
                result = new(map.Width, f[1].AsInt());
            }
            else
            {
                result = new(f[1].AsInt(), map.Height);
            }

            for (int x = 0; x < map.Width; ++x)
            {
                for (int y = 0; y < map.Height; ++y)
                {
                    if (map.IsMarked(x, y))
                    {
                        if (result.IsCoord(x, y))
                        {
                            result.Mark(x, y);
                        }
                        else
                        {
                            if (y > result.Height)
                                result.Mark(x, result.Height - (y - result.Height));
                            else
                                result.Mark(result.Width - (x - result.Width), y);
                        }
                    }
                }
            }

            return result;
        }

        private static void Print(Matrix<int> map)
        {
            StringBuilder sb = new();
            for (int y = 0; y < map.Height; ++y)
            {
                for (int x = 0; x < map.Width; ++x)
                {
                    sb.Append($"{(map.IsMarked(x, y) ? '#' : '.')}");
                }

                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        public override string P1()
        {
            Matrix<int> map = Load();

            map = Fold(map, Folds().First());

            return map.SumMarked().ToString();
        }

        public override string P2()
        {
            Matrix<int> map = Load();

            Folds().ForEach(f => map = Fold(map, f));

            Print(map);

            return map.SumMarked().ToString();
        }
    }
}

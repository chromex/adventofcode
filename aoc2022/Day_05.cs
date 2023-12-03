using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022
{
    class Day_05 : BetterBaseDay
    {
        public override string P1()
        {
            List<List<string>> columns = new();

            int nCol = (Input[0].Length + 1) / 4;
            for (int i = 0; i < nCol; ++i)
                columns.Add(new List<string>());

            int row = 0;
            for (; Input[row][1] != '1'; ++row)
            {
                string line = Input[row];
                
                for (int index = 0; index < line.Length; index += 4)
                {
                    if (line[index] == '[')
                    {
                        columns[index / 4].Insert(0, $"{line[index + 1]}");
                    }
                }
            }

            row += 2;
            for (; row < Input.Length; ++row)
            {
                Parser p = new(Input[row]);
                p.Burn(1);
                int count = p.GetNumber();
                p.Burn(1);
                int from = p.GetNumber() - 1;
                p.Burn(1);
                int to = p.GetNumber() - 1;

                while (count > 0)
                {
                    columns[to].Push(columns[from].Pop());
                    --count;
                }
            }

            return string.Join("", columns.Select(c => c.Last()));
        }

        public override string P2()
        {
            List<List<string>> columns = new();

            int nCol = (Input[0].Length + 1) / 4;
            for (int i = 0; i < nCol; ++i)
                columns.Add(new List<string>());

            int row = 0;
            for (; Input[row][1] != '1'; ++row)
            {
                string line = Input[row];

                for (int index = 0; index < line.Length; index += 4)
                {
                    if (line[index] == '[')
                    {
                        columns[index / 4].Insert(0, $"{line[index + 1]}");
                    }
                }
            }

            row += 2;
            for (; row < Input.Length; ++row)
            {
                Parser p = new(Input[row]);
                p.Burn(1);
                int count = p.GetNumber();
                p.Burn(1);
                int from = p.GetNumber() - 1;
                p.Burn(1);
                int to = p.GetNumber() - 1;

                columns[to].AddRange(columns[from].TakeLast(count));
                columns[from].RemoveRange(columns[from].Count - count, count);
            }

            return string.Join("", columns.Select(c => c.Last()));
        }
    }
}

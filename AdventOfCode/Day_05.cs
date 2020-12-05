using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private string[] lines;

        public Day_05()
        { lines = File.ReadAllLines(InputFilePath); }

        public override string Solve_1()
        {
            int highId = 0;
            foreach (string line in lines)
            {
                highId = Math.Max(highId, GetId(line));
            }

            return $"{highId}";
        }

        public override string Solve_2()
        {
            List<int> ids = lines.Select(line => GetId(line)).ToList();
            ids.Sort();
            for (int index = 1; index < ids.Count - 2; ++index)
            {
                if (ids[index] + 1 != ids[index + 1])
                {
                    return $"{ids[index] + 1}";
                }
            }

            return "err";
        }

        private int GetId(string pass)
        {
            int row = ConvertBinary(pass.Substring(0, 7), 'B');
            int col = ConvertBinary(pass.Substring(7), 'R');
            return row * 8 + col;
        }

        private int ConvertBinary(string number, char oneCh)
        {
            int res = 0;

            foreach (char ch in number)
            {
                res <<= 1;

                if (ch == oneCh)
                {
                    res += 1;
                }
            }

            return res;
        }
    }
}

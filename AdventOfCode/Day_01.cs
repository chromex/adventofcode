using AoCUtil;
using AoCHelper;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly List<int> _numbers;

        public Day_01()
        {
            _numbers = GetExpenseNumbers();
        }

        public override string Solve_1()
        {
            for (int index1 = 0; index1 < _numbers.Count - 2; ++index1)
            {
                for (int index2 = index1 + 1; index2 < _numbers.Count - 1; ++index2)
                {
                    if (_numbers[index1] + _numbers[index2] == 2020)
                    {
                        return $"{_numbers[index1] * _numbers[index2]}";
                    }
                }
            }

            return "err";
        }

        public override string Solve_2()
        {
            for (int index1 = 0; index1 < _numbers.Count - 3; ++index1)
            {
                for (int index2 = index1 + 1; index2 < _numbers.Count - 2; ++index2)
                {
                    for (int index3 = index2 + 1; index3 < _numbers.Count - 1; ++index3)
                    {
                        if (_numbers[index1] + _numbers[index2] + _numbers[index3] == 2020)
                        {
                            return $"{_numbers[index1] * _numbers[index2] * _numbers[index3]}";
                        }
                    }
                }
            }

            return "err";
        }

        private List<int> GetExpenseNumbers()
        {
            string[] lines = File.ReadAllLines(InputFilePath);
            List<int> numbers = new List<int>();
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    numbers.Add(int.Parse(line));
                }
            }

            numbers.Sort();

            return numbers;
        }
    }
}

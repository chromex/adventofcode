using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day_03 : BetterBaseDay
    {
        public override string P1()
        {
            int[] sums = new int[Input[0].Length];

            Input.ForEach(line =>
            {
                for (int i = 0; i < line.Length; ++i)
                {
                    if (line[i] == '1') ++sums[i];
                }
            });

            string res = string.Empty;
            string res2 = string.Empty;
            sums.ForEach(c =>
            {
                if (c > (Input.Length / 2))
                {
                    res += '1';
                    res2 += '0';
                }
                else
                {
                    res += '0';
                    res2 += '1';
                }
            });

            uint gamma = (uint)Util.ParseBinary(res);
            uint epsilon = (uint)Util.ParseBinary(res2);

            return (gamma * epsilon).ToString();
        }

        private static List<string> Filter(List<string> orig, char val, int index, Func<int, int, bool> pred)
        {
            if (orig.Count == 1) return orig;

            int sumOne = 0;

            orig.ForEach(line =>
            {
                if (line[index] == '1') ++sumOne;
            });

            int sumTwo = orig.Count - sumOne;

            char mostCommon = pred(sumOne, sumTwo) ? '1' : '0';
            if (sumOne == sumTwo)
            {
                mostCommon = val;
            }

            return orig.Where(line => line[index] == mostCommon).ToList();
        }

        public override string P2()
        {
            List<string> oxygenValues = Input.ToList();
            List<string> co2Values = Input.ToList();

            int index = 0;
            while (index < Input[0].Length)
            {
                oxygenValues = Filter(oxygenValues, '1', index, (sumOne, sumZero) => sumOne > sumZero);
                co2Values = Filter(co2Values, '0', index, (sumOne, sumZero) => sumOne < sumZero);

                ++index;
            }

            return (Util.ParseBinary(oxygenValues[0]) * Util.ParseBinary(co2Values[0])).ToString();
        }
    }
}

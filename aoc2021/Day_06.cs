using AoCUtil;

namespace aoc2021
{
    class Day_06 : BetterBaseDay
    {
        private string Run(int days)
        {
            ulong[] numAge = new ulong[9];

            Input[0].Split(',').ForEach(s => ++numAge[s.AsInt()]);

            for (int d = 0; d < days; ++d)
            {
                ulong nZero = numAge[0];
                Util.LeftShiftArray(numAge, 1);
                numAge[6] += nZero;
            }

            ulong sum = 0;
            numAge.ForEach(a => sum += a);
            return sum.ToString();
        }

        public override string P1()
        {
            return Run(80);
        }

        public override string P2()
        {
            return Run(256);
        }
    }
}

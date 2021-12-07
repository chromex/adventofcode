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
                numAge.RotateLeft();
                numAge[6] += numAge[8];
            }

            return numAge.Sum().ToString();
        }

        public override string P1() => Run(80);

        public override string P2() => Run(256);
    }
}

using AoCUtil;

namespace aoc2015
{
    class Day_20 : BetterBaseDay
    {
        private readonly static int Target = 34000000;

        public override string P1()
        {
            int[] houses = new int[1000000];

            for (int elf = 1; elf < houses.Length; ++elf)
            {
                for (int house = elf; house < houses.Length; house += elf)
                {
                    houses[house] += elf * 10;
                }
            }

            for (int house = 1; house < houses.Length; ++house)
            {
                if (houses[house] > Target)
                {
                    return house.ToString();
                }
            }

            return "no";
        }

        public override string P2()
        {
            int[] houses = new int[1000000];

            for (int elf = 1; elf < houses.Length; ++elf)
            {
                int visits = 0;
                for (int house = elf; house < houses.Length; house += elf)
                {
                    houses[house] += elf * 11;
                    if (++visits > 50)
                    {
                        break;
                    }
                }
            }

            for (int house = 1; house < houses.Length; ++house)
            {
                if (houses[house] > Target)
                {
                    return house.ToString();
                }
            }

            return "no";
        }
    }
}

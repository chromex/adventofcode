using AoCUtil;

namespace aoc2016
{
    class Day_06 : BetterBaseDay
    {
        public override string Solve_1()
        {
            FrequencyTable<char>[] table = new FrequencyTable<char>[Input[0].Length];
            for (int i = 0; i < table.Length; ++i)
                table[i] = new();

            Input.ForEach(line =>
            {
                for (int i = 0; i < table.Length; ++i)
                    table[i].Add(line[i]);
            });

            string code = "";
            for (int i = 0; i < table.Length; ++i)
                code += table[i].GetMostFrequent();

            return code;
        }

        public override string Solve_2()
        {
            FrequencyTable<char>[] table = new FrequencyTable<char>[Input[0].Length];
            for (int i = 0; i < table.Length; ++i)
                table[i] = new();

            Input.ForEach(line =>
            {
                for (int i = 0; i < table.Length; ++i)
                    table[i].Add(line[i]);
            });

            string code = "";
            for (int i = 0; i < table.Length; ++i)
                code += table[i].GetLeastFrequent();

            return code;
        }
    }
}

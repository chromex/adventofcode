using AoCUtil;

namespace aoc2022
{
    class Day_04 : BetterBaseDay
    {
        public override string P1()
        {
            int count = 0;

            Input.Split(',').ForEach(entry =>
            {
                var one = entry[0].Split('-');
                var two = entry[1].Split('-');

                IntRange or = new(one[0], one[1]);
                IntRange tr = new(two[0], two[1]);

                if (or.Contains(tr) || tr.Contains(or))
                    ++count;
            });

            return count.ToString();
        }

        public override string P2()
        {
            int count = 0;

            Input.Split(',').ForEach(entry =>
            {
                var one = entry[0].Split('-');
                var two = entry[1].Split('-');

                IntRange or = new(one[0], one[1]);
                IntRange tr = new(two[0], two[1]);

                if (or.Overlaps(tr)) 
                    ++count;
            });

            return count.ToString();
        }
    }
}

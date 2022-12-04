using AoCUtil;
using System.Linq;

namespace aoc2022
{
    class Day_03 : BetterBaseDay
    {
        public override string P1()
        {
            int score = 0;

            Input.ForEach(l =>
            {
                var left = l.Substring(0, l.Length / 2);
                var right = l.Substring(l.Length / 2);

                var shared = left.Intersect(right).First();
                if (char.IsUpper(shared))
                    score += (shared - 'A' + 27);
                else
                    score += (shared - 'a' + 1);
            });

            return score.ToString();
        }

        public override string P2()
        {
            int score = 0;

            for (int i = 0; i < Input.Length; i += 3)
            {
                var one = Input[i];
                var two = Input[i + 1];
                var three = Input[i + 2];

                var shared = one.Intersect(two).Intersect(three).First();
                if (char.IsUpper(shared))
                    score += (shared - 'A' + 27);
                else
                    score += (shared - 'a' + 1);
            }

            return score.ToString();
        }
    }
}

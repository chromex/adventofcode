using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022
{
    class Day_02 : BetterBaseDay
    {
        enum Move
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2
        }

        public override string P1()
        {
            ulong score = 0;

            Input.Split(' ').ForEach(s =>
            {
                Move opp = s[0] switch
                {
                    "A" => Move.Rock,
                    "B" => Move.Paper,
                    "C" => Move.Scissors
                };

                Move me = s[1] switch
                {
                    "X" => Move.Rock,
                    "Y" => Move.Paper,
                    "Z" => Move.Scissors
                };

                if (opp == me)
                {
                    // Tie
                    score += 3;
                }
                else if ((((ulong)me - 1) == (ulong)opp) || (me == Move.Rock && opp == Move.Scissors))
                {
                    // Win
                    score += 6;
                }

                score += (ulong)me + 1;
            });

            return score.ToString();
        }

        public override string P2()
        {
            ulong score = 0;

            Input.Split(' ').ForEach(s =>
            {
                Move opp = s[0] switch
                {
                    "A" => Move.Rock,
                    "B" => Move.Paper,
                    "C" => Move.Scissors
                };

                Move me = Move.Paper;
                switch (s[1])
                {
                    case "X": // Lose
                        me = (Move)(((int)opp - 1) % 3);
                        if ((int)me == -1) me = Move.Scissors;
                        break;

                    case "Y": // Tie
                        me = opp;
                        break;

                    case "Z": // Win
                        me = (Move)(((int)opp + 1) % 3);
                        break;
                }

                if (opp == me)
                {
                    // Tie
                    score += 3;
                }
                else if ((((ulong)me - 1) == (ulong)opp) || (me == Move.Rock && opp == Move.Scissors))
                {
                    // Win
                    score += 6;
                }

                score += (ulong)me + 1;
            });

            return score.ToString();
        }
    }
}

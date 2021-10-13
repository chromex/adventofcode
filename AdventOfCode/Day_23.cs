using AoCUtil;
using System;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day_23 : BetterBaseDay
    {
        // 45798623
        public override string Solve_1()
        {
            RingList<int> cups = new RingList<int>(Input[0].Select(ch => ch - '0'));
            PlayGame2(cups, 100,false);
            return string.Join("", Enumerable.Range(cups.Raw.IndexOf(1) + 1, cups.Count - 1).Select(idx => cups[idx].ToString()));
        }

        private void PlayGame(RingList<int> cups, int roundCount)
        {
            int maxVal = cups.Count;
            for (int round = 0; round < roundCount; ++round)
            {
                if (round % 10000 == 0)
                    Console.WriteLine($"{round}");

                int[] pocket = Enumerable.Range(1, 3).Select(_ => cups.RemoveAt(1)).ToArray();

                int target = cups[0] - 1;
                while (pocket.Contains(target) || target < 1)
                {
                    --target;
                    if (target < 1)
                        target = maxVal;
                }

                for (int sidx = cups.Raw.Count - 1; sidx >= 0; --sidx)
                {
                    if (cups.Raw[sidx] == target)
                    {
                        cups.Raw.InsertRange(sidx + 1, pocket);
                        break;
                    }
                }

                cups.RotateLeft();
            }
        }

        private void PlayGame2(RingList<int> cups, int roundCount, bool print)
        {
            int[] reverseLookup = new int[cups.Count + 1];
            for (int idx = 0; idx < cups.Count; ++idx)
            {
                reverseLookup[cups[idx]] = idx;
            }

            int current = cups[0];
            int maxVal = cups.Count;
            for (int round = 0; round < roundCount; ++round)
            {
                if (print)
                {
                    Console.Write($"{round,5}: ");

                    int oneIndex = cups.Raw.IndexOf(1);
                    Console.Write($"1 -> {cups[oneIndex + 1],2} -> {cups[oneIndex + 2],2}: ");

                    StringBuilder sb = new StringBuilder();
                    foreach (int val in cups.Raw)
                    {
                        if (val == 1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        if (cups[0] == val)
                        {
                            sb.AppendFormat($"({val,2}) ");
                            Console.Write($"({val,2}) ");
                        }
                        else
                        {
                            sb.AppendFormat($" {val,2}  ");
                            Console.Write($" {val,2}  ");
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                }

                int offset = 0;
                int[] pocket = Enumerable.Range(1, 3).Select(_ =>
                {
                    if (reverseLookup[current] + 1 >= cups.Count)
                        ++offset;
                    return cups.RemoveAt(reverseLookup[current] - offset + 1);
                }).ToArray();

                int target = current - 1;
                while (pocket.Contains(target) || target < 1)
                {
                    --target;
                    if (target < 1)
                        target = maxVal;
                }

                // Get target index
                int offset2 = 0;
                foreach (int val in pocket)
                    if (reverseLookup[val] < reverseLookup[target])
                        ++offset2;

                //Debug.Assert(offset == offset2);

                cups.Raw.InsertRange(reverseLookup[target] - offset2 + 1, pocket);

                int begin, end;
                if (reverseLookup[target] > reverseLookup[current])
                {
                    begin = reverseLookup[current] + 1;
                    end = reverseLookup[target];
                }
                else
                {
                    begin = reverseLookup[target] + 1;
                    end = reverseLookup[current] + 3;
                }

                if (cups.Count - reverseLookup[current] <= 3)
                {
                    begin = 0; 
                    end = cups.Count - 1;
                }

                for (; begin <= end; ++begin)
                {
                    reverseLookup[cups[begin]] = begin;
                }

                current = cups[reverseLookup[current] + 1];
            }
        }

        public override string Solve_2()
        {
            RingList<int> cups;
            {
                int[] vals = new int[30];
                for (int idx = 0; idx < vals.Count(); ++idx)
                    vals[idx] = idx + 1;
                int[] bottom = Input[0].Select(ch => ch - '0').ToArray();
                for (int idx = 0; idx < bottom.Count(); ++idx)
                {
                    vals[idx] = bottom[idx];
                }
                cups = new RingList<int>(vals);
            }

            PlayGame2(cups, 1000, false);

            int oneIndex = cups.Raw.IndexOf(1);
            long nextCup = cups[oneIndex + 1];
            long nextCup2 = cups[oneIndex + 2];

            return (nextCup * nextCup2).ToString();
        }
    }
}

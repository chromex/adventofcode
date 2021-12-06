using AoCUtil;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2016
{
    class Day_07 : BetterBaseDay
    {
        private static void GetSections(string ip, List<string> sequences, List<string> hypernetSequences)
        {
            StringBuilder sb = new();

            for (int i = 0; i < ip.Length; ++i)
            {
                if (ip[i] == '[')
                {
                    if (sb.Length > 0)
                    {
                        sequences.Add(sb.ToString());
                        sb.Clear();
                    }

                    int end = ip.IndexOf(']', i);
                    hypernetSequences.Add(ip.Substring(i + 1, end - i - 1));
                    i = end;
                }
                else
                {
                    sb.Append(ip[i]);
                }
            }

            if (sb.Length > 0)
                sequences.Add(sb.ToString());
        }

        private static bool CheckABBA(string sequence)
        {
            for (int i = 0; i < sequence.Length - 3; ++i)
                if (sequence[i] != sequence[i+1] &&  sequence[i] == sequence[i + 3] && sequence[i + 1] == sequence[i + 2])
                    return true;

            return false;
        }

        private static bool IsTLS(string ip)
        {
            List<string> sequences = new(), hypernetSequences = new();
            GetSections(ip, sequences, hypernetSequences);

            if (hypernetSequences.Any(seq => CheckABBA(seq)))
                return false;

            return sequences.Any(seq => CheckABBA(seq));
        }

        private static List<string> GetBABs(List<string> sequences)
        {
            List<string> result = new();

            sequences.ForEach(seq =>
            {
                for (int i = 0; i < seq.Length - 2; ++i)
                    if (seq[i] != seq[i + 1] && seq[i] == seq[i + 2])
                        result.Add($"{seq[i + 1]}{seq[i]}{seq[i + 1]}");
            });

            return result;
        }

        private static bool IsSSL(string ip)
        {
            List<string> sequences = new(), hypernetSequences = new();
            GetSections(ip, sequences, hypernetSequences);

            List<string> babs = GetBABs(sequences);
            return hypernetSequences.Any(hyper => babs.Any(bab => hyper.Contains(bab)));
        }

        public override string P1()
        {
            return Input.Count(line => IsTLS(line)).ToString();
        }

        public override string P2()
        {
            return Input.Count(line => IsSSL(line)).ToString();
        }
    }
}

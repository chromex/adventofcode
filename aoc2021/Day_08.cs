using AoCUtil;
using System.Linq;

namespace aoc2021
{
    class Day_08 : BetterBaseDay
    {
        public override string P1()
        {
            int sum = 0;

            Input.ForEach(line =>
            {
                string[] parts = line.Split('|');
                parts[1].Split(' ').ForEach(entry =>
                {
                    if (entry.Length == 2 || entry.Length == 3 || entry.Length == 4 || entry.Length == 7)
                        ++sum;
                });
            });

            return sum.ToString();
        }

        private static string Sub(string left, string right)
        {
            string ret = string.Empty;

            foreach (char c in left)
            {
                if (!right.Contains(c))
                {
                    ret += c;
                }
            }

            return ret;
        }

        private static bool Contains(string code, string vals)
        {
            foreach (char c in vals)
            {
                if (!code.Contains(c))
                    return false;
            }

            return true;
        }

        private static bool Compare(string left, string right)
        {
            return (left.Length == right.Length) && Contains(left, right);
        }

        private static int Id(string[] entries, string val)
        {
            for (int i = 0; i < entries.Length; ++i)
            {
                if (Compare(entries[i], val))
                    return i;
            }

            return -1;
        }

        private static int Process(string line)
        {
            string[] entries = new string[10];

            string[] values = line.Split(' ');
            foreach (string val in values)
            {
                switch (val.Length)
                {
                    case 2: entries[1] = val; break;
                    case 4: entries[4] = val; break;
                    case 3: entries[7] = val; break;
                    case 7: entries[8] = val; break;
                }
            }

            // a: 7 - 1
            string a = Sub(entries[7], entries[1]);
            // eg: 8 - a - 4
            string eg = Sub(Sub(entries[8], a), entries[4]);
            // bd: 4 - 1
            string bd = Sub(entries[4], entries[1]);

            foreach (string val in values)
            {
                switch (val.Length)
                {
                    case 5: // 2, 3, 5
                        // 2: contains eg
                        if (Contains(val, eg))
                        {
                            entries[2] = val;
                        }
                        // 3: only one with 1 in it
                        else if (Contains(val, entries[1]))
                        {
                            entries[3] = val;
                        }
                        // 5: contains bd
                        else
                        {
                            entries[5] = val;
                        }

                        break;

                    case 6: // 0, 6, 9
                        // 9: contains 4 && 7
                        if (Contains(val, entries[4]) && Contains(val, entries[7]))
                        {
                            entries[9] = val;
                        }
                        // 6: does not contain 7
                        else if (!Contains(val, entries[7]))
                        {
                            entries[6] = val;
                        }
                        // 0: 7 not 4
                        else
                        {
                            entries[0] = val;
                        }

                        break;
                }
            }

            string res = $"{Id(entries, values[11])}{Id(entries, values[12])}{Id(entries, values[13])}{Id(entries, values[14])}";

            return res.AsInt();
        }

        public override string P2()
        {
            return Input.Select(line => Process(line)).Sum().ToString();
        }
    }
}

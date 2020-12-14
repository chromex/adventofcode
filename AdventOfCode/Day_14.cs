using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_14 : BetterBaseDay
    {
        public Day_14()
        { }

        public override string Solve_1()
        {
            ulong zeroMask = ulong.MaxValue;
            ulong oneMask = 0;
            Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

            foreach (string line in Input)
            {
                Parser p = new Parser(line);
                string i = p.GetIdent();
                if (i == "mask")
                {
                    p.Burn(1);

                    zeroMask = ulong.MaxValue;
                    oneMask = 0;

                    foreach (char c in p.GetIdent())
                    {
                        zeroMask = (zeroMask << 1) + 1;
                        oneMask = oneMask << 1;

                        if (c == '0')
                        {
                            zeroMask -= 1;
                        }
                        else if (c == '1')
                        {
                            oneMask += 1;
                        }
                    }

                    continue;
                }

                p.Burn(1);
                ulong addr = (ulong)p.GetNumber();
                p.Burn(2);
                ulong val = (ulong)p.GetNumber();

                memory[addr] = (val | oneMask) & zeroMask;
            }

            ulong sum = 0;
            foreach (ulong val in memory.Values)
                sum += val;
            return sum.ToString();
        }

        public override string Solve_2()
        {
            string mask = string.Empty;
            Dictionary<string, long> memory = new Dictionary<string, long>();

            foreach (string line in Input)
            {
                Parser p = new Parser(line);

                if (p.GetIdent() == "mask")
                {
                    p.Burn();
                    mask = new string(p.GetIdent().Reverse().ToArray());
                    continue;
                }

                List<string> addresses = new List<string>();
                int index = p.GetNumber();
                p.Burn(2);

                addresses.Add("");
                foreach (char c in mask)
                {
                    if (c == '0')
                    {
                        addresses = AddChar(addresses, (index & 1).ToString());
                    }
                    else if (c == '1')
                    {
                        addresses = AddChar(addresses, "1");
                    }
                    else
                    {
                        List<string> left = AddChar(addresses, "0");
                        List<string> right = AddChar(addresses, "1");
                        addresses = left;
                        addresses.AddRange(right);
                    }

                    index >>= 1;
                }

                foreach (string addr in addresses)
                {
                    memory[addr] = p.GetNumber();
                }
            }

            return memory.Values.Sum().ToString();
        }

        private List<string> AddChar(List<string> list, string c)
        {
            return list.Select(addr => c + addr).ToList();
        }
    }
}

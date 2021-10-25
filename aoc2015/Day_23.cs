using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_23 : BetterBaseDay
    {
        private record State { public uint A { get; set; } public uint B { get; set; } }

        private static int ParseInt(string s)
        {
            int val = int.Parse(s.Substring(1));
            if (s[0] == '-')
                val *= -1;
            return val;
        }

        private void Sim(State s)
        {
            List<string[]> instructions = Input.Select(line => line.Split(" ")).ToList();

            int pc = 0;

            while (pc < instructions.Count)
            {
                string[] inst = instructions[pc];

                switch (inst[0])
                {
                    case "hlf":
                        s.A = s.A / 2;
                        ++pc;
                        break;

                    case "tpl":
                        s.A = s.A * 3;
                        ++pc;
                        break;

                    case "inc":
                        if (inst[1] == "a")
                            ++s.A;
                        else
                            ++s.B;
                        ++pc;
                        break;

                    case "jmp":
                        pc += ParseInt(inst[1]);
                        break;

                    case "jie":
                        if (s.A % 2 == 0)
                            pc += ParseInt(inst[2]);
                        else
                            ++pc;
                        break;

                    case "jio":
                        if (s.A == 1)
                            pc += ParseInt(inst[2]);
                        else
                            ++pc;
                        break;
                }
            }
        }

        public override string Solve_1()
        {
            State s = new();
            Sim(s);

            return s.B.ToString();
        }

        public override string Solve_2()
        {
            State s = new() { A = 1 };
            Sim(s);

            return s.B.ToString();
        }
    }
}

using AoCUtil;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_08 : BetterBaseDay
    {
        private List<Inst> instructions;

        public Day_08()
        {
            instructions = Input.Select(line => ParseEntry(line)).ToList();
        }

        private Inst ParseEntry(string line)
        {
            string[] split = line.Split(" ");

            Inst ret = new Inst();

            ret.inst = split[0];
            ret.arg = int.Parse(split[1]);

            return ret;
        }

        private int acc = 0;
        private int pos = 0;

        public override string Solve_1()
        {
            RunUntilDouble();
            return $"{acc}";
        }

        private bool RunUntilDouble()
        {
            acc = 0;
            pos = 0;
            foreach (Inst inst in instructions)
                inst.executed = false;

            while (true)
            {
                Inst current = instructions[pos];

                if (current.executed)
                {
                    return false;
                }

                switch (current.inst)
                {
                    case "nop":
                        ++pos;
                        break;

                    case "acc":
                        acc += current.arg;
                        ++pos;
                        break;

                    case "jmp":
                        pos += current.arg;
                        break;
                }

                current.executed = true;

                if (pos >= instructions.Count)
                {
                    return true;
                }
            }
        }

        public override string Solve_2()
        {
            for (int index = 0; index < instructions.Count; ++index)
            {
                if (instructions[index].inst == "nop")
                {
                    instructions[index].inst = "jmp";
                    if (RunUntilDouble())
                    {
                        return $"{acc}";
                    }
                    else
                    {
                        instructions[index].inst = "nop";
                    }
                }
                else if (instructions[index].inst == "jmp")
                {
                    instructions[index].inst = "nop";
                    if (RunUntilDouble())
                    {
                        return $"{acc}";
                    }
                    else
                    {
                        instructions[index].inst = "jmp";
                    }
                }
            }

            return "err";
        }

        class Inst
        {
            public string inst;
            public int arg;
            public bool executed;
        }
    }
}

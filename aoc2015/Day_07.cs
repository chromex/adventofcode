using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_07 : BetterBaseDay
    {
        private Dictionary<string, ushort> settledWires = new();

        private static ushort Mask(int val)
        {
            return (ushort)(val & 0xFFFF);
        }

        private bool TryEval(string str, out ushort val)
        {
            if (ushort.TryParse(str, out val))
            {
                return true;
            }
            else if (settledWires.ContainsKey(str))
            {
                val = settledWires[str];
                return true;
            }

            return false;
        }

        private bool TryApply(string[] line)
        {
            ushort val = 0;

            switch (line.Length)
            {
                case 3:
                    if (TryEval(line[0], out val))
                    {
                        settledWires[line[2]] = val;
                        return true;
                    }

                    break;

                case 4:
                    if (settledWires.ContainsKey(line[1]))
                    {
                        settledWires[line[3]] = Mask(~settledWires[line[1]]);
                        return true;
                    }

                    break;

                case 5:
                    if (!line[1].Contains("SHIFT") && TryEval(line[0], out val) && settledWires.ContainsKey(line[2]))
                    {
                        switch (line[1])
                        {
                            case "AND":
                                settledWires[line[4]] = Mask(val & settledWires[line[2]]);
                                return true;

                            case "OR":
                                settledWires[line[4]] = Mask(val | settledWires[line[2]]);
                                return true;
                        }
                    }
                    else if (settledWires.ContainsKey(line[0]))
                    {
                        switch (line[1])
                        {
                            case "LSHIFT":
                                settledWires[line[4]] = Mask(settledWires[line[0]] << int.Parse(line[2]));
                                return true;

                            case "RSHIFT":
                                settledWires[line[4]] = Mask(settledWires[line[0]] >> int.Parse(line[2]));
                                return true;
                        }
                    }

                    break;
            }

            return false;
        }

        public override string Solve_1()
        {
            List<string[]> lines = Input.Select(line => line.Split(" ")).ToList();

            while (lines.Count > 0)
            {
                for (int index = 0; index < lines.Count; )
                {
                    if (TryApply(lines[index]))
                    {
                        lines.RemoveAt(index);
                    }
                    else
                    {
                        ++index;
                    }
                }
            }

            return settledWires["a"].ToString();
        }

        public override string Solve_2()
        {
            // Updated the input source change line 335 to go from "1674 -> b" to "46065 -> b"

            return "no";
        }
    }
}

using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_02 : BetterBaseDay
    {
        private readonly List<PEntry> passwords;

        public Day_02()
        {
            passwords = Input.Select(line => ParsePEntry(line)).ToList();
        }

        public override string Solve_1()
        {
            return $"{passwords.Where(entry => Valid(entry)).Count()}";
        }

        public override string Solve_2()
        {
            return $"{passwords.Where(entry => Valid2(entry)).Count()}";
        }

        private bool Valid(PEntry entry)
        {
            int num = entry.Password.Count((c) => c == entry.Character);

            return num >= entry.LowRange && num <= entry.HighRange;
        }

        private bool Valid2(PEntry entry)
        {
            bool p1 = CharInPos(entry.Character, entry.LowRange - 1, entry.Password);
            bool p2 = CharInPos(entry.Character, entry.HighRange - 1, entry.Password);

            return p1 ^ p2;
        }

        private bool CharInPos(char c, int pos, string line)
        {
            if (pos >= 0 && pos < line.Length)
            {
                return line[pos] == c;
            }

            return false;
        }

        private PEntry ParsePEntry(string line)
        {
            Parser p = new Parser(line);
            PEntry entry = new PEntry();

            entry.LowRange = p.GetNumber();
            p.Burn();
            entry.HighRange = p.GetNumber();
            entry.Character = p.GetIdent()[0];
            p.Burn();
            entry.Password = p.GetIdent();

            return entry;
        }

        private class PEntry
        {
            public int LowRange;
            public int HighRange;
            public char Character;
            public string Password;
        }
    }
}

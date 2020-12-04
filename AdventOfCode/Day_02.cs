using AoCHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly List<PEntry> passwords;

        public Day_02()
        {
            passwords = GetPasswordEntries();
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
            int num = entry.Password.Where((c) => c == entry.Character).Count();

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

        private List<PEntry> GetPasswordEntries()
        {
            string[] lines = File.ReadAllLines(InputFilePath);
            List<PEntry> numbers = new List<PEntry>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                numbers.Add(ParsePEntry(line));
            }

            return numbers;
        }

        private PEntry ParsePEntry(string line)
        {
            string[] splits = line.Split(' ');
            string[] rangeSplit = splits[0].Split('-');

            int rangeStart = int.Parse(rangeSplit[0]);
            int rangeEnd = int.Parse(rangeSplit[1]);
            char c = splits[1][0];
            string password = splits[2];

            return new PEntry() { LowRange = rangeStart, HighRange = rangeEnd, Character = c, Password = password };
        }

        private class PEntry
        {
            public int LowRange { get; set; }

            public int HighRange { get; set; }

            public char Character { get; set; }

            public string Password { get; set; }

            public override string ToString()
            {
                return $"{LowRange} to {HighRange} of {Character} in {Password}";
            }
        }
    }
}

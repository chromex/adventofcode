using AoCUtil;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_04 : BetterBaseDay
    {
        private List<Passport> passports = new List<Passport>();

        public Day_04()
        {
            passports = Util.ParseRecords<Passport>(Input, ":");
        }

        public override string Solve_1()
        {
            return $"{passports.Count(passport => passport.IsValid())}";
        }

        public override string Solve_2()
        {
            return $"{passports.Count(passport => passport.IsValid2())}";
        }

        private class Passport
        {
            public string byr;
            public string iyr;
            public string eyr;
            public string hgt;
            public string hcl;
            public string ecl;
            public string pid;
            public string cid;

            string[] validEcl = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            public bool IsValid()
            {
                return byr != null && iyr != null && eyr != null && hgt != null && hcl != null && ecl != null && pid != null;
            }

            public bool IsValid2()
            {
                if (!Util.StrInRange(byr, 1920, 2002)) return false;
                if (!Util.StrInRange(iyr, 2010, 2020)) return false;
                if (!Util.StrInRange(eyr, 2020, 2030)) return false;

                if (hgt != null)
                {
                    if (Regex.IsMatch(hgt, @"^\d*cm$"))
                    {
                        if (!Util.StrInRange(hgt.Substring(0, hgt.Length - 2), 150, 193))
                            return false;
                    }
                    else if (Regex.IsMatch(hgt, @"^\d*in$"))
                    {
                        if (!Util.StrInRange(hgt.Substring(0, hgt.Length - 2), 59, 76))
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                if (hcl == null || !Regex.IsMatch(hcl, @"^#[0-9a-fA-F]{6}$"))
                    return false;

                if (!validEcl.Contains(ecl))
                    return false;

                if (pid == null || !Regex.IsMatch(pid, @"^\d{9}$"))
                    return false;

                return true;
            }
        }
    }
}

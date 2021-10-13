using AoCUtil;
using System.Security.Cryptography;
using System.Text;

namespace aoc2015
{
    class Day_04 : BetterBaseDay
    {
        private readonly string PRIVATE = "yzbqklnj";

        public override string Solve_1()
        {
            for (int i = 0; i < int.MaxValue; ++i)
            {
                if (CreateMD5($"{PRIVATE}{i}", "00000"))
                {
                    return i.ToString();
                }
            }

            return "lol";
        }

        public override string Solve_2()
        {
            for (int i = 0; i < int.MaxValue; ++i)
            {
                if (CreateMD5($"{PRIVATE}{i}", "000000"))
                {
                    return i.ToString();
                }
            }

            return "lol";
        }

        private static bool CreateMD5(string input, string check)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return $"{hashBytes[0]:X2}{hashBytes[1]:X2}{hashBytes[2]:X2}".StartsWith(check);
            }
        }
    }
}

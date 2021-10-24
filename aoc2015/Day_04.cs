using AoCUtil;
using System.Security.Cryptography;
using System.Text;

namespace aoc2015
{
    class Day_04 : BetterBaseDay
    {
        private static readonly MD5 _md5 = MD5.Create();

        public override string Solve_1()
        {
            for (int i = 0; i < int.MaxValue; ++i)
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes($"yzbqklnj{i}");
                byte[] hashBytes = _md5.ComputeHash(inputBytes);

                if (hashBytes[0] == 0 && hashBytes[1] == 0 && (hashBytes[2] & 0xF0) == 0)
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
                byte[] inputBytes = Encoding.ASCII.GetBytes($"yzbqklnj{i}");
                byte[] hashBytes = _md5.ComputeHash(inputBytes);

                if (hashBytes[0] == 0 && hashBytes[1] == 0 && hashBytes[2] == 0)
                {
                    return i.ToString();
                }
            }

            return "lol";
        }
    }
}

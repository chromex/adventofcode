using AoCUtil;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace aoc2016
{
    class Day_05 : BetterBaseDay
    {
        private static readonly MD5 _md5 = MD5.Create();

        public override string Solve_1()
        {
            string code = "";

            for (int i = 0; i < int.MaxValue && code.Length < 8; ++i)
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes($"ffykfhsq{i}");
                byte[] hashBytes = _md5.ComputeHash(inputBytes);

                if (hashBytes[0] == 0 && hashBytes[1] == 0 && (hashBytes[2] & 0xF0) == 0)
                {
                    code += $"{(hashBytes[2] & 0x0F):X}";
                }
            }

            return code;
        }

        public override string Solve_2()
        {
            char[] code = { '-', '-', '-', '-', '-', '-', '-', '-' };

            for (int i = 0; code.Contains('-'); ++i)
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes($"ffykfhsq{i}");
                byte[] hashBytes = _md5.ComputeHash(inputBytes);

                if (hashBytes[0] == 0 && hashBytes[1] == 0 && (hashBytes[2] & 0xF0) == 0)
                {
                    int index = (hashBytes[2] & 0x0F);
                    if (index < 8 && code[index] == '-')
                    {
                        code[index] = ($"{(hashBytes[3] & 0xF0):X}")[0];
                    }
                }
            }

            return new string(code);
        }
    }
}

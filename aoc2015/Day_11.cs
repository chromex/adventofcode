using AoCUtil;

namespace aoc2015
{
    class Day_11 : BetterBaseDay
    {
        private static bool HasDuplicates(char[] pass)
        {
            for (int i = 0; i < pass.Length - 3; ++i)
            {
                char c0 = pass[i];
                char c1 = pass[i + 1];

                if (c0 == c1)
                {
                    for (int j = i + 2; j < pass.Length - 1; ++j)
                    {
                        char c2 = pass[j];
                        char c3 = pass[j + 1];

                        if (c2 == c3)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsValid(char[] pass)
        {
            for (int i = 0; i < pass.Length - 3; ++i)
            {
                char c0 = pass[i];
                char c1 = pass[i+1];
                char c2 = pass[i+2];

                if ((c0 + 1 == c1) && (c1 + 1 == c2))
                {
                    return HasDuplicates(pass);
                }
            }

            return false;
        }

        private static bool Bump(char[] pass, int index)
        {
            char c = pass[index];
            if (c == 'z')
            {
                pass[index] = 'a';
                return true;
            }

            c = (char)(c + 1);
            if (c == 'i' || c == 'o' || c == 'l')
            {
                c = (char)(c + 1);
            }

            pass[index] = c;
            return false;
        }

        private static void Increment(char[] pass)
        {
            int index = pass.Length - 1;

            while (index >= 0 && Bump(pass, index))
            {
                --index;
            }
        }

        public override string Solve_1()
        {
            char[] pass = "vzbxkghb".ToCharArray();

            do
            {
                Increment(pass);
            } while (!IsValid(pass));

            return string.Join("", pass);
        }

        public override string Solve_2()
        {
            char[] pass = "vzbxxyzz".ToCharArray();

            do
            {
                Increment(pass);
            } while (!IsValid(pass));

            return string.Join("", pass);
        }
    }
}

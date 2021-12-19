using AoCUtil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace aoc2021
{
    class Day_16 : BetterBaseDay
    {
        class Stream
        {
            public string Data;
            public int Index;

            public bool IsDone => Index >= (Data.Length - 7);

            public BigInteger Read(int len) => Util.ParseBinary(ReadString(len));

            public string ReadString(int len)
            {
                string ret = Data.Substring(Index, len);
                Index += len;
                return ret;
            }
        }

        private static Stream Process(string hex)
        {
            StringBuilder sb = new();

            hex.Select(c => int.Parse($"{c}", NumberStyles.HexNumber)).ForEach(v =>
            {
                sb.Append(Convert.ToString(v + 16, 2).Substring(1));
            });

            return new Stream() { Data = sb.ToString() };
        }

        private BigInteger versionSum;

        private BigInteger Parse(Stream s)
        {
            BigInteger ver = s.Read(3);
            BigInteger type = s.Read(3);

            versionSum += ver;

            if (type == 4)
            {
                StringBuilder sb = new();
                bool consuming;
                do
                {
                    consuming = s.Read(1) == 1;
                    sb.Append(s.ReadString(4));
                } while (consuming);

                return Util.ParseBinary(sb.ToString());
            }
            else
            {
                List<BigInteger> subs = new();
                BigInteger lenType = s.Read(1);

                if (lenType == 0)
                {
                    BigInteger totalLength = s.Read(15);
                    int finish = s.Index + (int)totalLength;

                    while (s.Index < finish)
                    {
                        subs.Add(Parse(s));
                    }
                }
                else
                {
                    BigInteger totalPackets = s.Read(11);

                    for (BigInteger c = 0; c < totalPackets; ++c)
                    {
                        subs.Add(Parse(s));
                    }
                }

                BigInteger ret = 0;

                switch ((int)type)
                {
                    case 0: 
                        subs.ForEach(v => ret += v);
                        break;
                    case 1:
                        ret = 1;
                        subs.ForEach(v => ret *= v);
                        break;
                    case 2: ret = subs.Min(); break;
                    case 3: ret = subs.Max(); break;
                    case 5: ret = subs[0] > subs[1] ? 1 : 0; break;
                    case 6: ret = subs[0] < subs[1] ? 1 : 0; break;
                    case 7: ret = subs[0] == subs[1] ? 1 : 0; break;
                }

                return ret;
            }
        }

        public override string P1()
        {
            Parse(Process(Input[0]));

            return versionSum.ToString();
        }

        public override string P2()
        {
            return $"{Parse(Process(Input[0]))}";
        }
    }
}

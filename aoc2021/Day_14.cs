using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2021
{
    class Day_14 : BetterBaseDay
    {
        private Dictionary<string, string> Load()
        {
            Dictionary<string, string> pairs = new();

            Input.Where(s => s.Contains("->")).Split(' ').ForEach(s => pairs[s[0]] = s[2]);

            return pairs;
        }
        private char[] Load2()
        {
            char[] subs = new char[10000];

            Input.Where(s => s.Contains("->")).Split(' ').ForEach(s =>
            {
                string pair = s[0];
                subs[(pair[0] * 100 + pair[1])] = s[2][0];
            });

            return subs;
        }

        private static string Sub(string template, Dictionary<string, string> subs)
        {
            StringBuilder sb = new();

            for (int i = 0; i < template.Length - 1; ++i)
            {
                string p = template.Substring(i, 2);

                sb.Append(template[i]);

                if (subs.ContainsKey(p))
                {
                    sb.Append(subs[p]);
                }
            }

            sb.Append(template.Last());

            return sb.ToString();
        }

        public override string P1()
        {
            string template = Input[0];
            Dictionary<string, string> subs = Load();

            for (int i = 0; i < 10; ++i)
            {
                template = Sub(template, subs);
            }

            var groups = template.GroupBy(c => c);
            int max = groups.Max(g => g.Count());
            int min = groups.Min(g => g.Count());

            return (max - min).ToString();
        }

        private static void Visit(int depth, char p0, char p1, char[] subs, long[] counts, int max)
        {
            int key = p0 * 100 + p1;

            if (depth < max && subs[key] != 0)
            {
                Visit(depth + 1, p0, subs[key], subs, counts, max);
                Visit(depth + 1, subs[key], p1, subs, counts, max);
            }
            else
            {
                ++counts[p0];
            }
        }

        private static void Visit2(int depth, char p0, char p1, char[] subs, long[] counts, int max, Dictionary<string, long[]> countsCollection)
        {
            int key = p0 * 100 + p1;

            if (depth < max && subs[key] != 0)
            {
                if (depth < (max / 2))
                {
                    Visit2(depth + 1, p0, subs[key], subs, counts, max, countsCollection);
                    Visit2(depth + 1, subs[key], p1, subs, counts, max, countsCollection);
                }
                else
                {
                    long[] cpy = countsCollection[$"{p0}{p1}"];
                    for (int i = 0; i < counts.Length; ++i)
                    {
                        counts[i] += cpy[i];
                    }
                }
            }
            else
            {
                ++counts[p0];
            }
        }

        public override string P2()
        {
            string template = Input[0];
            Dictionary<string, string> longSub = Load();
            Dictionary<string, long[]> countsCollection = new();
            char[] subs = Load2();

            foreach (string pair in longSub.Keys)
            {
                long[] pcount = new long[100];
                Visit(0, pair[0], pair[1], subs, pcount, 20);
                countsCollection[pair] = pcount;
            }

            long[] counts = new long[100];

            for (int i = 0; i < template.Length - 1; ++i)
            {
                string p = template.Substring(i, 2);
                Visit2(0, p[0], p[1], subs, counts, 40, countsCollection);
            }

            ++counts[template.Last()];

            long max = counts.Where(p => p > 0).Max();
            long min = counts.Where(p => p > 0).Min();

            return (max - min).ToString();
        }
    }
}

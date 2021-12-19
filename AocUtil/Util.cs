using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace AoCUtil
{
    public static class Util
    {
        public static bool StrInRange(string s, int low, int high)
        {
            int val;
            return int.TryParse(s, out val) && val >= low && val <= high;
        }

        public static void SetProperty<T>(T obj, string param, string val)
        {
            Type t = typeof(T);
            FieldInfo fieldInfo = t.GetField(param);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, val);
            }
        }

        public static List<int> ParseNumbers(string[] lines)
        {
            return lines.Select(str => int.Parse(str)).ToList();
        }

        public static List<string> ParseRecords(string[] lines, string seperator)
        {
            List<string> results = new List<string>();
            string sum = string.Empty;
            bool first = true;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (!string.IsNullOrEmpty(sum))
                    {
                        results.Add(sum);
                        sum = string.Empty;
                    }

                    first = true;
                }
                else
                {
                    if (first)
                    {
                        sum = line;
                        first = false;
                    }
                    else
                    {
                        sum = $"{sum}{seperator}{line}";
                    }
                }
            }

            if (!string.IsNullOrEmpty(sum))
            {
                results.Add(sum);
            }

            return results;
        }

        public static List<List<string>> ParseSets(string[] lines)
        {
            List<List<string>> results = new List<List<string>>();

            List<string> current = new List<string>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (current.Count > 0)
                    {
                        results.Add(current);
                        current = new List<string>();
                    }
                }
                else
                {
                    current.Add(line);
                }
            }

            if (current.Count > 0)
            {
                results.Add(current);
            }

            return results;
        }

        public static List<T> ParseRecords<T>(string[] lines, string kvpSplit) where T : new()
        {
            List<T> results = new List<T>();

            List<string> textRecords = Util.ParseRecords(lines, " ");
            foreach (string s in textRecords)
            {
                T record = new T();

                string[] properties = s.Split(" ");

                foreach (string property in properties)
                {
                    if (!string.IsNullOrEmpty(property))
                    {
                        string[] kvp = property.Split(kvpSplit);
                        Util.SetProperty(record, kvp[0], kvp[1]);
                    }
                }

                results.Add(record);
            }

            return results;
        }

        public static string RevStr(string s)
        {
            var arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static void Swap<T>(ref T x, ref T y)
        {
            T tempswap = x;
            x = y;
            y = tempswap;
        }

        public static void LeftShiftArray<T>(T[] arr, int shift)
        {
            shift = shift % arr.Length;
            T[] buffer = new T[shift];
            Array.Copy(arr, buffer, shift);
            Array.Copy(arr, shift, arr, 0, arr.Length - shift);
            Array.Copy(buffer, 0, arr, arr.Length - shift, shift);
        }

        public static void RightShiftArray<T>(T[] arr, int shift)
        {
            shift = shift % arr.Length;
            T[] buffer = new T[shift];
            Array.Copy(arr, arr.Length - shift, buffer, 0, shift);
            Array.Copy(arr, 0, arr, shift, arr.Length - shift);
            Array.Copy(buffer, 0, arr, 0, shift);
        }

        public static BigInteger ParseBinary(string str) 
        {
            BigInteger result = 0;
            BigInteger adder = 1;

            for (int i = str.Length - 1; i >= 0; --i)
            {
                if (str[i] == '1')
                    result += adder;

                adder *= 2;
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    public static class Extensions
    {
        // Enumerable
        //

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static void ForEachBreakable<T>(this IEnumerable<T> enumeration, Func<T, bool> func)
        {
            foreach (T item in enumeration)
            {
                if (!func(item))
                    break;
            }
        }

        public static IEnumerable<string[]> Split(this IEnumerable<string> enumeration, char c)
        {
            foreach (string str in enumeration)
                yield return str.Split(c);
        }

        // This is a missing special case in linq
        public static ulong Sum(this IEnumerable<ulong> enumeration)
        {
            ulong sum = 0;
            enumeration.ForEach(v => sum += v);
            return sum;
        }

        // Lists
        //

        public static void Add<T>(this List<T> list, params T[] args)
        {
            foreach (T val in args)
            {
                list.Add(val);
            }
        }

        public static T Dequeue<T>(this List<T> list)
        {
            T val = list.FirstOrDefault();
            if (val != null)
                list.RemoveAt(0);
            return val;
        }

        public static void Push<T>(this List<T> list, T val) => list.Add(val);
        
        public static T Pop<T>(this List<T> list)
        {
            if (list.Count > 0)
            {
                T ret = list.Last();
                list.RemoveAt(list.Count - 1);
                return ret;
            }

            return default(T);
        }

        // String Parsing
        //

        public static int AsInt(this string str) => int.Parse(str);
        public static ulong AsULong(this string str) => ulong.Parse(str);

        // Arrays
        //

        public static void RotateLeft<T>(this T[] arr) => Util.LeftShiftArray(arr, 1);
        public static void RotateLeft<T>(this T[] arr, int amount) => Util.LeftShiftArray(arr, amount);
        public static void RotateRight<T>(this T[] arr) => Util.RightShiftArray(arr, 1);
        public static void RotateRight<T>(this T[] arr, int amount) => Util.RightShiftArray(arr, amount);
    }
}
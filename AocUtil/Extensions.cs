using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCUtil
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

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
    }
}
using System;
using System.Reflection;

namespace AdventOfCode
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
    }
}

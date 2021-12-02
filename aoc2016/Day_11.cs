//using AoCUtil;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace aoc2016
//{
//    class Day_11 : BetterBaseDay
//    {
//        private static string GetAbbr(string entry)
//        {
//            string[] spl = entry.Split(' ', StringSplitOptions.RemoveEmptyEntries);
//            return $"{spl[0][0]}{spl[1][0]}";
//        }

//        private static List<string> LoadItems(string floor)
//        {
//            List<string> list = new();

//            string[] spl = floor.Split(" a ");
//            for (int i = 1; i < spl.Length; ++i)
//                list.Add(GetAbbr(spl[i]));

//            return list;
//        }

//        private static Dictionary<int, List<string>> LoadMap(string[] input)
//        {
//            Dictionary<int, List<string>> map = new();
//            map[4] = new();
//            map[3] = LoadItems(input[2]);
//            map[2] = LoadItems(input[1]);
//            map[1] = LoadItems(input[0]);
//            map[1].Add("E");
//            return map;
//        }

//        private static int CurrentLevel(Dictionary<int, List<string>> map) => map.First(level => level.Value.Contains("E")).Key;

//        private static bool IsLegal(Dictionary<int, List<string>> map)
//        {
//            map.Where(level =>
//            {

//            });
//        }

//        private static void PrintMap(Dictionary<int, List<string>> map)
//        {
//            Console.WriteLine($"F4 {string.Join(" ", map[4])}");
//            Console.WriteLine($"F3 {string.Join(" ", map[3])}");
//            Console.WriteLine($"F2 {string.Join(" ", map[2])}");
//            Console.WriteLine($"F1 {string.Join(" ", map[1])}");
//        }

//        public override string Solve_1()
//        {
//            PrintMap(LoadMap(Input));

//            // Recursively iterate through possible moves with a depth count
//                // When evaluating a move, determine if both the floor we are departing and arriving at both are legal

//            return "no";
//        }

//        public override string Solve_2()
//        {
//            return "no";
//        }
//    }
//}

using AoCUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2015
{
    class Day_21 : BetterBaseDay
    {
        private record Stats(int Damage, int Armor)
        {
            public int Health { get; set; }
        }

        private record Item(int Cost, int Damage, int Armor);

        private static Item[] Weapons = { 
            new Item(8, 4, 0), 
            new Item(10, 5, 0), 
            new Item(25, 6, 0), 
            new Item(40, 7, 0),
            new Item(74, 8, 0)};

        private static Item[] Armors = {
            new Item(13, 0, 1),
            new Item(31, 0, 2),
            new Item(53, 0, 3),
            new Item(75, 0, 4),
            new Item(102, 0, 5)};

        private static Item[] Rings = {
            new Item(25, 1, 0),
            new Item(50, 2, 0),
            new Item(100, 3, 0),
            new Item(20, 0, 1),
            new Item(40, 0, 2),
            new Item(80, 0, 3)};

        private static Stats GetBoss() => new Stats(8, 2) { Health = 109 };

        private static bool Hit(Stats one, Stats two) => (two.Health -= Math.Max(one.Damage - two.Armor, 1)) <= 0;

        private static bool Sim(Stats player, Stats boss)
        {
            while (true)
            {
                if (Hit(player, boss))
                {
                    return true;
                }

                if (Hit(boss, player))
                {
                    return false;
                }
            }
        }

        private int _min = int.MaxValue;
        private int _max = int.MinValue;

        private void Fight(List<Item> items)
        {
            Stats player = new(items.Select(i => i.Damage).Sum(), items.Select(i => i.Armor).Sum()) { Health = 100 };
            if (Sim(player, GetBoss()))
            {
                _min = Math.Min(_min, items.Select(i => i.Cost).Sum());
            }
            else
            {
                _max = Math.Max(_max, items.Select(i => i.Cost).Sum());
            }
        }

        private void BuyRings(List<Item> items)
        {
            Fight(items);

            for (int first = 0; first < Rings.Length; ++first)
            {
                items.Add(Rings[first]);

                Fight(items);

                for (int second = first + 1; second < Rings.Length; ++second)
                {
                    items.Add(Rings[second]);

                    Fight(items);

                    items.Remove(Rings[second]);
                }

                items.Remove(Rings[first]);
            }
        }

        private void BuyArmor(List<Item> items)
        {
            BuyRings(items);

            foreach (Item armor in Armors)
            {
                items.Add(armor);

                BuyRings(items);

                items.Remove(armor);
            }
        }

        public override string Solve_1()
        {
            List<Item> items = new();

            foreach (Item weapon in Weapons)
            {
                items.Add(weapon);

                BuyArmor(items);

                items.Remove(weapon);
            }

            return _min.ToString();
        }

        public override string Solve_2()
        {
            return _max.ToString();
        }
    }
}

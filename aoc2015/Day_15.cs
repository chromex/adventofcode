using AoCUtil;
using System;
using System.Linq;

namespace aoc2015
{
    class Day_15 : BetterBaseDay
    {
        private record Ingredient(int Capacity, int Durability, int Flavor, int Texture, int Calories);

        private Ingredient[] GetIngredients() =>
            Input.Select(line =>
            {
                string[] spl = line.Split(" ");
                return new Ingredient(int.Parse(spl[2]), int.Parse(spl[4]), int.Parse(spl[6]), int.Parse(spl[8]), int.Parse(spl[10]));
            }).ToArray();

        private static int Score(Ingredient[] ingredients, int[] weight)
        {
            int capacity = 0, durability = 0, flavor = 0, texture = 0;

            for (int index = 0; index < ingredients.Length; ++index)
            {
                capacity += ingredients[index].Capacity * weight[index];
                durability += ingredients[index].Durability * weight[index];
                flavor += ingredients[index].Flavor * weight[index];
                texture += ingredients[index].Texture * weight[index];
            }

            return Math.Max(capacity, 0) * Math.Max(durability, 0) * Math.Max(flavor, 0) * Math.Max(texture, 0);
        }

        private static int Calories(Ingredient[] ingredients, int[] weight)
        {
            int cals = 0;

            for (int index = 0; index < ingredients.Length; ++index)
            {
                cals += ingredients[index].Calories * weight[index];
            }

            return cals;
        }

        public override string Solve_1()
        {
            Ingredient[] ingredients = GetIngredients();
            int highest = int.MinValue;

            for (int a = 1; a < 100; ++a)
            {
                for (int b = 1; b < 100; ++b)
                {
                    for (int c = 1; c < 100; ++c)
                    {
                        for (int d = 1; d < 100; ++d)
                        {
                            if ((a + b + c + d) == 100)
                            {
                                highest = Math.Max(Score(ingredients, new[] { a, b, c, d }), highest);
                            }
                        }
                    }
                }
            }

            return highest.ToString();
        }

        public override string Solve_2()
        {
            Ingredient[] ingredients = GetIngredients();
            int highest = int.MinValue;

            for (int a = 1; a < 100; ++a)
            {
                for (int b = 1; b < 100; ++b)
                {
                    for (int c = 1; c < 100; ++c)
                    {
                        for (int d = 1; d < 100; ++d)
                        {
                            int[] weights = new[] { a, b, c, d };
                            if ((a + b + c + d) == 100 && Calories(ingredients, weights) == 500)
                            {
                                highest = Math.Max(Score(ingredients, weights), highest);
                            }
                        }
                    }
                }
            }

            return highest.ToString();
        }
    }
}

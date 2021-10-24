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

            for (int a = 1; a <= 97; ++a)
            {
                int maxB = 97 - a + 1;

                for (int b = 1; b <= maxB; ++b)
                {
                    int maxC = maxB - b + 1;

                    for (int c = 1; c <= maxC; ++c)
                    {
                        int minD = 100 - (a + b + c);
                        int maxD = maxC - c + 1;

                        for (int d = minD; d <= maxD; ++d)
                        {
                            highest = Math.Max(Score(ingredients, new[] { a, b, c, d }), highest);
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

            for (int a = 1; a <= 97; ++a)
            {
                int maxB = 97 - a + 1;

                for (int b = 1; b <= maxB; ++b)
                {
                    int maxC = maxB - b + 1;

                    for (int c = 1; c <= maxC; ++c)
                    {
                        int minD = 100 - (a + b + c);
                        int maxD = maxC - c + 1;

                        for (int d = minD; d <= maxD; ++d)
                        {
                            int[] weights = new[] { a, b, c, d };
                            if (Calories(ingredients, weights) == 500)
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

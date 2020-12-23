using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day21
    {
        public static int Part1(IEnumerable<string> data)
        {
            var foods = parseFoods(data);
            var possibleIngredients = getPossibleIngredientsPerAllergen(foods);
            var actualIngredients = getActualIngredientsPerAllergen(possibleIngredients);

            return foods
                .Sum(food => food.ingredients!
                    .Count(i => !actualIngredients.Values.Contains(i)));
        }

        public static string Part2(IEnumerable<string> data)
        {
            var foods = parseFoods(data);
            var possibleIngredients = getPossibleIngredientsPerAllergen(foods);
            var actualIngredients = getActualIngredientsPerAllergen(possibleIngredients);

            return string.Join(",",
                actualIngredients.Keys
                    .OrderBy(a => a)
                    .Select(k => actualIngredients[k]));
        }

        private static List<(string[] ingredients, string[] allergens)> parseFoods(
            IEnumerable<string> data) =>
            data
                .Select(line => line
                    .TrimEnd(')')
                    .Split(" (contains "))
                .Select(halves => (
                    halves[0].Split(),
                    halves[1].Split(", ")))
                .ToList();

        private static Dictionary<string, string[][]> getPossibleIngredientsPerAllergen(
            IReadOnlyCollection<(string[] ingredients, string[] allergens)> foods) =>
            foods
                .SelectMany(f => f.allergens)
                .Distinct()
                .ToDictionary(
                    allergen => allergen,
                    allergen => foods
                        .Where(f => f.allergens!.Contains(allergen))
                        .Select(f => f.ingredients).ToArray());

        private static Dictionary<string, string> getActualIngredientsPerAllergen(
            IDictionary<string, string[][]> allergenToIngredientLists)
        {
            var allergenToIngredient = new Dictionary<string, string>();
            while (allergenToIngredientLists.Any())
            {
                foreach (var (allergen, foodIngredients) in allergenToIngredientLists)
                {
                    var possibleIngredients = foodIngredients
                        .Select(a => a.AsEnumerable())
                        .Aggregate((a, b) => a.Intersect(b)).ToArray();

                    if (possibleIngredients.Length == 1)
                    {
                        var matchingIngredient = possibleIngredients[0];
                        allergenToIngredient.Add(allergen, matchingIngredient);
                        allergenToIngredientLists.Remove(allergen);

                        foreach (var (key, value) in allergenToIngredientLists.ToArray())
                        {
                            allergenToIngredientLists[key] = value
                                .Select(allergen => allergen
                                    .Where(ingredient => ingredient != matchingIngredient)
                                    .ToArray())
                                .ToArray();
                        }

                        break;
                    }
                }
            }

            return allergenToIngredient;
        }
    }
}

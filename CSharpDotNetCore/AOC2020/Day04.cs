using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public static class Day04
    {
        public static int part1(string input) => parse(input).Count(isValid1);
        public static int part2(string input) => parse(input).Count(isValid2);

        private static IEnumerable<Dictionary<string, string>> parse(string input)
        {
            var result = new List<Dictionary<string, string>>();
            var next = new Dictionary<string, string>();

            foreach (var line in input.Split("\n"))
                if (string.IsNullOrEmpty(line))
                {
                    if (next.Count == 0) continue;
                    result.Add(next);
                    next = new Dictionary<string, string>();
                }
                else
                    foreach (var field in line.Split(" "))
                    {
                        var split = field.Split(":");
                        next.Add(split[0], split[1]);
                    }

            if (next.Count > 0) result.Add(next);

            return result;
        }

        private static bool isValid1(Dictionary<string,string> input) =>
            input.ContainsKey("cid")
                ? input.Count == 8
                : input.Count == 7;

        private static bool isValid2(Dictionary<string, string> input) => 
            isValid1(input) && input.All(validField);

        private static bool validField(KeyValuePair<string, string> field) =>
            field.Key switch
            {
                "byr" => isInt(field.Value, 1920, 2002),
                "iyr" => isInt(field.Value, 2010, 2020),
                "eyr" => isInt(field.Value, 2020, 2030),
                "hgt" => isHeight(field.Value),
                "hcl" => isHairColor(field.Value),
                "ecl" => isEyeColor(field.Value),
                "pid" => isId(field.Value),
                "cid" => true,
                _ => false
            };

        private static bool isInt(string value, int min, int max)
        {
            try
            {
                var year = int.Parse(value);
                return year >= min && year <= max;
            }
            catch
            {
                return false;
            }
        }

        private static bool isHeight(string value) =>
            value.EndsWith("cm")
                ? isInt(value.Replace("cm", ""), 150, 193)
                : value.EndsWith("in") && 
                  isInt(value.Replace("in", ""), 59, 76);

        private static bool isHairColor(string value) =>
            Regex.IsMatch(value, "^#([0-9]|[a-f]){6}$");

        static readonly HashSet<string> validEyeColors = 
            new HashSet<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        private static bool isEyeColor(string value) => 
            validEyeColors.Contains(value);

        private static bool isId(string value) =>
            Regex.IsMatch(value, "^[0-9]{9}$");
    }
}

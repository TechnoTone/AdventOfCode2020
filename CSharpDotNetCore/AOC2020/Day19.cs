using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public static class Day19
    {
        public static int Part1(string data)
        {
            var sections = data.Replace("\r", "").Split("\n\n");
            var lookup = parseRules(sections[0].Lines());

            var regex = $"^{GenerateRegex(lookup)}$";
            return sections[1].Lines().Count(x => Regex.IsMatch(x, regex));
        }

        public static int Part2(string data)
        {
            var sections = data.Replace("\r", "").Split("\n\n");
            var lookup = parseRules(sections[0].Lines());

            lookup["8"] = "( 42 | 42 8 )";
            lookup["11"] = "( 42 31 | 42 11 31 )";

            var regex = $"^{GenerateRegex(lookup)}$";
            return sections[1].Lines().Count(x => Regex.IsMatch(x, regex));
        }

        private static Dictionary<string, string> parseRules(IEnumerable<string> rules)
        {
            var lookup = new Dictionary<string, string>();

            foreach (var rule in rules)
            {
                var split = rule.Split(":");

                var value = split[1].Replace("\"", "").Trim();
                if (value.Contains("|")) value = $"( {value} )";

                lookup.Add(split[0], value);
            }

            return lookup;
        }

        private static string GenerateRegex(IReadOnlyDictionary<string, string> lookup)
        {
            const int MAX_SIZE = 50000;
            
            var current = lookup["0"].Split(" ").ToList();
            while (current.Any(x => x.Any(char.IsDigit)) && current.Count < MAX_SIZE)
            {
                current = current
                    .Select(x => lookup.ContainsKey(x) ? lookup[x] : x)
                    .SelectMany(x => x.Split(" "))
                    .ToList();
            }

            //for part 2, remove all the remaining 8's and 11's from the resultant regex
            //part 1 wont have them anyway so it doesn't matter.
            current.Remove("8");
            current.Remove("11");

            return string.Join("", current);
        }
    }

}

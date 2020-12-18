using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public static class Day18
    {
        private const string BRACKET_SEARCH_PATTERN = @"(\([\d\s\*\+]+\))";

        public static long Part1(IEnumerable<string> data) => data.Select(Evaluate1).Sum();

        public static long Part2(IEnumerable<string> data) => data.Select(Evaluate2).Sum();

        public static long Evaluate1(string input)
        {
            while (Regex.IsMatch(input, BRACKET_SEARCH_PATTERN))
            {
                var match = Regex.Match(input, BRACKET_SEARCH_PATTERN);
                input = input.Remove(match.Index, match.Length);
                input = input.Insert(match.Index, Evaluate1(match.Value.Substring(1, match.Length - 2)).ToString());
            }

            var result = 0L;
            var operation = ' ';
            foreach (var (s,ix) in input.Split(" ").Select((s,ix)=>(s,ix)))
            {
                if (ix == 0)
                    result = long.Parse(s);
                else switch (s)
                {
                    case "*":
                        operation = '*';
                        break;
                    case "+":
                        operation = '+';
                        break;
                    default:
                        switch (operation)
                        {
                            case '*':
                                result *= long.Parse(s);
                                break;
                            case '+':
                                result += long.Parse(s);
                                break;
                        }

                        break;
                }
            }

            return result;
        }

        public static long Evaluate2(string input)
        {
            while (Regex.IsMatch(input, BRACKET_SEARCH_PATTERN))
            {
                var match = Regex.Match(input, BRACKET_SEARCH_PATTERN);
                input = input.Remove(match.Index, match.Length);
                input = input.Insert(match.Index, Evaluate2(match.Value.Substring(1, match.Length - 2)).ToString());
            }

            return
                input.Contains("*")
                    ? input.Split(" * ").Select(Evaluate2).Aggregate((a, b) => a * b)
                    : input.Split(" + ").Sum(long.Parse);
        }

    }

}

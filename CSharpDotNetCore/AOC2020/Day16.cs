using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    public static class Day16
    {
        public static long Part1(string data)
        {
            var dataGroups = data.Replace("\r", "").Split("\n\n").ToList();
            var rules = readRules(dataGroups[0].Lines()).ToList();
            var nearbyTickets = readTickets(dataGroups[2].Lines().Skip(1));

            return nearbyTickets.Select(t => getErrors(rules, t)).Sum();
        }

        public static long Part2(string data)
        {
            var ticket = yourTicket(data);

            return ticket.Keys
                .Where(k => k.StartsWith("departure"))
                .Select(k => ticket[k])
                .Aggregate((a, b) => a * b);
        }

        public static Dictionary<string, long> yourTicket(string data)
        {
            var dataGroups = data.Replace("\r", "").Split("\n\n").ToList();
            var rules = readRules(dataGroups[0].Lines()).ToList();
            var ticket = readTickets(dataGroups[1].Lines().Skip(1)).First();
            var nearbyTickets = readTickets(dataGroups[2].Lines().Skip(1));
            var validTickets = nearbyTickets.Where(t => getErrors(rules, t) == 0).ToList();

            var fieldIndices = validTickets.First().Select((v, ix) => ix).ToList();

            var result = new Dictionary<string, long>();

            while (fieldIndices.Count > 0)
                foreach (var rule in rules.Where(r => !result.ContainsKey(r.FieldName)))
                {
                    var matchingFields =
                        fieldIndices
                            .Where(fieldIndex =>
                                validTickets.All(t =>
                                    rule.IsValid(t[fieldIndex])))
                            .ToList();

                    if (matchingFields.Count != 1) continue;
                    
                    result.Add(rule.FieldName, ticket[matchingFields[0]]);
                    fieldIndices.Remove(matchingFields[0]);

                    if (result.Keys.Count(k => k.StartsWith("departure")) == 6)
                        //nasty hack to break the look as soon as all "departure" fields are identified
                        return result;

                }

            return result;
        }


        private static IEnumerable<Rule> readRules(IEnumerable<string> rules) =>
            rules.IgnoreEmptyLines().Select(Rule.Parse);

        private readonly struct Rule
        {
            public readonly string FieldName;
            private readonly long min1;
            private readonly long max1;
            private readonly long min2;
            private readonly long max2;

            private Rule(string fieldName, long min1, long max1, long min2, long max2)
            {
                FieldName = fieldName;
                this.min1 = min1;
                this.max1 = max1;
                this.min2 = min2;
                this.max2 = max2;
            }

            public bool IsValid(long value) =>
                value >= min1 && value <= max1 ||
                value >= min2 && value <= max2;

            public static Rule Parse(string input)
            {
                var matches = Regex.Match(input, @"^(.+): (\d+)-(\d+) or (\d+)-(\d+)");
                return new Rule(
                    matches.Groups[1].Value,
                    long.Parse(matches.Groups[2].Value),
                    long.Parse(matches.Groups[3].Value),
                    long.Parse(matches.Groups[4].Value),
                    long.Parse(matches.Groups[5].Value));
            }
        }

        private static IEnumerable<List<long>> readTickets(IEnumerable<string> tickets) =>
            tickets.IgnoreEmptyLines().Select(t => t.ParseCommaSeparatedIntegers());

        private static long getErrors(IEnumerable<Rule> rules, IEnumerable<long> ticket) => 
            ticket.Sum(field => getError(rules, field));

        private static long getError(IEnumerable<Rule> rules, long field) =>
            rules.Any(rule => rule.IsValid(field))
                ? 0
                : field;
    }
}

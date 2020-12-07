using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public class Day07
    {
        private readonly Dictionary<string, List<QuantityAndBag>> parsed;

        public Day07(string input)
        {
            parsed = parse(input);
        }
        
        public int Part1()
        {
            var reversed = reverse(parsed);
            
            var result = new HashSet<string>();
            var queue = new List<string> {"shiny gold"};
            
            while (queue.Count > 0)
            {
                queue = queue
                    .Where(q=>reversed.ContainsKey(q))
                    .SelectMany(q => reversed[q])
                    .Where(q => !result.Contains(q)).ToList();
                
                foreach (var n in queue) result.Add(n);
            }

            return result.Count;
        }

        public int Part2() => count("shiny gold");

        private int count(string bag) =>
            parsed.ContainsKey(bag)
                ? parsed[bag].Select(b => b.Quantity + b.Quantity * count(b.Bag)).Sum()
                : 0;

        private readonly struct QuantityAndBag : IComparable<QuantityAndBag>
        {
            public readonly int Quantity;
            public readonly string Bag;

            private QuantityAndBag(int quantity, string bag)
            {
                Quantity = quantity;
                Bag = bag;
            }

            public static QuantityAndBag Parse(string input) => 
                new QuantityAndBag(int.Parse(input.Substring(0, 1)), input.Substring(2));

            public int CompareTo(QuantityAndBag other) => 
                string.Compare(Bag, other.Bag, StringComparison.CurrentCultureIgnoreCase);
        }
        
        private static Dictionary<string, List<QuantityAndBag>> parse(string input)
        {
            var result = new Dictionary<string, List<QuantityAndBag>>();
            var lines = input.Replace("\r", "").Split("\n").Where(s => !string.IsNullOrEmpty(s));
            foreach (var line in lines)
            {
                var lr = line.Split(" contain ");
                var container = lr[0].Replace(" bags", "");
                if (lr[1].Contains("no ")) continue;
                var contents = parseBags(lr[1]);
                result.Add(container, contents);
            }
            
            return result;
        }

        private static List<QuantityAndBag> parseBags(string input) =>
            input
                .Replace(" bags", "")
                .Replace(" bag", "")
                .Replace(".", "")
                .Split(", ").Select(QuantityAndBag.Parse)
                .ToList();

        private static Dictionary<string, List<string>> reverse(Dictionary<string, List<QuantityAndBag>> bags)
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var (key, values) in bags)
                values.ForEach(v =>
                {
                    if (!result.TryAdd(v.Bag, new List<string> {key}))
                        result[v.Bag].Add(key);
                });

            return result;
        }
    }
}

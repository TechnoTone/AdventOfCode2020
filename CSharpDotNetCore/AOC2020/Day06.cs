using System.Collections.Immutable;
using System.Linq;

namespace AOC2020
{
    public static class Day06
    {
        public static int part1(string input)
        {
            var answerGroups = input.Replace("\r", "").Split("\n\n").Select(g => g.Replace("\n", ""));
            var groupCounts = answerGroups.Select(answers => answers.ToHashSet().Count);
            return groupCounts.Sum();
        }

        public static int part2(string input)
        {
            var answerGroups = input.Replace("\r", "").Split("\n\n")
                .Select(g => g.Split("\n").Where(s => !string.IsNullOrEmpty(s)));

            return answerGroups.Select(g => g
                    .Select(line => line.ToImmutableHashSet())
                    .Aggregate((a, b) => a.Intersect(b))
                    .Count)
                .Sum();
        }
    }
}

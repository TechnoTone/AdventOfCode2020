using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day02
    {
        public static int part1(IEnumerable<string> input) => validPasswords(input);

        private static int validPasswords(IEnumerable<string> input) => input.Where(isValid1).Count();

        private static bool isValid1(string input)
        {
            var parts = input.Split(" ");
            var min = int.Parse(parts[0].Split("-")[0]);
            var max = int.Parse(parts[0].Split("-")[1]);
            var letter = parts[1].Substring(0, 1);
            var password = parts[2];

            var count = countLetter1(password, letter);

            return count >= min && count <= max;
        }

        private static int countLetter1(string password, string letter)
        {
            var count = 0;
            var pos = password.IndexOf(letter);
            while (pos >= 0)
            {
                count++;
                pos = password.IndexOf(letter, pos+1);
            }

            return count;
        }

        public static int part2(IEnumerable<string> input) => input.Where(isValid2).Count();

        private static bool isValid2(string input)
        {
            var parts = input.Split(" ");
            var p1 = int.Parse(parts[0].Split("-")[0]);
            var p2 = int.Parse(parts[0].Split("-")[1]);
            var letter = parts[1][0];
            var password = parts[2];

            return password[p1 - 1] == letter ^ password[p2 - 1] == letter;
        }

    }
}

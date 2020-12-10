using System.Collections.Generic;

namespace AOC2020
{
    public static class Day10
    {
        public static int Part1(List<int> data)
        {
            data.Sort();

            var d1 = 0;
            var d3 = 1;
            var last = 0;
            data.ForEach(x =>
            {
                if (x - last == 1) d1++;
                if (x - last == 3) d3++;
                last = x;
            });

            return d1 * d3;
        }
        
        public static long Part2(List<int> data)
        {
            data.Sort();
            
            var steps = new List<int>();
            var last = 0;
            data.ForEach(x =>
            {
                steps.Add(x - last);
                last = x;
            });

            var permutations = new[] {1, 1, 2, 4, 7};
            
            long result = 1;
            var count = 0;
            steps.Add(0);
            steps.ForEach(x =>
            {
                if (x == 1)
                    count++;
                else if (count > 0)
                {
                    result *= permutations[count];

                    count = 0;
                }
            });

            return result;
        }

    }
}

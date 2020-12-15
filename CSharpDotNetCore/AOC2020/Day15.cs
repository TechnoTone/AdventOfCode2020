using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day15
    {
        public static long Part1(List<long> data) => getTarget(data, 2020);

        public static long Part2(List<long> data) => getTarget(data, 30000000);

        private static long getTarget(IReadOnlyList<long> data, int target)
        {
            var spoken = data.ToList();
            var lookup = new Dictionary<long, long>();
            for (var i = 0; i < data.Count; i++) lookup[data[i]] = i;

            while (spoken.Count < target)
            {
                var i = spoken.Count;
                var lastNum = spoken.Last();
                if (lookup.ContainsKey(lastNum))
                {
                    var nextNum = i - lookup[lastNum] - 1;
                    lookup[lastNum] = i - 1;
                    spoken.Add(nextNum);
                }
                else
                {
                    lookup[lastNum] = i - 1;
                    spoken.Add(0);
                }
            }

            return spoken.Last();
        }

        
        
    }
}

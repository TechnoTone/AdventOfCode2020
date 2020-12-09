using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day09
    {
        public static long Part1(List<long> data, int preamble)
        {
            var chunk = data.Take(preamble).ToList();
            for (var ix = preamble; ix < data.Count; ix++)
            {
                var next = data[ix];
                
                if (!chunk.Any(x => chunk.Any(y => x != y && x + y == next)))
                    return next;

                chunk.RemoveAt(0);
                chunk.Add(next);
            }

            return 0;
        }
        
        public static long Part2(List<long> data, int target)
        {
            var ix = 0;
            while (true)
            {
                var n = 2;
                while (true)
                {
                    var chunk = data.Skip(ix).Take(n).ToList();
                    var total = chunk.Sum();

                    if (total == target)
                        return chunk.Min() + chunk.Max();

                    if (total > target)
                        break;

                    n++;
                }

                ix++;

                if (ix >= data.Count)
                    break;
            }

            return 0;
        }

    }
}

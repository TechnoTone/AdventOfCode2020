using System.Collections.Generic;
using System.Linq;
using static AOC2020.HelperFunctions;

namespace AOC2020
{
    public static class Day13
    {
        public static int Part1(IEnumerable<string> data)
        {
            var startTime = int.Parse(data.First());
            var busses = data.Last().Split(",").Where(bus => bus != "x").Select(int.Parse);
            var nextBus = busses.OrderBy(bus => remains(bus, startTime)).First();
            return nextBus * remains(nextBus, startTime);
        }

        public static long Part2(IEnumerable<string> data)
        {
            var busses = data.Last()
                .Split(',')
                .Select((bus, ix) => (bus, ix))
                .Where(t => !t.bus.Equals("x"))
                .Select(t => (bus: long.Parse(t.bus), t.ix))
                .ToList();
            
            long timestamp = 0;
            var period = busses[0].bus;
            
            for (var i = 2; i <= busses.Count; i++)
            {
                while (busses.Take(i).Any(t => (timestamp + t.ix) % t.bus != 0)) 
                    timestamp += period;

                period = LCM(period, busses[i - 1].bus);
            }

            return timestamp;
        }

        private static int remains(int busId, long time) =>
            (int) (time % busId) == 0 ? 0 : busId - (int) (time % busId);

    }
}

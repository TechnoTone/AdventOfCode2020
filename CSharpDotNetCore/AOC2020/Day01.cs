using System.Collections.Generic;

namespace AOC2020
{
    public static class Day01
    {
        public static int findSumOfPair(List<int> expenses)
        {
            for (var i = 0; i < expenses.Count - 1; i++)
            for (var j = i + 1; j < expenses.Count; j++)
                if (expenses[i] + expenses[j] == 2020)
                    return expenses[i] * expenses[j];

            return 0;
        }

        public static int findSumOfTriplet(List<int> expenses)
        {
            for (var i = 0; i < expenses.Count-2; i++)
            for (var j = i + 1; j < expenses.Count - 1; j++)
            for (var k = j + 1; k < expenses.Count; k++)
                if (expenses[i] + expenses[j] + expenses[k] == 2020)
                    return expenses[i] * expenses[j] * expenses[k];

            return 0;
        }
    }
}

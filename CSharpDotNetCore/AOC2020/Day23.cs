using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day23
    {
        public static int Part1(string data)
        {
            var cups = new LinkedList<int>(data.Select(c => (int) c - 48));
            
            var cup1 = playMoves(cups, 100);

            var result = "";
            8.Repeat(() =>
            {
                cup1 = cup1.NextCircular();
                result += cup1.Value.ToString();
            });

            return int.Parse(result);
        }

        public static long Part2(string data)
        {
            var cupLabels = data.Select(c => (int) c - 48).ToList();
            cupLabels.AddRange(Enumerable.Range(10, 1_000_000 - 9));
            var cups = new LinkedList<int>(cupLabels);
            
            var cup1 = playMoves(cups, 10_000_000);

            return (long) cup1.NextCircular().Value * cup1.NextCircular().NextCircular().Value;
        }

        private static LinkedListNode<int> playMoves(LinkedList<int> cups, int count)
        {
            var lookup = new Dictionary<int, LinkedListNode<int>>();
            var currentCup = cups.First;
            while (currentCup != null)
            {
                lookup.Add(currentCup.Value, currentCup);
                currentCup = currentCup.Next;
            }

            currentCup = cups.First;
            var hand = new Stack<LinkedListNode<int>>();

            void pickUpNextCup()
            {
                hand.Push(currentCup.NextCircular());
                cups.Remove(currentCup.NextCircular());
            }

            while (count>0)
            {
                pickUpNextCup();
                pickUpNextCup();
                pickUpNextCup();

                var destination = currentCup.Value - 1;
                while (destination == 0 || hand.Any(c => c.Value == destination))
                    destination = destination == 0 ? lookup.Count: destination - 1;

                cups.AddAfter(lookup[destination], hand.Pop());
                cups.AddAfter(lookup[destination], hand.Pop());
                cups.AddAfter(lookup[destination], hand.Pop());

                currentCup = currentCup.NextCircular();
                count--;
            }

            return lookup[1];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day22
    {
        public static int Part1(string data)
        {
            var (deck1, deck2) = getDecks(data);

            while (deck1.Count > 0 && deck2.Count > 0)
                playRound(deck1, deck2);

            return getScore(deck1) + getScore(deck2);
        }
    
        public static int Part2(string data)
        {
            var (deck1, deck2) = getDecks(data);

            playRecursive(deck1, deck2);
            return getScore(deck1) + getScore(deck2);
        }

        private static (Queue<int>,Queue<int>) getDecks(string data)
        {
            var decks = data
                .Replace("\r", "")
                .Split("\n\n")
                .Select(deck => deck
                    .Lines()
                    .IgnoreEmptyLines()
                    .Skip(1)
                    .Select(int.Parse)
                    .ToQueue())
                .ToList();
            
            return (decks[0], decks[1]);
        }


        private static void playRound(Queue<int> deck1, Queue<int> deck2)
        {
            var c1 = deck1.Dequeue();
            var c2 = deck2.Dequeue();

            if (c1 > c2)
            {
                deck1.Enqueue(c1);
                deck1.Enqueue(c2);
            }
            else
            {
                deck2.Enqueue(c2);
                deck2.Enqueue(c1);
            }
        }
        
        private static void playRecursive(Queue<int> player1, Queue<int> player2)
        {
            var dp = new HashSet<int>();
            while (player1.Count > 0 && player2.Count > 0)
            {
                playRecursiveRound(player1, player2, dp);
            }
        }
        
        private static void playRecursiveRound(Queue<int> deck1, Queue<int> deck2, ISet<int> history)
        {
            var hash = HashCode.Combine(deck1.GetSequenceHashCode(), deck2.GetSequenceHashCode());

            if (history.Contains(hash))
            {
                deck2.Clear();
                return;
            }

            history.Add(hash);

            var c1 = deck1.Dequeue();
            var c2 = deck2.Dequeue();

            if (deck1.Count >= c1 && deck2.Count >= c2)
            {
                var sub1 = new Queue<int>(deck1.Take(c1));
                var sub2 = new Queue<int>(deck2.Take(c2));

                playRecursive(sub1, sub2);
                if (sub1.Count > 1)
                {
                    deck1.Enqueue(c1);
                    deck1.Enqueue(c2);
                }
                else
                {
                    deck2.Enqueue(c2);
                    deck2.Enqueue(c1);
                }
            }
            else if (c1 > c2)
            {
                deck1.Enqueue(c1);
                deck1.Enqueue(c2);
            }
            else if (c2 > c1)
            {
                deck2.Enqueue(c2);
                deck2.Enqueue(c1);
            }
        }
        
        private static int getScore(Queue<int> deck)
        {
            var score = 0;
            while (deck.Count > 0)
                score += deck.Count * deck.Dequeue();

            return score;
        }
    }
}

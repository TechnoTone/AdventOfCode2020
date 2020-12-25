namespace AOC2020
{
    public static class Day25
    {
        public static int Part1(int cardPublicKey, int doorPublicKey)
        {
            var cardLoopSize = getLoopSize(cardPublicKey);
            return transform(doorPublicKey, cardLoopSize);
        }

        private static int getLoopSize(int publicKey)
        {
            const int SUBJECT = 7;
            var loop = 0;
            long value = 1;

            while (value != publicKey)
            {
                loop++;
                value *= SUBJECT;
                value %= 20201227;
            }

            return loop;
        }

        private static int transform(in int key, in int loopSize)
        {
            long value = 1;
            for (var loop = 0; loop < loopSize; loop++)
            {
                value *= key;
                value %= 20201227;
            }

            return (int)value;
        }
    }
}

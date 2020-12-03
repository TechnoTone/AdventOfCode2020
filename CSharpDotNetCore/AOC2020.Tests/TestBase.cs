using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020.Tests
{
    public class TestBase
    {
        internal readonly InputReader input;

        internal class InputReader
        {
            private const string INPUT_PATH = "../../../../input";
            private readonly int day;

            public InputReader(int day)
            {
                this.day = day;
            }

            internal string readAllText() =>
                File.ReadAllText($"{INPUT_PATH}/day{day:00}.txt");
            
            private IEnumerable<string> readAllLines() =>
                File.ReadAllLines($"{INPUT_PATH}/day{day:00}.txt");

            internal List<string> linesOfStrings() => 
                readAllLines().ToList();

            internal List<int> linesOfIntegers() =>
                linesOfStrings().ConvertAll(int.Parse);

            internal List<string> commaSeparatedStrings() =>
                readAllText().Split(',').ToList();

            internal List<long> commaSeparatedIntegers() =>
                commaSeparatedStrings().ConvertAll(long.Parse);

            internal Tuple<int, int> range()
            {
                var ns = readAllText().Split('-').ToList().ConvertAll(int.Parse);
                return new Tuple<int, int>(ns[0], ns[1]);
            }
        }

        protected TestBase(int day) => input = new InputReader(day);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day14
    {
        public static long Part1(IEnumerable<string> program)
        {
            long andMask = 0;
            long orMask = 0;
            var memory = new Dictionary<long, long>();
            
            foreach (var line in program)
            {
                if (isMask(line))
                {
                    var mask = line.Substring(7);
                    andMask = Convert.ToInt64(mask.Replace("X", "1"), 2);
                    orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
                }
                else
                {
                    var instruction = readInstruction(line);
                    memory[instruction.Address] = (instruction.Value & andMask) | orMask;
                }
            }

            return memory.Values.Sum();
        }

        public static long Part2(IEnumerable<string> program)
        {
            long orMask = 0;
            long fuzzMask = 0;
            var memory = new Dictionary<long, long>();

            foreach (var line in program)
            {
                if (isMask(line))
                {
                    var mask = line.Substring(7);
                    orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
                    fuzzMask = Convert.ToInt64(mask.Replace("1", "0").Replace("X", "1"), 2);
                }
                else
                {
                    var instruction = readInstruction(line);
                    foreach (var address in getAddresses(instruction.Address | orMask, fuzzMask))
                        memory[address] = instruction.Value;
                }
            }

            return memory.Values.Sum();
        }

        private static IEnumerable<long> getAddresses(long instructionAddress, in long mask)
        {
            var options = new List<long>();
            long i = 1;
            while (i < 68719476736)
            {
                if ((mask & i) > 0) options.Add(i);
                i *= 2;
            }

            var zeroed = instructionAddress & ~mask;
            return options.Combinations().Select(x => zeroed | x.Sum());
        }

        private static bool isMask(string line) => line.StartsWith("mask");

        private struct Instruction
        {
            public long Address;
            public long Value;
        }

        private static Instruction readInstruction(string line) =>
            new Instruction
            {
                Address = long.Parse(line.Substring(4, line.IndexOf("]") - 4)),
                Value = long.Parse(line.Substring(line.IndexOf("=") + 2))
            };
        
    }
}

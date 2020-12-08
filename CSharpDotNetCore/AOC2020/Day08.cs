using System.Collections.Generic;

namespace AOC2020
{
    public class Day08
    {
        private readonly List<string> program;

        public Day08(List<string> program)
        {
            this.program = program;
        }

        public int part1()
        {
            var computer = new OpComputer(program);
            return computer.RunUntilLooped();
        }
        
        public int part2()
        {
            var computer = new OpComputer(program);
            return computer.FixCorruptionAndRun();
        }

    }
}

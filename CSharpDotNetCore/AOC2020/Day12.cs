using System.Collections.Generic;
using System.Drawing;

namespace AOC2020
{
    public static class Day12
    {
        public static int Part1(IEnumerable<string> data)
        {
            var position = Point.Empty;
            var direction = CompassDirection.East;

            void turnLeft(int amount)
            {
                while (amount > 0)
                {
                    direction = direction.TurnLeft();
                    amount -= 90;
                }
            }
            
            void turnRight(int amount)
            {
                while (amount > 0)
                {
                    direction = direction.TurnRight();
                    amount -= 90;
                }
            }
            
            void perform(string instruction)
            {
                var (movement, amount) = parse(instruction);
                switch (movement)
                {
                    case 'N':
                        position.Move(CompassDirection.North, amount);
                        break;
                    case 'S':
                        position.Move(CompassDirection.South, amount);
                        break;
                    case 'E':
                        position.Move(CompassDirection.East, amount);
                        break;
                    case 'W':
                        position.Move(CompassDirection.West, amount);
                        break;
                    case 'L':
                        turnLeft(amount);
                        break;
                    case 'R':
                        turnRight(amount);
                        break;
                    case 'F':
                        position.Move(direction, amount);
                        break;
                }
            }
            
            foreach (var s in data) perform(s);
            return position.ManhattanDistance();
        }


        public static int Part2(IEnumerable<string> data)
        {
            var position = Point.Empty;
            var waypoint = new Point(10, 1);

            void turnLeft(int amount) => turnRight(360 - amount);

            void turnRight(int amount)
            {
                var x = waypoint.X;
                var y = waypoint.Y;

                switch (amount)
                {
                    case 180:
                        waypoint.X *= -1;
                        waypoint.Y *= -1;
                        break;
                    case 90:
                    {
                        waypoint.X = y;
                        waypoint.Y = -x;
                        break;
                    }
                    case 270:
                    {
                        waypoint.X = -y;
                        waypoint.Y = x;
                        break;
                    }
                }
            }

            void perform(string instruction)
            {
                var (movement, amount) = parse(instruction);
                switch (movement)
                {
                    case 'N':
                        waypoint.Move(CompassDirection.North, amount);
                        break;
                    case 'S':
                        waypoint.Move(CompassDirection.South, amount);
                        break;
                    case 'E':
                        waypoint.Move(CompassDirection.East, amount);
                        break;
                    case 'W':
                        waypoint.Move(CompassDirection.West, amount);
                        break;
                    case 'L':
                        turnLeft(amount);
                        break;
                    case 'R':
                        turnRight(amount);
                        break;
                    case 'F':
                        position.X += waypoint.X * amount;
                        position.Y += waypoint.Y * amount;
                        break;
                }
            }

            foreach (var s in data) perform(s);
            return position.ManhattanDistance();
        }
        
        private static (char, int) parse(string instruction) => 
            (instruction[0], int.Parse(instruction.Substring(1)));
    }
}

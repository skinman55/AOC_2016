using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_1
{
    class Program
    {
        enum Direction
        {
            North,
            South,
            East,
            West
        };
        
        private static string input = "L1, L3, L5, L3, R1, L4, L5, R1, R3, L5, R1, L3, L2, L3, R2, R2, L3, L3, R1, L2, R1, L3, L2, R4, R2, L5, R4, L5, R4, L2, R3, L2, R4, R1, L5, L4, R1, L2, R3, R1, R2, L4, R1, L2, R3, L2, L3, R5, L192, R4, L5, R4, L1, R4, L4, R2, L5, R45, L2, L5, R4, R5, L3, R5, R77, R2, R5, L5, R1, R4, L4, L4, R2, L4, L1, R191, R1, L1, L2, L2, L4, L3, R1, L3, R1, R5, R3, L1, L4, L2, L3, L1, L1, R5, L4, R1, L3, R1, L2, R1, R4, R5, L4, L2, R4, R5, L1, L2, R3, L4, R2, R2, R3, L2, L3, L5, R3, R1, L4, L3, R4, R2, R2, R2, R1, L4, R4, R1, R2, R1, L2, L2, R4, L1, L2, R3, L3, L5, L4, R4, L3, L1, L5, L3, L5, R5, L5, L4, L2, R1, L2, L4, L2, L4, L1, R4, R4, R5, R1, L4, R2, L4, L2, L4, R2, L4, L1, L2, R1, R4, R3, R2, R2, R5, L1, L2";
        
        //private static string input = "R8, R4, R4, R8";
        //private static string input = "R5, L5, R5, R3";
        private static int numVerticalSteps = 0;
        private static int numHorizontalSteps = 0;
        private static bool foundFirstDuplicateVisit = false; 

        private static int x = 0;
        private static int y = 0;
        private static List<string> vistedlocations = new List<string>();

        static Direction CurrentDirection = Direction.North;

        static void Main(string[] args)
        {
            var instructions = input.Split(',');
            foreach (var instruction in instructions)
            {
                var value = instruction.Trim();
                switch (value[0])
                {
                    case 'L':
                        MoveLeft(int.Parse(value.Replace("L","")));
                        break;

                    case 'R':
                        MoveRight(int.Parse(value.Replace("R","")));
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            var numBlocks = Math.Abs(numVerticalSteps + numHorizontalSteps);
            Console.WriteLine("Total Distance");
            Console.WriteLine(numBlocks);
            Console.ReadLine();
        }

        private static void MoveNorth(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                y++;
                numVerticalSteps++;
                StoreVisitedLocation();
            }
        }

        private static void MoveSouth(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                y--;
                numVerticalSteps--;
                StoreVisitedLocation();
            }
        }

        private static void MoveEast(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                x++;
                numHorizontalSteps++;
                StoreVisitedLocation();
            }
            
        }

        private static void MoveWest(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                x--;
                numHorizontalSteps--;
                StoreVisitedLocation();
            }
        }

        private static void StoreVisitedLocation()
        {
            if (foundFirstDuplicateVisit)
                return;

            var coordinates = $"{x},{y}";


            if (vistedlocations.Contains(coordinates))
            {
                foundFirstDuplicateVisit = true;
                // break;
                Console.WriteLine("Visited Location Twice.  Distance From Start:");
                var numBlocks = Math.Abs(numVerticalSteps + numHorizontalSteps);
                Console.WriteLine(numBlocks);
                //Console.ReadLine();
            }
            else
            {
                vistedlocations.Add(coordinates);
            }
        }

        private static void MoveLeft(int steps)
        {
            TurnLeft();
            switch (CurrentDirection)
            {
                case Direction.North:
                    MoveNorth(steps);
                    break;

                case Direction.South:
                    MoveSouth(steps);
                    break;

                case Direction.East:
                    MoveEast(steps);
                    break;

                case Direction.West:
                    MoveWest(steps);
                    break;
            }
        }

     
        private static void MoveRight(int steps)
        {
            TurnRight();
            switch (CurrentDirection)
            {
                case Direction.North:
                    MoveNorth(steps);
                    break;
                case Direction.South:
                    MoveSouth(steps);
                    break;
                case Direction.East:
                    MoveEast(steps);
                    break;
                case Direction.West:
                    MoveWest(steps);
                    break;
            }
        }

        private static void TurnRight()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    CurrentDirection = Direction.East;
                    break;
                case Direction.South:
                    CurrentDirection = Direction.West;
                    break;
                case Direction.East:
                    CurrentDirection = Direction.South;
                    break;
                case Direction.West:
                    CurrentDirection = Direction.North;
                    break;
            }
        }

        private static void TurnLeft()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    CurrentDirection = Direction.West;
                    break;
                case Direction.South:
                    CurrentDirection = Direction.East;
                    break;
                case Direction.East:
                    CurrentDirection = Direction.North;
                    break;
                case Direction.West:
                    CurrentDirection = Direction.South;
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var dd = new DisplayDecoder();
            dd.Run();
        }
    }

    class DisplayDecoder
    {
        const int DISPLAY_WIDTH = 50;
        const int DISPLAY_HEIGHT = 6;

        //const int DISPLAY_WIDTH = 7;
        //const int DISPLAY_HEIGHT = 3;

        bool[,] _tinyDisplay = new bool[DISPLAY_WIDTH,DISPLAY_HEIGHT];
        public void Run()
        {
            var input = File.ReadAllLines("input2.txt");
            foreach (var instruction in input)
            {
                ExecuteInstruction(instruction);
            }
            Console.WriteLine("Count: " + CountDisplay());
            Console.ReadLine();
        }

        private void ExecuteInstruction(string instruction)
        {
            if (instruction.StartsWith("rect "))
            {
                ProcessRectangle(instruction.Replace("rect ", ""));
            }
            else if (instruction.StartsWith("rotate column x="))
            {
                RotateColumn(instruction.Replace("rotate column x=", ""));
            }
            else if (instruction.StartsWith("rotate row y="))
            {
                RotateRow(instruction.Replace("rotate row y=", ""));
            }
            else
            {
                throw new ArgumentException("Invalid instruction: " + instruction);
            }

            PrintDisplay();
        }

        private void RotateRow(string args)
        {
            int row, shift;
            ParseRotateArgs(args, out row, out shift);

            var _tempArray = new bool[DISPLAY_WIDTH];

            for (var i = 0; i < DISPLAY_WIDTH; i++)
            {
                _tempArray[i] = _tinyDisplay[i, row];
                _tinyDisplay[i, row] = false;
            }

            var x = shift;
            for (var i = 0; i < DISPLAY_WIDTH; i++)
            {
                if (x >= DISPLAY_WIDTH)
                {
                    x = 0;
                }
                _tinyDisplay[x, row] = _tempArray[i];
                x++;
            }
        }

        private void RotateColumn(string args)
        {
            int column, shift;
            ParseRotateArgs(args, out column, out shift);
            
            var _tempArray = new bool[DISPLAY_HEIGHT];

            for (var i = 0; i < DISPLAY_HEIGHT; i++)
            { 
                _tempArray[i] = _tinyDisplay[column, i];
                _tinyDisplay[column, i] = false;
            }

            var y = shift;
            for(var i = 0; i < DISPLAY_HEIGHT; i++)
            {
                if (y >= DISPLAY_HEIGHT)
                {
                    y = 0;
                }
                _tinyDisplay[column, y] = _tempArray[i];
                y++;
            }
        }

        private void ProcessRectangle(string args)
        {
            var idx = args.IndexOf("x");
            var x = int.Parse(args.Substring(0, idx));
            var y = int.Parse(args.Substring(idx + 1, args.Length - idx - 1));

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    _tinyDisplay[i, j] = true;
                }
            }
        }

        private void ParseRotateArgs(string args, out int x, out int y)
        {
            var idx = args.IndexOf(" by ");

             x = int.Parse(args.Substring(0, idx));
             y = int.Parse(args.Substring(idx + 4, args.Length - idx - 4));
        }

        private void PrintDisplay()
        {
            for (var i = 0; i < DISPLAY_HEIGHT; i++)
            {
                for (var j = 0; j < DISPLAY_WIDTH; j++)
                {
                   Console.Write(_tinyDisplay[j,i] ? "X" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("--------------------------------------------------");
        }

        private int CountDisplay()
        {
            var count = 0;
            for (var i = 0; i < DISPLAY_HEIGHT; i++)
            {
                for (var j = 0; j < DISPLAY_WIDTH; j++)
                {
                    if (_tinyDisplay[j, i])
                        count++;
                }
            }

            return count;
        }
    }
}

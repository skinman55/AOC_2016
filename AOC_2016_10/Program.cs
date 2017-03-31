using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC_2016_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var bc = new BotController();
            bc.Run();
        }
    }

    class BotController
    {
        public List<Robot> Robots = new List<Robot>();

        //private Robot[] Robots = new Robot[10000];
        private int[] outputs = new int[10000];

        public void Run()
        {
            var input = File.ReadAllLines("input2.txt");
            var i = 0;
            foreach (var instruction in input)
            {
                ParseInstructions(instruction);
            }

            Console.WriteLine("bin 0: " + outputs[0]);
            Console.WriteLine("bin 1: " + outputs[1]);
            Console.WriteLine("bin 2: " + outputs[2]);

            Console.WriteLine("Part 2: " + outputs[0] * outputs[1] * outputs[2]);
            //Console.WriteLine("Count: " + CountDisplay());
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void ParseInstructions(string instruction)
        {
            //Console.WriteLine(instruction);

            if (instruction.StartsWith("value "))
            {
                ProcessValueInstruction(instruction);
            }
            else if (instruction.StartsWith("bot "))
            {
                ProcessGiveInstruction(instruction);
            }
            else
            {
                throw new ArgumentException("Invalid instruction: " + instruction);
            }

            ExecuteInstruction(instruction);
        }

        private void ExecuteInstruction(string instruction)
        {

            var didWork = true;

            while (didWork)
            {
                didWork = false;
                var botIDs = Robots.Select(x => x.Id).ToList();

                foreach (var botId in botIDs)
                {
                    var bot = Robots.FirstOrDefault(x => x.Id == botId);
                    if (bot.Chips.Count == 2)
                    {


                        if (bot.HighChip == 61 && bot.LowChip == 17)
                        {
                            Console.WriteLine("part 1: " + bot.Id);
                        }


                        //Console.WriteLine(instruction);
                        //Console.WriteLine(bot.Id);
                        //Console.WriteLine(bot.HighChip);


                        if (bot.LowTo.HasValue)
                        {
                            didWork = true;
                            //Console.WriteLine(
                            //    $"bot{bot.Id} gives low ({bot.LowChip}) to bot: {bot.LowTo} ");

                            var lowBot = FindOrCreateBot(bot.LowTo.Value);
                            lowBot.AcceptChip(bot.TakeLowChip());

                           
                        }

                        if (bot.HighTo.HasValue)
                        {
                            didWork = true;
                            //Console.WriteLine(
                            //    $"bot{bot.Id} gives gives high ({bot.HighChip}) to bot: {bot.HighTo}");
                            var highBot = FindOrCreateBot(bot.HighTo.Value);
                            highBot.AcceptChip(bot.TakeHighChip());

                        }



                        if (bot.LowToOutput.HasValue)
                        {
                            //didWork = true;
                            //Console.WriteLine($"Bot: {bot.Id} Gives LowChip: {bot.LowChip} To Output {bot.LowToOutput}");


                            outputs[bot.LowToOutput.Value] = bot.TakeLowChip();
                        }


                        if (bot.HighToOutput.HasValue )
                        {
                            //didWork = true;
                            //Console.WriteLine(
                            //    $"Bot: {bot.Id} Gives HighChip: {bot.HighChip} To Output {bot.HighToOutput}");


                            outputs[bot.HighToOutput.Value] = bot.TakeHighChip();
                        }

                        if (didWork)
                            break;
                    }
                }
            }
        }


        private void ProcessGiveInstruction(string instruction)
        {
            
            var botId = GetValueFromString(instruction, "bot ");
            var lowToBot = GetValueFromString(instruction, " gives low to bot ");
            var lowToOutput = GetValueFromString(instruction, " gives low to output ");
            var highToBot = GetValueFromString(instruction, " and high to bot ");
            var highToOutput = GetValueFromString(instruction, " and high to output ");

            var bot = FindOrCreateBot(botId.Value);
            //Console.Write("Id: " + bot.Id);

            if (lowToBot.HasValue)
            {
                bot.LowTo = lowToBot.Value;
                //Console.Write(" lowTo: " + bot.LowTo);
            }

            if (highToBot.HasValue)
            {
                bot.HighTo = highToBot.Value;
                //Console.Write(" highTo: " + bot.HighTo);
            }

            if (lowToOutput.HasValue)
            {
                bot.LowToOutput = lowToOutput.Value;
                //Console.Write(" lowToOutput: " + bot.LowToOutput);
                //outputs[botId.Value] = lowToOutput.Value;
            }

            if (highToOutput.HasValue)
            {
                bot.HighToOutput = highToOutput.Value;
                //Console.Write(" highToOutput: " + bot.HighToOutput);
                //outputs[botId.Value] = highToOutput.Value;
            }

            //Console.WriteLine();

        }

        private int? GetValueFromString(string input, string pattern)
        {
            var m = Regex.Match(input, pattern);
            if (!m.Success)
            {
                return null;
            }

            string value;

            // find distance to next space 
            var delimiter = input.IndexOf(" ", m.Index + pattern.Length);
            // or if at end of string, then use distance between end of pattern and end of the string
            if (delimiter < 0)
            {
                delimiter = input.Length - (m.Index + pattern.Length);
                value = input.Substring(m.Index + pattern.Length, delimiter);
            }
            else
            {
                value = input.Substring(m.Index + pattern.Length, delimiter - pattern.Length - m.Index);
            }

            return int.Parse(value);

        }
        private void ProcessValueInstruction(string instruction)
        {
            //example: value 5 goes to bot 2
            const string segment1 ="value ";
            const string segment2 = " goes to bot ";
            var idx = instruction.IndexOf(segment2);
            var value = int.Parse(instruction.Substring(segment1.Length, idx - segment1.Length));
            var botId = int.Parse(instruction.Substring(idx + segment2.Length, instruction.Length - idx - segment2.Length));

            var bot = FindOrCreateBot(botId);
            bot.AcceptChip(value);
            //Console.WriteLine("bot: " + bot.Id + " value: " + value);
        }

        private Robot FindOrCreateBot(int botId)
        {
            var bot = Robots.FirstOrDefault(r => r.Id == botId);
            if (bot == null)
            {
                bot = new Robot(botId);
                Robots.Add(bot);
            }
            return bot;
        }
    }

    public class Robot
    {
        public int Id;

        public Robot(int robotId)
        {
            Id = robotId;
        }

        public List<int> Chips = new List<int>();
        public int? HighTo;
        public int? LowTo;
        public int? HighToOutput;
        public int? LowToOutput;

        public int HighChip => Chips.Max();
        public int LowChip => Chips.Min();

        public void AcceptChip(int chipValue)
        {
            if (Chips.Count < 2)
            {
                Chips.Add(chipValue);
            }
            else
            {
                throw new Exception("Robot has too many chips!");
            }
        }

        public int TakeHighChip()
        {
            var value = Chips.Max();
            Chips.Remove(value);
            return value;
        }

        public int TakeLowChip()
        {
            var value = Chips.Min();
            Chips.Remove(value);
            return value;
        }
    }
}


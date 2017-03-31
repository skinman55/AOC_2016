using System;
using System.Security.Cryptography;
using System.Text;

namespace AOC_2016_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var dcg = new DoorCodeGetter();
            dcg.Run();
        }

        public class DoorCodeGetter
        {
            private readonly string input = "ffykfhsq";

            public void Run()
            {
                FindDigits_PartTwo();
            }

            public void FindDigits_PartOne()
            {
                var digitsFound = 0;
                var sb = new StringBuilder();
                var idx = 0;
                do
                {
                    var hash = CalculateMD5Hash($"{input}{idx}");

                    if (hash.StartsWith("00000"))
                    {
                            
                        digitsFound++;
                        Console.WriteLine("Digit Found: " + hash[5]);
                        sb.Append(hash[5]);
                    }

                    idx++;
                } while (digitsFound < 8);

                Console.WriteLine("Result: " + sb);
                Console.ReadLine();
            }

            public void FindDigits_PartTwo()
            {
                var digitsFound = 0;
                var code = new char[8];
                var idx = 0;
                do
                {
                    var hash = CalculateMD5Hash($"{input}{idx}");

                    if (hash.StartsWith("00000"))
                    {
                        int position;
                        if (int.TryParse(hash[5].ToString(), out position))
                        {
                            if (position < 8 && code[position] == '\0')
                            {
                                digitsFound++;
                                //Console.WriteLine("Digit Found: " + hash[6]);
                                code[position] = hash[6];
                                //Console.Clear();
                                //Console.WriteLine("Result: " + new string(code));
                            }
                        }
                    }

                    if (idx % 10000 == 0)
                    {
                        PrintCinematicEffects(code);
                    }

                    idx++;
                } while (digitsFound < 8);

                Console.Clear();
                Console.WriteLine("CODE FOUND: " + new string(code));
                Console.ReadLine();
            }

            private void PrintCinematicEffects(char[] code)
            {
                var tempCode = new char[8];
                Random rnd = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                for (var x = 0; x < code.Length; x++)
                {
                    if (code[x] == '\0')
                    {
                        tempCode[x] = chars[rnd.Next(0, 35)];
                    }
                    else
                    {
                        tempCode[x] = code[x];
                    }
                }
                Console.Clear();
                Console.WriteLine("WORKING: " + new string(tempCode));
            }

            private string CalculateMD5Hash(string input)
            {

                MD5 md5 = MD5.Create();

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace AOC_2016_9
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Decoder();
            c.Run();
        }
    }

    class Decoder
    {
        public void Run()
        {
           // var sb = new StringBuilder();
            long count = 0;
            var input = File.ReadAllLines("input2.txt");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var line in input)
            {
                count += DecompressLineV2(line);
            }
            sw.Stop();
            Console.WriteLine("Finished in " + sw.Elapsed.Minutes + " minutes");
            Console.WriteLine("Length: " + count);
            Console.ReadLine();
        }

        private string DecompressLineV1(string line)
        {
            
            var sb = new StringBuilder();
            var markerLength = 0;
            var markerCycles = 0;
            for (var idx = 0; idx < line.Length; idx++)
            {
                
                if (markerLength > 0)
                {
                    var temp = line.Substring(idx, markerLength);
                    for (var x = 0; x < markerCycles; x++)
                    {
                        sb.Append(temp);
                    }
                    idx += markerLength - 1;
                    markerLength = 0;
                    markerCycles = 0;
                    continue;
                    
                }
                var c = line[idx];
                if (c == '(')
                {
                    var endOfMarker = line.IndexOf(")", idx);
                    var marker = line.Substring(idx, endOfMarker - idx + 1);
                    ParseMarker(marker, out markerLength, out markerCycles);
                    idx = endOfMarker;
                    continue;
                }

                sb.Append(c);

            }
            //Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private long DecompressLineV2(string line)
        {


           // var sb = new StringBuilder();
            long len = 0;
            var markerLength = 0;
            var markerCycles = 0;
            for (var idx = 0; idx < line.Length; idx++)
            {

                if (markerLength > 0)
                {
                    var temp = line.Substring(idx, markerLength);
                    for (var x = 0; x < markerCycles; x++)
                    {
                       len += DecompressLineV2(temp);
                    }
                    idx += markerLength - 1;
                    markerLength = 0;
                    markerCycles = 0;
                    continue;

                }
                var c = line[idx];
                if (c == '(')
                {
                    var endOfMarker = line.IndexOf(")", idx);
                    var marker = line.Substring(idx, endOfMarker - idx + 1);
                    ParseMarker(marker, out markerLength, out markerCycles);
                    idx = endOfMarker;
                    continue;
                }

                len++;

            }

            // Console.WriteLine(sb.ToString());
            return len;
        }

        private void ParseMarker(string marker, out int markerLength, out int markerCycles)
        {
            var idx = marker.IndexOf("x");
          
            markerLength = int.Parse(marker.Substring(1, idx-1));
            markerCycles = int.Parse(marker.Substring(idx + 1, marker.Length - idx - 2));
        }
    }
}

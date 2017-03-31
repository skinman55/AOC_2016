using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var mc = new MessageDecoder();
            mc.Run();
        }
    }

    class MessageDecoder
    {
        public void Run()
        {
            var col1 = new List<char>();
            var col2 = new List<char>();
            var col3 = new List<char>();
            var col4 = new List<char>();
            var col5 = new List<char>();
            var col6 = new List<char>();
            var col7 = new List<char>();
            var col8 = new List<char>();

            var input = File.ReadAllLines("input.txt");

            foreach (var str in input)
            {
                col1.Add(str[0]);
                col2.Add(str[1]);
                col3.Add(str[2]);
                col4.Add(str[3]);
                col5.Add(str[4]);
                col6.Add(str[5]);
                col7.Add(str[6]);
                col8.Add(str[7]);
            }

            var sb = new StringBuilder();
            sb.Append(col1.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col2.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col3.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col4.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col5.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col6.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col7.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);
            sb.Append(col8.GroupBy(c => c).OrderBy(x => x.Count()).FirstOrDefault().Key);

            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}

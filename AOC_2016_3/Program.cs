using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var tc = new TriangleChecker();
            tc.Run();
        }
    }

    public class TriangleChecker
    {
        private readonly List<Triangle> triangles = new List<Triangle>();

        public void Run()
        {
            ParseInput();
            var invalidTriangles = triangles.Count(t => t.isValid());
            Console.WriteLine(invalidTriangles);
            Console.ReadKey();
        }

        private void ParseInput()
        {
            var input = File.ReadAllLines("input.txt");

            var rowNum = 0;

            var t1 = new List<int>();
            var t2 = new List<int>();
            var t3 = new List<int>();

            foreach (var line in input)
            {
                rowNum++;

                t1.Add(int.Parse(line.Substring(0, 4)));
                t2.Add(int.Parse(line.Substring(4, 4)));
                t3.Add(int.Parse(line.Substring(8)));

                if (rowNum % 3 == 0)
                {
                    triangles.Add(new Triangle(t1[0], t1[1], t1[2]));
                    triangles.Add(new Triangle(t2[0], t2[1], t2[2]));
                    triangles.Add(new Triangle(t3[0], t3[1], t3[2]));

                    t1.Clear();
                    t2.Clear();
                    t3.Clear();
                }
            }
        }
    }

    public class Triangle
    {
        private List<int> sides = new List<int>();

        public Triangle(int s1, int s2, int s3)
        {
            sides.Add(s1);
            sides.Add(s2);
            sides.Add(s3);
        }

        public bool isValid()
        {
            var sumOfAll = sides.Sum();
            var longestSide = sides.Max();
            return longestSide < (sumOfAll - longestSide);
        }

    }
}




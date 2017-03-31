using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_12
{
    public static class Any
    {
        private static readonly Random _random = new Random();

        public static char Register()
        {
            return (char) (_random.Next() % char.MaxValue);
        }

        public static int RegisterValue()
        {
            return _random.Next();
        }
    }
}

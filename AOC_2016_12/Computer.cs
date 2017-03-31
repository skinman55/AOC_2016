using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_12
{
    public class Computer
    {
        private readonly Dictionary<char, int> _registers;

        public Computer()
        {
             _registers = new Dictionary<char, int>();
        }
        public virtual int this[char i]
        {
            get
            {
                if (_registers.ContainsKey(i))
                {
                    return _registers[i];
                }
                return 0;
            }
            set { _registers[i] = value; }
        }
    }
}

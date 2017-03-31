using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_12
{
    class IncrementInstruction
    {
        private readonly char _register;

        public IncrementInstruction(char register)
        {
            this._register = register;
        }

        public void Execute(Computer computer)
        {
            computer[_register] += 1;
        }
    }
}

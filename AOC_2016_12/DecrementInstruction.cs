using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_12
{
    class DecrementInstruction
    {
        private readonly char _register;

        public DecrementInstruction(char register)
        {
            this._register = register;
        }

        public void Execute(Computer computer)
        {
            computer[_register] -= 1;
        }
    }
}

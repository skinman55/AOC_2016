using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace AOC_2016_12
{
    class MockComputerBuilder
    {
        private readonly Mock<Computer> _computer;

        public MockComputerBuilder()
        {
            _computer = new Mock<Computer> { CallBase = true };
        }

        public MockComputerBuilder WithDefaultRegisterValue(char register, int registerValue)
        {
            _computer.Object[register] = registerValue;
            return this;
        }

        public Computer Build()
        {
            return _computer.Object;
        }
    }
}

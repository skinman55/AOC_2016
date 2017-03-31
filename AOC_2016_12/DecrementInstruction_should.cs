using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace AOC_2016_12
{
    class DecrementInstruction_should
    {
        [Test]
        public void decrement_register()
        {
            var register = Any.Register();
            var initialRegisterValue = Any.RegisterValue();

            var mockComputer = new Mock<Computer>();

            var initial = Any.RegisterValue();
            var x = initial;

            mockComputer.Setup(c => c[It.IsAny<char>()]).Returns(x);

            var computer = new MockComputerBuilder().WithDefaultRegisterValue(register, initialRegisterValue).Build();
         
            var instruction = new DecrementInstruction(register);
            instruction.Execute(computer);

            Assert.AreEqual(initial, computer[register]);
        }

       
    }
}

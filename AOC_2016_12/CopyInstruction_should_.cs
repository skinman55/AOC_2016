using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AOC_2016_12
{
    class CopyInstruction_should_
    {
        [Test]
        public void copy_register()
        {
            var registerSource = Any.Register();
            var registerValueToCopy = Any.RegisterValue();

            var registerDestination = (char) (registerSource + 1);
            var initialValue = registerValueToCopy + 1;

            var computer = new MockComputerBuilder()
                .WithDefaultRegisterValue(registerSource, registerValueToCopy)
                .WithDefaultRegisterValue(registerDestination, initialValue)
                .Build();
            
        }

       
    }

    public class CopyInstruction
    {
        private readonly char registerSource;
        private readonly char destination;
        public CopyInstruction(char registerSource,char destination)
        {
            this.destination = destination;
            this.registerSource = registerSource;
        }
    }
}

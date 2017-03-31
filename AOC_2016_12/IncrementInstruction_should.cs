using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AOC_2016_12
{
    class IncrementInstruction_should
    {
        [Test]
        public void increment_register()
        {
            var computer = new Computer();
            var initialA = computer['a'];
            var incrementInstruction = new IncrementInstruction('a');
            incrementInstruction.Execute(computer);

            Assert.AreEqual(initialA + 1, computer['a']);
        }
    }
}

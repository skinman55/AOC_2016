using NUnit.Framework;

namespace AOC_2016_12
{
    // ReSharper disable once ArrangeTypeModifiers
    class Computer_should_
    {
        [TestCase('a')]
        [TestCase('b')]
        [TestCase('c')]
        [TestCase('d')]
        public void default_registers_to_zero(char register)
        {
            var computer = new Computer();

            Assert.AreEqual(0, computer[register]);
        }

        [Test]
        public void set_register_value()
        {
            var computer = new Computer();
            var reg = Any.Register();
            computer[reg] = 1;

            Assert.AreEqual(computer[reg], 1);
        }
    }
}

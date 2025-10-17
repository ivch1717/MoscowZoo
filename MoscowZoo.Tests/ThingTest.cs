using Xunit;
using MoscowZoo;

namespace MoscowZoo.Tests
{
    public class ThingTests
    {
        [Fact]
        public void Thing_Constructor_SetsProperties()
        {
            var thing = new TestThing(123);
            Assert.Equal(123, thing.InventorNumber);
        }

        [Fact]
        public void Thing_ToString_ReturnsCorrectFormat()
        {
            var thing = new TestThing(123);
            var result = thing.ToString();
            Assert.Equal("Инвентарный номер: 123", result);
        }

        private class TestThing : Thing
        {
            public TestThing(int inventorNumber) : base(inventorNumber) { }
        }
    }

    public class TableTests
    {
        [Fact]
        public void Table_Constructor_SetsProperties()
        {
            var table = new Table(123, 100, 50);
            Assert.Equal(123, table.InventorNumber);
            Assert.Equal(100, table.Height);
            Assert.Equal(50, table.Width);
        }

        [Fact]
        public void Table_ToString_StartsWithCorrectPrefix()
        {
            var table = new Table(123, 100, 50);
            var result = table.ToString();
            Assert.StartsWith("Стол:", result);
            Assert.Contains("Инвентарный номер: 123", result);
            Assert.Contains("Длина стола: 100", result);
            Assert.Contains("Ширина стола: 50", result);
        }

        [Fact]
        public void Table_ToString_ContainsAllProperties()
        {
            var table = new Table(456, 80, 120);
            var result = table.ToString();
            Assert.Contains("456", result);
            Assert.Contains("80", result);
            Assert.Contains("120", result);
        }
    }

    public class ComputerTests
    {
        [Fact]
        public void Computer_Constructor_SetsProperties()
        {
            var computer = new Computer(789, 512);
            Assert.Equal(789, computer.InventorNumber);
            Assert.Equal(512, computer.AmountAvailableMemory);
        }

        [Fact]
        public void Computer_ToString_StartsWithCorrectPrefix()
        {
            var computer = new Computer(789, 512);
            var result = computer.ToString();
            Assert.StartsWith("Компьютер:", result);
            Assert.Contains("Инвентарный номер: 789", result);
            Assert.Contains("Количество свободной памяти: 512", result);
        }

        [Fact]
        public void Computer_ToString_ContainsAllProperties()
        {
            var computer = new Computer(999, 1024);
            var result = computer.ToString();
            Assert.Contains("999", result);
            Assert.Contains("1024", result);
        }

        [Fact]
        public void Computer_DifferentMemorySizes_DisplayCorrectly()
        {
            var computer1 = new Computer(1, 256);
            var computer2 = new Computer(2, 1024);
            
            var result1 = computer1.ToString();
            var result2 = computer2.ToString();
            
            Assert.Contains("256", result1);
            Assert.Contains("1024", result2);
        }
    }

    public class ThingInheritanceTests
    {
        [Fact]
        public void Table_IsThing()
        {
            var table = new Table(123, 100, 50);
            Assert.IsAssignableFrom<Thing>(table);
            Assert.IsAssignableFrom<IInventory>(table);
        }

        [Fact]
        public void Computer_IsThing()
        {
            var computer = new Computer(456, 512);
            Assert.IsAssignableFrom<Thing>(computer);
            Assert.IsAssignableFrom<IInventory>(computer);
        }

        [Fact]
        public void Thing_IsIInventory()
        {
            var thing = new TestThing(789);
            Assert.IsAssignableFrom<IInventory>(thing);
        }

        private class TestThing : Thing
        {
            public TestThing(int inventorNumber) : base(inventorNumber) { }
        }
    }
}
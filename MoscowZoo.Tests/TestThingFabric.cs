using Xunit;
using MoscowZoo.fabrics;
using MoscowZoo;
using System;
using System.Collections.Generic;

namespace MoscowZoo.Tests
{
    public class ComputerFabricTests
    {
        [Fact]
        public void ComputerFabric_CreateThing_ValidParameters_CreatesComputer()
        {
            var fabric = new ComputerFabric();
            object[] parameters = ["123", "512"];
            var thing = fabric.CreateThing(parameters);
            
            Assert.IsType<Computer>(thing);
            var computer = (Computer)thing;
            Assert.Equal(123, computer.InventorNumber);
            Assert.Equal(512, computer.AmountAvailableMemory);
        }

        [Fact]
        public void ComputerFabric_CreateThing_InvalidInventorNumber_ThrowsFormatException()
        {
            var fabric = new ComputerFabric();
            object[] parameters = ["abc", "512"];
            
            Assert.Throws<FormatException>(() => fabric.CreateThing(parameters));
        }

        [Fact]
        public void ComputerFabric_CreateThing_InvalidMemory_ThrowsFormatException()
        {
            var fabric = new ComputerFabric();
            object[] parameters = ["123", "invalid"];
            
            Assert.Throws<FormatException>(() => fabric.CreateThing(parameters));
        }

        [Fact]
        public void ComputerFabric_CreateThing_NotEnoughParameters_ThrowsIndexOutOfRangeException()
        {
            var fabric = new ComputerFabric();
            object[] parameters = ["123"];
            
            Assert.Throws<IndexOutOfRangeException>(() => fabric.CreateThing(parameters));
        }
    }

    public class TableFabricTests
    {
        [Fact]
        public void TableFabric_CreateThing_ValidParameters_CreatesTable()
        {
            var fabric = new TableFabric();
            object[] parameters = ["456", "100", "50"];
            var thing = fabric.CreateThing(parameters);
            
            Assert.IsType<Table>(thing);
            var table = (Table)thing;
            Assert.Equal(456, table.InventorNumber);
            Assert.Equal(100, table.Height);
            Assert.Equal(50, table.Width);
        }

        [Fact]
        public void TableFabric_CreateThing_InvalidHeight_ThrowsFormatException()
        {
            var fabric = new TableFabric();
            object[] parameters = ["456", "abc", "50"];
            
            Assert.Throws<FormatException>(() => fabric.CreateThing(parameters));
        }

        [Fact]
        public void TableFabric_CreateThing_InvalidWidth_ThrowsFormatException()
        {
            var fabric = new TableFabric();
            object[] parameters = ["456", "100", "invalid"];
            
            Assert.Throws<FormatException>(() => fabric.CreateThing(parameters));
        }

        [Fact]
        public void TableFabric_CreateThing_NotEnoughParameters_ThrowsIndexOutOfRangeException()
        {
            var fabric = new TableFabric();
            object[] parameters = ["456", "100"];
            
            Assert.Throws<IndexOutOfRangeException>(() => fabric.CreateThing(parameters));
        }
    }

    public class FactoryThingResolverTests
    {
        [Fact]
        public void FactoryThingResolver_GetFabric_ComputerType_ReturnsComputerFabric()
        {
            var resolver = new FactoryThingResolver();
            var fabric = resolver.GetFabric("компьютер");
            
            Assert.IsType<ComputerFabric>(fabric);
        }

        [Fact]
        public void FactoryThingResolver_GetFabric_TableType_ReturnsTableFabric()
        {
            var resolver = new FactoryThingResolver();
            var fabric = resolver.GetFabric("стол");
            
            Assert.IsType<TableFabric>(fabric);
        }

        [Fact]
        public void FactoryThingResolver_GetFabric_CaseInsensitive_ReturnsFabric()
        {
            var resolver = new FactoryThingResolver();
            var fabric1 = resolver.GetFabric("Компьютер");
            var fabric2 = resolver.GetFabric("компьютер");
            var fabric3 = resolver.GetFabric("КОМПЬЮТЕР");
            
            Assert.IsType<ComputerFabric>(fabric1);
            Assert.IsType<ComputerFabric>(fabric2);
            Assert.IsType<ComputerFabric>(fabric3);
        }

        [Fact]
        public void FactoryThingResolver_GetFabric_InvalidType_ThrowsException()
        {
            var resolver = new FactoryThingResolver();
            
            Assert.Throws<KeyNotFoundException>(() => resolver.GetFabric("неизвестнаявещь"));
        }

        [Fact]
        public void FactoryThingResolver_GetFabric_EmptyString_ThrowsException()
        {
            var resolver = new FactoryThingResolver();
            
            Assert.Throws<KeyNotFoundException>(() => resolver.GetFabric(""));
        }

        [Fact]
        public void FactoryThingResolver_GetFabric_NullString_ThrowsException()
        {
            var resolver = new FactoryThingResolver();
            
            Assert.Throws<NullReferenceException>(() => resolver.GetFabric(null));
        }

        [Fact]
        public void FactoryThingResolver_AllFabrics_CreateCorrectObjects()
        {
            var resolver = new FactoryThingResolver();
            
            var computerFabric = resolver.GetFabric("компьютер");
            var computer = computerFabric.CreateThing("789", "256");
            Assert.IsType<Computer>(computer);
            
            var tableFabric = resolver.GetFabric("стол");
            var table = tableFabric.CreateThing("999", "80", "120");
            Assert.IsType<Table>(table);
        }
    }
}
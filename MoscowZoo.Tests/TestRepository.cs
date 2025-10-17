using Xunit;
using MoscowZoo.repositories;
using MoscowZoo;
using System.Collections.Generic;

namespace MoscowZoo.Tests
{
    public class AnimalRepositoryTests
    {
        private readonly AnimalRepository _repository = new AnimalRepository();

        [Fact]
        public void AddAnimal_ValidAnimal_AddsToCollection()
        {
            var animal = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            
            _repository.AddAnimal(animal);
            
            var animals = _repository.GetAnimals();
            Assert.Single(animals);
            Assert.Equal(animal, animals[0]);
        }

        [Fact]
        public void RemoveAnimal_ValidId_RemovesAnimal()
        {
            var animal1 = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            var animal2 = new Wolf(8, Gender.мужской, 4, "Akela", 300);
            
            _repository.AddAnimal(animal1);
            _repository.AddAnimal(animal2);
            
            _repository.RemoveAnimal(0);
            
            var animals = _repository.GetAnimals();
            Assert.Single(animals);
            Assert.Equal(animal2, animals[0]);
        }

        [Fact]
        public void RemoveAnimal_InvalidId_ThrowsException()
        {
            var animal = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            _repository.AddAnimal(animal);
            
            Assert.Throws<ArgumentException>(() => _repository.RemoveAnimal(-1));
            Assert.Throws<ArgumentException>(() => _repository.RemoveAnimal(1));
        }

        [Fact]
        public void GetAnimals_EmptyRepository_ReturnsEmptyList()
        {
            var animals = _repository.GetAnimals();
            
            Assert.Empty(animals);
        }

        [Fact]
        public void GetAnimals_WithAnimals_ReturnsAllAnimals()
        {
            var animal1 = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            var animal2 = new Wolf(8, Gender.мужской, 4, "Akela", 300);
            
            _repository.AddAnimal(animal1);
            _repository.AddAnimal(animal2);
            
            var animals = _repository.GetAnimals();
            
            Assert.Equal(2, animals.Count);
            Assert.Contains(animal1, animals);
            Assert.Contains(animal2, animals);
        }

        [Fact]
        public void IsEmpty_EmptyRepository_ReturnsTrue()
        {
            Assert.True(_repository.IsEmpty());
        }

        [Fact]
        public void IsEmpty_WithAnimals_ReturnsFalse()
        {
            var animal = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            _repository.AddAnimal(animal);
            
            Assert.False(_repository.IsEmpty());
        }
    }

    public class ThingRepositoryTests
    {
        private readonly ThingRepository _repository = new ThingRepository();

        [Fact]
        public void AddThing_ValidThing_AddsToCollection()
        {
            var table = new Table(123, 100, 50);
            
            _repository.AddThing(table);
            
            var inventories = _repository.GetInventory();
            Assert.Single(inventories);
            Assert.Equal(table, inventories[0]);
        }

        [Fact]
        public void RemoveInventory_ValidId_RemovesThing()
        {
            var table = new Table(123, 100, 50);
            var computer = new Computer(456, 512);
            
            _repository.AddThing(table);
            _repository.AddThing(computer);
            
            _repository.RemoveInventory(0);
            
            var inventories = _repository.GetInventory();
            Assert.Single(inventories);
            Assert.Equal(computer, inventories[0]);
        }

        [Fact]
        public void RemoveInventory_InvalidId_ThrowsException()
        {
            var table = new Table(123, 100, 50);
            _repository.AddThing(table);
            
            Assert.Throws<ArgumentException>(() => _repository.RemoveInventory(-1));
            Assert.Throws<ArgumentException>(() => _repository.RemoveInventory(1));
        }

        [Fact]
        public void GetInventory_EmptyRepository_ReturnsEmptyList()
        {
            var inventories = _repository.GetInventory();
            
            Assert.Empty(inventories);
        }

        [Fact]
        public void GetInventory_WithThings_ReturnsAllThings()
        {
            var table = new Table(123, 100, 50);
            var computer = new Computer(456, 512);
            
            _repository.AddThing(table);
            _repository.AddThing(computer);
            
            var inventories = _repository.GetInventory();
            
            Assert.Equal(2, inventories.Count);
            Assert.Contains(table, inventories);
            Assert.Contains(computer, inventories);
        }

        [Fact]
        public void IsEmpty_EmptyRepository_ReturnsTrue()
        {
            Assert.True(_repository.IsEmpty());
        }

        [Fact]
        public void IsEmpty_WithThings_ReturnsFalse()
        {
            var table = new Table(123, 100, 50);
            _repository.AddThing(table);
            
            Assert.False(_repository.IsEmpty());
        }

        [Fact]
        public void AddThing_MixedTypes_WorksCorrectly()
        {
            var table = new Table(1, 80, 120);
            var computer = new Computer(2, 1024);
            
            _repository.AddThing(table);
            _repository.AddThing(computer);
            
            var inventories = _repository.GetInventory();
            Assert.Equal(2, inventories.Count);
            Assert.IsType<Table>(inventories[0]);
            Assert.IsType<Computer>(inventories[1]);
        }
    }
}
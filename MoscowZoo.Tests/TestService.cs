using Xunit;
using MoscowZoo.service;
using MoscowZoo.repositories;
using MoscowZoo.fabrics;
using MoscowZoo.vet_clinic;
using MoscowZoo;
using Moq;
using System.Collections.Generic;

namespace MoscowZoo.Tests
{
    public class AnimalServiceTests
    {
        private readonly Mock<IAnimalRepository> _mockRepository;
        private readonly Mock<IFactoryAnimalResolver> _mockResolver;
        private readonly Mock<IVeterinaryClinic> _mockClinic;
        private readonly AnimalService _animalService;

        public AnimalServiceTests()
        {
            _mockRepository = new Mock<IAnimalRepository>();
            _mockResolver = new Mock<IFactoryAnimalResolver>();
            _mockClinic = new Mock<IVeterinaryClinic>();
            _animalService = new AnimalService(_mockRepository.Object, _mockResolver.Object, _mockClinic.Object);
        }

        [Fact]
        public void Add_HealthyAnimal_AddsToRepository()
        {
            var mockFabric = new Mock<IAnimalFabric>();
            var animal = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            
            _mockResolver.Setup(r => r.GetFabric("кролик")).Returns(mockFabric.Object);
            mockFabric.Setup(f => f.CreateAnimal(It.IsAny<object[]>())).Returns(animal);
            _mockClinic.Setup(c => c.IsHealthy(animal)).Returns(true);
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);

            var result = _animalService.Add("кролик", "2", "женский", "1", "Bunny", "5");

            Assert.Equal("Животное успешно добавлено", result);
            _mockRepository.Verify(r => r.AddAnimal(animal), Times.Once);
        }

        [Fact]
        public void Add_SickAnimal_ReturnsErrorMessage()
        {
            var mockFabric = new Mock<IAnimalFabric>();
            var animal = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            
            _mockResolver.Setup(r => r.GetFabric("кролик")).Returns(mockFabric.Object);
            mockFabric.Setup(f => f.CreateAnimal(It.IsAny<object[]>())).Returns(animal);
            _mockClinic.Setup(c => c.IsHealthy(animal)).Returns(false);

            var result = _animalService.Add("кролик", "2", "женский", "1", "Bunny", "5");

            Assert.Equal("К сожалению животное больное, мы пока не можем добавить его в зоопарк", result);
            _mockRepository.Verify(r => r.AddAnimal(It.IsAny<IAlive>()), Times.Never);
        }

        [Fact]
        public void Remove_ValidId_RemovesAnimal()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);

            var result = _animalService.Remove(0);

            Assert.Equal("Животное выпущено на волю", result);
            _mockRepository.Verify(r => r.RemoveAnimal(0), Times.Once);
        }

        [Fact]
        public void Remove_EmptyRepository_ReturnsErrorMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _animalService.Remove(0);

            Assert.Equal("В нашем ззопарке нет животных", result);
            _mockRepository.Verify(r => r.RemoveAnimal(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Report_EmptyRepository_ReturnsEmptyMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _animalService.Report();

            Assert.Equal("В нашем зоопарке пока ещё нет животных(\nПриходите завтра!", result);
        }

        [Fact]
        public void Report_WithAnimals_ReturnsFormattedList()
        {
            var animals = new List<IAlive>
            {
                new Rabbit(2, Gender.женский, 1, "Bunny", 5),
                new Wolf(8, Gender.мужской, 4, "Akela", 300)
            };
            
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);
            _mockRepository.Setup(r => r.GetAnimals()).Returns(animals);

            var result = _animalService.Report();

            Assert.Contains("Спиок животных:", result);
            Assert.Contains("Bunny", result);
            Assert.Contains("Akela", result);
            Assert.Contains("0:", result);
            Assert.Contains("1:", result);
        }

        [Fact]
        public void ReportFood_EmptyRepository_ReturnsEmptyMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _animalService.ReportFood();

            Assert.Equal("В нашем зоопарке пока ещё нет животных(\nПриходите завтра!", result);
        }

        [Fact]
        public void ReportFood_WithAnimals_ReturnsTotalFood()
        {
            var animals = new List<IAlive>
            {
                new Rabbit(2, Gender.женский, 1, "Bunny", 5),
                new Wolf(8, Gender.мужской, 4, "Akela", 300)
            };
            
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);
            _mockRepository.Setup(r => r.GetAnimals()).Returns(animals);

            var result = _animalService.ReportFood();

            Assert.Equal("Количество килограм корма нужное животынм в день: 10", result);
        }

        [Fact]
        public void ContactZoo_EmptyRepository_ReturnsEmptyMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _animalService.ContactZoo();

            Assert.Equal("В нашем зоопарке пока ещё нет животных(\nПриходите завтра!", result);
        }

        [Fact]
        public void ContactZoo_NoContactAnimals_ReturnsNoContactMessage()
        {
            var animals = new List<IAlive>
            {
                new Rabbit(2, Gender.женский, 1, "Bunny", 3), // LevelKind = 3 < 5
                new Wolf(8, Gender.мужской, 4, "Akela", 300) // Not Herbo
            };
            
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);
            _mockRepository.Setup(r => r.GetAnimals()).Returns(animals);

            var result = _animalService.ContactZoo();

            Assert.Equal("В нашем ззопарке пока нет контактных животных", result);
        }

        [Fact]
        public void ContactZoo_WithContactAnimals_ReturnsContactList()
        {
            var animals = new List<IAlive>
            {
                new Rabbit(2, Gender.женский, 1, "Bunny", 7), // LevelKind = 7 > 5
                new Rabbit(3, Gender.мужской, 2, "Roger", 6), // LevelKind = 6 > 5
                new Wolf(8, Gender.мужской, 4, "Akela", 300) // Not Herbo
            };
            
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);
            _mockRepository.Setup(r => r.GetAnimals()).Returns(animals);

            var result = _animalService.ContactZoo();

            Assert.Contains("Контактный ззопарк:", result);
            Assert.Contains("Bunny", result);
            Assert.Contains("Roger", result);
            Assert.DoesNotContain("Akela", result);
        }
    }

    public class ThingServiceTests
    {
        private readonly Mock<IThingRepository> _mockRepository;
        private readonly Mock<IFactoryThingResolver> _mockResolver;
        private readonly ThingService _thingService;

        public ThingServiceTests()
        {
            _mockRepository = new Mock<IThingRepository>();
            _mockResolver = new Mock<IFactoryThingResolver>();
            _thingService = new ThingService(_mockRepository.Object, _mockResolver.Object);
        }

        [Fact]
        public void Add_ValidThing_AddsToRepository()
        {
            var mockFabric = new Mock<IThingFabric>();
            var table = new Table(123, 100, 50);
            
            _mockResolver.Setup(r => r.GetFabric("стол")).Returns(mockFabric.Object);
            mockFabric.Setup(f => f.CreateThing(It.IsAny<object[]>())).Returns(table);
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);

            var result = _thingService.Add("стол", "123", "100", "50");

            Assert.Equal("Инвернтарь успешно добавлен", result);
            _mockRepository.Verify(r => r.AddThing(table), Times.Once);
        }

        [Fact]
        public void Remove_ValidId_RemovesThing()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);

            var result = _thingService.Remove(0);

            Assert.Equal("инвентарь успешно удалён", result);
            _mockRepository.Verify(r => r.RemoveInventory(0), Times.Once);
        }

        [Fact]
        public void Remove_EmptyRepository_ReturnsErrorMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _thingService.Remove(0);

            Assert.Equal("Список вещей пустой", result);
            _mockRepository.Verify(r => r.RemoveInventory(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Report_EmptyRepository_ReturnsEmptyMessage()
        {
            _mockRepository.Setup(r => r.IsEmpty()).Returns(true);

            var result = _thingService.Report();

            Assert.Equal("Список вещей пустой", result);
        }

        [Fact]
        public void Report_WithThings_ReturnsFormattedList()
        {
            var things = new List<IInventory>
            {
                new Table(123, 100, 50),
                new Computer(456, 512)
            };
            
            _mockRepository.Setup(r => r.IsEmpty()).Returns(false);
            _mockRepository.Setup(r => r.GetInventory()).Returns(things);

            var result = _thingService.Report();

            Assert.Contains("Список вещей:", result);
            Assert.Contains("123", result);
            Assert.Contains("456", result);
            Assert.Contains("0:", result);
            Assert.Contains("1:", result);
        }
    }
}
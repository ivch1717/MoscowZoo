using System;
using System.Collections.Generic;
using MoscowZoo;
using MoscowZoo.fabrics;
using Xunit;

namespace MoscowZoo.Tests
{
    public class AnimalTests
    {
        [Fact]
        public void Animal_Constructor_SetsProperties()
        {
            var animal = new TestAnimal(5, Gender.мужской, 3, "TestName");
            Xunit.Assert.Equal(5, animal.Food);
            Xunit.Assert.Equal(Gender.мужской, animal.Gender);
            Xunit.Assert.Equal(3, animal.Age);
            Xunit.Assert.Equal("TestName", animal.Name);
        }

        [Fact]
        public void Animal_ToString_ReturnsCorrectFormat()
        {
            var animal = new TestAnimal(5, Gender.мужской, 3, "TestName");
            var result = animal.ToString();
            Xunit.Assert.Contains("Количество килограммов еды/сутки: 5", result);
            Xunit.Assert.Contains("Имя: TestName", result);
        }

        private class TestAnimal : Animal
        {
            public TestAnimal(int food, Gender gender, int age, string name) 
                : base(food, gender, age, name) { }
        }
    }

    public class MonkeyTests
    {
        [Fact]
        public void Monkey_Constructor_SetsProperties()
        {
            var monkey = new Monkey(3, Gender.мужской, 5, "George", 4, 8);
            Xunit.Assert.Equal(8, monkey.LevelIntelligence);
            Xunit.Assert.Equal(4, monkey.LevelKind);
        }

        [Fact]
        public void Monkey_ToString_StartsWithCorrectPrefix()
        {
            var monkey = new Monkey(3, Gender.мужской, 5, "George", 4, 8);
            var result = monkey.ToString();
            Xunit.Assert.StartsWith("Обезьяна:", result);
        }
    }

    public class FabricTests
    {
        [Fact]
        public void MonkeyFabric_CreateAnimal_ValidParameters_CreatesMonkey()
        {
            var fabric = new MonkeyFabric();
            object[] parameters = ["5", "мужской", "3", "George", "4", "7"];
            var animal = fabric.CreateAnimal(parameters);
            
            Xunit.Assert.IsType<Monkey>(animal);
            var monkey = (Monkey)animal;
            Xunit.Assert.Equal(5, monkey.Food);
            Xunit.Assert.Equal(Gender.мужской, monkey.Gender);
            Xunit.Assert.Equal(3, monkey.Age);
            Xunit.Assert.Equal("George", monkey.Name);
            Xunit.Assert.Equal(4, monkey.LevelKind);
            Xunit.Assert.Equal(7, monkey.LevelIntelligence);
        }

        [Fact]
        public void MonkeyFabric_CreateAnimal_InvalidFood_ThrowsFormatException()
        {
            var fabric = new MonkeyFabric();
            object[] parameters = ["abc", "мужской", "3", "George", "4", "7"];
            
            Xunit.Assert.Throws<FormatException>(() => fabric.CreateAnimal(parameters));
        }

        [Fact]
        public void RabbitFabric_CreateAnimal_ValidParameters_CreatesRabbit()
        {
            var fabric = new RabbitFabric();
            object[] parameters = ["2", "женский", "1", "Bunny", "5"];
            var animal = fabric.CreateAnimal(parameters);
            
            Xunit.Assert.IsType<Rabbit>(animal);
            var rabbit = (Rabbit)animal;
            Xunit.Assert.Equal(2, rabbit.Food);
            Xunit.Assert.Equal(Gender.женский, rabbit.Gender);
        }

        [Fact]
        public void FactoryAnimalResolver_GetFabric_ValidType_ReturnsFabric()
        {
            var resolver = new FactoryAnimalResolver();
            var fabric = resolver.GetFabric("обезьяна");
            
            Xunit.Assert.IsType<MonkeyFabric>(fabric);
        }

        [Fact]
        public void FactoryAnimalResolver_GetFabric_InvalidType_ThrowsException()
        {
            var resolver = new FactoryAnimalResolver();
            
            Xunit.Assert.Throws<KeyNotFoundException>(() => resolver.GetFabric("несуществующий"));
        }
    }

    public class GenderTests
    {
        [Fact]
        public void EnumParse_ValidString_ParsesCorrectly()
        {
            var result = Enum.Parse<Gender>("мужской", true);
            Xunit.Assert.Equal(Gender.мужской, result);
        }

        [Fact]
        public void EnumParse_InvalidString_ThrowsArgumentException()
        {
            Xunit.Assert.Throws<ArgumentException>(() => Enum.Parse<Gender>("Invalid", true));
        }
    }
    
    public class RabbitTests
    {
        [Fact]
        public void Rabbit_Constructor_SetsProperties()
        {
            var rabbit = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            Xunit.Assert.Equal(2, rabbit.Food);
            Xunit.Assert.Equal(Gender.женский, rabbit.Gender);
            Xunit.Assert.Equal(1, rabbit.Age);
            Xunit.Assert.Equal("Bunny", rabbit.Name);
            Xunit.Assert.Equal(5, rabbit.LevelKind);
        }

        [Fact]
        public void Rabbit_ToString_StartsWithCorrectPrefix()
        {
            var rabbit = new Rabbit(2, Gender.женский, 1, "Bunny", 5);
            var result = rabbit.ToString();
            Xunit.Assert.StartsWith("Кролик:", result);
            Xunit.Assert.Contains("Уровень доброты: 5", result);
        }
    }

    public class TigerTests
    {
        [Fact]
        public void Tiger_Constructor_SetsProperties()
        {
            var tiger = new Tiger(10, Gender.мужской, 5, "Sherkhan", 500, 120);
            Xunit.Assert.Equal(10, tiger.Food);
            Xunit.Assert.Equal(Gender.мужской, tiger.Gender);
            Xunit.Assert.Equal(5, tiger.Age);
            Xunit.Assert.Equal("Sherkhan", tiger.Name);
            Xunit.Assert.Equal(500, tiger.BiteForce);
            Xunit.Assert.Equal(120, tiger.NumberPolo);
        }

        [Fact]
        public void Tiger_ToString_StartsWithCorrectPrefix()
        {
            var tiger = new Tiger(10, Gender.мужской, 5, "Sherkhan", 500, 120);
            var result = tiger.ToString();
            Xunit.Assert.StartsWith("Тигр:", result);
            Xunit.Assert.Contains("Сила укуса: 500", result);
            Xunit.Assert.Contains("Количество полосок: 120", result);
        }
    }

    public class WolfTests
    {
        [Fact]
        public void Wolf_Constructor_SetsProperties()
        {
            var wolf = new Wolf(8, Gender.мужской, 4, "Akela", 300);
            Xunit.Assert.Equal(8, wolf.Food);
            Xunit.Assert.Equal(Gender.мужской, wolf.Gender);
            Xunit.Assert.Equal(4, wolf.Age);
            Xunit.Assert.Equal("Akela", wolf.Name);
            Xunit.Assert.Equal(300, wolf.BiteForce);
        }

        [Fact]
        public void Wolf_ToString_StartsWithCorrectPrefix()
        {
            var wolf = new Wolf(8, Gender.мужской, 4, "Akela", 300);
            var result = wolf.ToString();
            Xunit.Assert.StartsWith("Волк:", result);
            Xunit.Assert.Contains("Сила укуса: 300", result);
        }
    }

    public class PredatorTests
    {
        [Fact]
        public void Predator_Constructor_SetsProperties()
        {
            var predator = new TestPredator(10, Gender.женский, 3, "Hunter", 400);
            Xunit.Assert.Equal(10, predator.Food);
            Xunit.Assert.Equal(Gender.женский, predator.Gender);
            Xunit.Assert.Equal(3, predator.Age);
            Xunit.Assert.Equal("Hunter", predator.Name);
            Xunit.Assert.Equal(400, predator.BiteForce);
        }

        [Fact]
        public void Predator_ToString_ContainsBiteForce()
        {
            var predator = new TestPredator(10, Gender.женский, 3, "Hunter", 400);
            var result = predator.ToString();
            Xunit.Assert.Contains("Сила укуса: 400", result);
        }

        private class TestPredator : Predator
        {
            public TestPredator(int food, Gender gender, int age, string name, int biteForce) 
                : base(food, gender, age, name, biteForce) { }
        }
    }

    public class HerboTests
    {
        [Fact]
        public void Herbo_Constructor_SetsProperties()
        {
            var herbo = new TestHerbo(3, Gender.мужской, 2, "GrassEater", 4);
            Xunit.Assert.Equal(3, herbo.Food);
            Xunit.Assert.Equal(Gender.мужской, herbo.Gender);
            Xunit.Assert.Equal(2, herbo.Age);
            Xunit.Assert.Equal("GrassEater", herbo.Name);
            Xunit.Assert.Equal(4, herbo.LevelKind);
        }

        [Fact]
        public void Herbo_ToString_ContainsLevelKind()
        {
            var herbo = new TestHerbo(3, Gender.мужской, 2, "GrassEater", 4);
            var result = herbo.ToString();
            Xunit.Assert.Contains("Уровень доброты: 4", result);
        }

        private class TestHerbo : Herbo
        {
            public TestHerbo(int food, Gender gender, int age, string name, int levelKind) 
                : base(food, gender, age, name, levelKind) { }
        }
    }
    
}
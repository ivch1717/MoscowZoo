using MoscowZoo;
using MoscowZoo.fabrics;
using Xunit;

public class FabricTest
{
    public class FabricAdditionalTests
    {
        [Fact]
        public void TigerFabric_CreateAnimal_ValidParameters_CreatesTiger()
        {
            var fabric = new TigerFabric();
            object[] parameters = ["10", "женский", "4", "Tigress", "450", "100"];
            var animal = fabric.CreateAnimal(parameters);

            Xunit.Assert.IsType<Tiger>(animal);
            var tiger = (Tiger)animal;
            Xunit.Assert.Equal(10, tiger.Food);
            Xunit.Assert.Equal(Gender.женский, tiger.Gender);
            Xunit.Assert.Equal(4, tiger.Age);
            Xunit.Assert.Equal("Tigress", tiger.Name);
            Xunit.Assert.Equal(450, tiger.BiteForce);
            Xunit.Assert.Equal(100, tiger.NumberPolo);
        }

        [Fact]
        public void TigerFabric_CreateAnimal_InvalidBiteForce_ThrowsFormatException()
        {
            var fabric = new TigerFabric();
            object[] parameters = ["10", "женский", "4", "Tigress", "invalid", "100"];

            Xunit.Assert.Throws<FormatException>(() => fabric.CreateAnimal(parameters));
        }

        [Fact]
        public void WolfFabric_CreateAnimal_ValidParameters_CreatesWolf()
        {
            var fabric = new WolfFabric();
            object[] parameters = ["7", "мужской", "3", "Grey", "350"];
            var animal = fabric.CreateAnimal(parameters);

            Xunit.Assert.IsType<Wolf>(animal);
            var wolf = (Wolf)animal;
            Xunit.Assert.Equal(7, wolf.Food);
            Xunit.Assert.Equal(Gender.мужской, wolf.Gender);
            Xunit.Assert.Equal(3, wolf.Age);
            Xunit.Assert.Equal("Grey", wolf.Name);
            Xunit.Assert.Equal(350, wolf.BiteForce);
        }

        [Fact]
        public void WolfFabric_CreateAnimal_InvalidGender_ThrowsArgumentException()
        {
            var fabric = new WolfFabric();
            object[] parameters = ["7", "invalid", "3", "Grey", "350"];

            Xunit.Assert.Throws<ArgumentException>(() => fabric.CreateAnimal(parameters));
        }

        [Fact]
        public void RabbitFabric_CreateAnimal_InvalidAge_ThrowsFormatException()
        {
            var fabric = new RabbitFabric();
            object[] parameters = ["2", "женский", "invalid", "Bunny", "5"];

            Xunit.Assert.Throws<FormatException>(() => fabric.CreateAnimal(parameters));
        }

        [Fact]
        public void FactoryAnimalResolver_GetFabric_CaseInsensitive_ReturnsFabric()
        {
            var resolver = new FactoryAnimalResolver();
            var fabric1 = resolver.GetFabric("Обезьяна");
            var fabric2 = resolver.GetFabric("обезьяна");
            var fabric3 = resolver.GetFabric("ОБЕЗЬЯНА");

            Xunit.Assert.IsType<MonkeyFabric>(fabric1);
            Xunit.Assert.IsType<MonkeyFabric>(fabric2);
            Xunit.Assert.IsType<MonkeyFabric>(fabric3);
        }

        [Fact]
        public void FactoryAnimalResolver_GetFabric_EmptyString_ThrowsException()
        {
            var resolver = new FactoryAnimalResolver();

            Xunit.Assert.Throws<KeyNotFoundException>(() => resolver.GetFabric(""));
        }

        [Fact]
        public void FactoryAnimalResolver_GetFabric_NullString_ThrowsException()
        {
            var resolver = new FactoryAnimalResolver();

            Xunit.Assert.Throws<NullReferenceException>(() => resolver.GetFabric(null));
        }
    }
}
using Xunit;
using MoscowZoo.reading_fields;
using System.Collections.Generic;

namespace MoscowZoo.Tests
{
    public class AnimalFieldValidatorTests
    {
        private readonly AnimalFieldValidator _validator = new AnimalFieldValidator();

        [Fact]
        public void ValidateAnimalFields_ValidData_NoException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Food", "5"},
                {"Gender", "Мужской"},
                {"Age", "3"},
                {"Name", "ТестовоеИмя"},
                {"LevelKind", "5"},
                {"LevelIntelligence", "50"},
                {"BiteForce", "100"},
                {"NumberPolo", "10"}
            };

            var exception = Record.Exception(() => _validator.ValidateAnimalFields(inputFields));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateAnimalFields_InvalidFood_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Food", "-5"},
                {"Gender", "Мужской"},
                {"Age", "3"},
                {"Name", "ТестовоеИмя"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }

        [Fact]
        public void ValidateAnimalFields_InvalidGender_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Food", "5"},
                {"Gender", "Неизвестный"},
                {"Age", "3"},
                {"Name", "ТестовоеИмя"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }

        [Fact]
        public void ValidateAnimalFields_InvalidAge_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Food", "5"},
                {"Gender", "Мужской"},
                {"Age", "0"},
                {"Name", "ТестовоеИмя"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }

        [Fact]
        public void ValidateAnimalFields_EmptyName_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Food", "5"},
                {"Gender", "Мужской"},
                {"Age", "3"},
                {"Name", ""}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }

        [Fact]
        public void ValidateAnimalFields_InvalidLevelKind_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"LevelKind", "15"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }

        [Fact]
        public void ValidateAnimalFields_UnknownField_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"UnknownField", "value"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateAnimalFields(inputFields));
        }
    }

    public class InformAnimalFieldTests
    {
        private readonly InformAnimalField _informAnimalField = new InformAnimalField();

        [Fact]
        public void ReadField_ValidAnimalType_ReturnsCorrectFields()
        {
            var result = _informAnimalField.ReadField("обезьяна");

            Assert.Equal(6, result.Count);
            Assert.Contains("Food", result.Keys);
            Assert.Contains("Gender", result.Keys);
            Assert.Contains("Age", result.Keys);
            Assert.Contains("Name", result.Keys);
            Assert.Contains("LevelKind", result.Keys);
            Assert.Contains("LevelIntelligence", result.Keys);
        }

        [Fact]
        public void ReadField_RabbitType_ReturnsCorrectFields()
        {
            var result = _informAnimalField.ReadField("кролик");

            Assert.Equal(5, result.Count);
            Assert.Contains("Food", result.Keys);
            Assert.Contains("Gender", result.Keys);
            Assert.Contains("Age", result.Keys);
            Assert.Contains("Name", result.Keys);
            Assert.Contains("LevelKind", result.Keys);
        }

        [Fact]
        public void ReadField_TigerType_ReturnsCorrectFields()
        {
            var result = _informAnimalField.ReadField("тигр");

            Assert.Equal(6, result.Count);
            Assert.Contains("BiteForce", result.Keys);
            Assert.Contains("NumberPolo", result.Keys);
        }

        [Fact]
        public void ReadField_WolfType_ReturnsCorrectFields()
        {
            var result = _informAnimalField.ReadField("волк");

            Assert.Equal(5, result.Count);
            Assert.Contains("BiteForce", result.Keys);
        }

        [Fact]
        public void ReadField_InvalidAnimalType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _informAnimalField.ReadField("неизвестноеживотное"));
        }

        [Fact]
        public void ReadField_AllFieldsHaveMessages()
        {
            var animals = new[] { "обезьяна", "кролик", "тигр", "волк" };

            foreach (var animal in animals)
            {
                var result = _informAnimalField.ReadField(animal);
                foreach (var field in result)
                {
                    Assert.False(string.IsNullOrEmpty(field.Value));
                }
            }
        }
    }

    public class InformThingFieldTests
    {
        private readonly InformThingField _informThingField = new InformThingField();

        [Fact]
        public void ReadField_TableType_ReturnsCorrectFields()
        {
            var result = _informThingField.ReadField("стол");

            Assert.Equal(3, result.Count);
            Assert.Contains("InventorNumber", result.Keys);
            Assert.Contains("Height", result.Keys);
            Assert.Contains("Width", result.Keys);
        }

        [Fact]
        public void ReadField_ComputerType_ReturnsCorrectFields()
        {
            var result = _informThingField.ReadField("компьютер");

            Assert.Equal(2, result.Count);
            Assert.Contains("InventorNumber", result.Keys);
            Assert.Contains("AmountAvailableMemory", result.Keys);
        }

        [Fact]
        public void ReadField_InvalidThingType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _informThingField.ReadField("неизвестнаявещь"));
        }

        [Fact]
        public void ReadField_AllFieldsHaveMessages()
        {
            var things = new[] { "стол", "компьютер" };

            foreach (var thing in things)
            {
                var result = _informThingField.ReadField(thing);
                foreach (var field in result)
                {
                    Assert.False(string.IsNullOrEmpty(field.Value));
                }
            }
        }
    }

    public class ThingFieldValidatorTests
    {
        private readonly ThingFieldValidator _validator = new ThingFieldValidator();

        [Fact]
        public void ValidateThingFields_ValidData_NoException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"InventorNumber", "123"},
                {"Height", "50"},
                {"Width", "30"},
                {"AmountAvailableMemory", "512"}
            };

            var exception = Record.Exception(() => _validator.ValidateThingFields(inputFields));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateThingFields_InvalidInventorNumber_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"InventorNumber", "-5"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateThingFields(inputFields));
        }

        [Fact]
        public void ValidateThingFields_InvalidHeight_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Height", "0"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateThingFields(inputFields));
        }

        [Fact]
        public void ValidateThingFields_InvalidWidth_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"Width", "-10"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateThingFields(inputFields));
        }

        [Fact]
        public void ValidateThingFields_InvalidMemory_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"AmountAvailableMemory", "abc"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateThingFields(inputFields));
        }

        [Fact]
        public void ValidateThingFields_UnknownField_ThrowsException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"UnknownField", "value"}
            };

            Assert.Throws<ArgumentException>(() => _validator.ValidateThingFields(inputFields));
        }

        [Fact]
        public void ValidateThingFields_TableFields_ValidData_NoException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"InventorNumber", "100"},
                {"Height", "80"},
                {"Width", "120"}
            };

            var exception = Record.Exception(() => _validator.ValidateThingFields(inputFields));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateThingFields_ComputerFields_ValidData_NoException()
        {
            var inputFields = new Dictionary<string, string>
            {
                {"InventorNumber", "200"},
                {"AmountAvailableMemory", "1024"}
            };

            var exception = Record.Exception(() => _validator.ValidateThingFields(inputFields));
            Assert.Null(exception);
        }
    }
}
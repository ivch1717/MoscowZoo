using Xunit;
using MoscowZoo;
using MoscowZoo.InputOutput;
using MoscowZoo.reading_fields;
using MoscowZoo.service;
using Moq;
using System;
using System.Collections.Generic;

namespace MoscowZoo.Tests
{
    public class MainWorkTests
    {
        private readonly Mock<IMenu> _mockMenu;
        private readonly Mock<IInputService> _mockInputService;
        private readonly Mock<IOutputService> _mockOutputService;
        private readonly Mock<IAnimalFieldValidator> _mockAnimalFieldValidator;
        private readonly Mock<IInformAnimalField> _mockInformAnimalField;
        private readonly Mock<IThingFieldValidator> _mockThingFieldValidator;
        private readonly Mock<IInformThingField> _mockInformThingField;
        private readonly Mock<IAnimalService> _mockAnimalService;
        private readonly Mock<IThingService> _mockThingService;
        private readonly MainWork _mainWork;

        public MainWorkTests()
        {
            _mockMenu = new Mock<IMenu>();
            _mockInputService = new Mock<IInputService>();
            _mockOutputService = new Mock<IOutputService>();
            _mockAnimalFieldValidator = new Mock<IAnimalFieldValidator>();
            _mockInformAnimalField = new Mock<IInformAnimalField>();
            _mockThingFieldValidator = new Mock<IThingFieldValidator>();
            _mockInformThingField = new Mock<IInformThingField>();
            _mockAnimalService = new Mock<IAnimalService>();
            _mockThingService = new Mock<IThingService>();

            _mainWork = new MainWork(
                _mockMenu.Object,
                _mockInputService.Object,
                _mockOutputService.Object,
                _mockAnimalFieldValidator.Object,
                _mockInformAnimalField.Object,
                _mockThingFieldValidator.Object,
                _mockInformThingField.Object,
                _mockAnimalService.Object,
                _mockThingService.Object);
        }
        

        [Fact]
        public void RemoveAnimal_ValidId_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(2);
            _mockInputService.Setup(i => i.InputInt(It.IsAny<string>())).Returns(0);
            _mockAnimalService.Setup(a => a.Remove(0)).Returns("Животное выпущено на волю");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockAnimalService.Verify(a => a.Remove(0), Times.Once);
            _mockOutputService.Verify(o => o.Output("Животное выпущено на волю"), Times.Once);
        }

        [Fact]
        public void RemoveThing_ValidId_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(3);
            _mockInputService.Setup(i => i.InputInt(It.IsAny<string>())).Returns(1);
            _mockThingService.Setup(t => t.Remove(1)).Returns("Инвентарь успешно удалён");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockThingService.Verify(t => t.Remove(1), Times.Once);
            _mockOutputService.Verify(o => o.Output("Инвентарь успешно удалён"), Times.Once);
        }

        [Fact]
        public void ReportAnimals_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(4);
            _mockAnimalService.Setup(a => a.Report()).Returns("Список животных");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockAnimalService.Verify(a => a.Report(), Times.Once);
            _mockOutputService.Verify(o => o.Output("Список животных"), Times.Once);
        }

        [Fact]
        public void ReportThings_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(5);
            _mockThingService.Setup(t => t.Report()).Returns("Список вещей");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockThingService.Verify(t => t.Report(), Times.Once);
            _mockOutputService.Verify(o => o.Output("Список вещей"), Times.Once);
        }

        [Fact]
        public void ReportFood_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(6);
            _mockAnimalService.Setup(a => a.ReportFood()).Returns("Отчёт по корму");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockAnimalService.Verify(a => a.ReportFood(), Times.Once);
            _mockOutputService.Verify(o => o.Output("Отчёт по корму"), Times.Once);
        }

        [Fact]
        public void ContactZoo_CallsServiceCorrectly()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(7);
            _mockAnimalService.Setup(a => a.ContactZoo()).Returns("Контактный зоопарк");

            var exception = Record.Exception(() => _mainWork.Run());

            _mockAnimalService.Verify(a => a.ContactZoo(), Times.Once);
            _mockOutputService.Verify(o => o.Output("Контактный зоопарк"), Times.Once);
        }

        [Fact]
        public void AddAnimal_TypeToLower_Called()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Returns(0);
            _mockInputService.Setup(i => i.Input(It.IsAny<string>())).Returns("КРОЛИК"); // uppercase
            
            var animalFields = new Dictionary<string, string>();
            _mockInformAnimalField.Setup(i => i.ReadField("кролик")).Returns(animalFields); // expect lowercase

            var exception = Record.Exception(() => _mainWork.Run());

            _mockInformAnimalField.Verify(i => i.ReadField("кролик"), Times.Once);
        }

        [Fact]
        public void ErrorHandling_ArgumentException_OutputsErrorMessage()
        {
            var menuItems = new string[]
            {
                "Добавить животное", "Добавить вещь", "Удалить животное", "Удалить вещь",
                "Список животных", "Список вещей", "Отчёт по корму", "Контактный зоопарк", "Выйти"
            };
            
            _mockMenu.Setup(m => m.ReadingMenu(menuItems)).Throws(new ArgumentException("Test error"));

            var exception = Record.Exception(() => _mainWork.Run());

            _mockOutputService.Verify(o => o.Output("Ошибка ввода: Test error"), Times.Once);
        }
    }
}
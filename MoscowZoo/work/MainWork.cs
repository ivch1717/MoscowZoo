using MoscowZoo.InputOutput;
using MoscowZoo.reading_fields;
using MoscowZoo.service;

namespace MoscowZoo;

public class MainWork
{
    private string[] menu =
    [
        "Добавить животное", "Добавить вещь",
        "Удалить животное", "Удалить вещь", "Список животных", "Список вещей", "Отчёт по корму",
        "Контактный зоопарк", "Выйти"
    ];
    
    private IMenu _menu;
    private IInputService _inputService;
    private IOutputService _outputService;
    private IAnimalFieldValidator _animalFieldValidator;
    private IInformAnimalField _informAnimalField;
    private IThingFieldValidator _thingFieldValidator;
    private IInformThingField _informThingField;
    private IAnimalService _animalService;
    private IThingService _thingService;

    public MainWork(
        IMenu menu,
        IInputService inputService,
        IOutputService outputService,
        IAnimalFieldValidator animalFieldValidator,
        IInformAnimalField informAnimalField,
        IThingFieldValidator thingFieldValidator,
        IInformThingField informThingField,
        IAnimalService animalService,
        IThingService thingService)
    {
        _menu = menu;
        _inputService = inputService;
        _outputService = outputService;
        _animalFieldValidator = animalFieldValidator;
        _informAnimalField = informAnimalField;
        _thingFieldValidator = thingFieldValidator;
        _informThingField = informThingField;
        _animalService = animalService;
        _thingService = thingService;
    }

    public void Run()
    {
        ExecuteWithErrorHandling(RunMenu);
    }

    
    /// <summary>
    /// Обрабатывает все ошибки
    /// </summary>
   private void ExecuteWithErrorHandling(Action action)
{
    while (true)
    {
        try
        {
            action();
            WaitForContinue();
        }
        catch (ArgumentNullException ex)
        {
            _outputService.Output($"Ошибка: значение не может быть null");
            WaitForContinue();
        }
        catch (ArgumentException ex)
        {
            _outputService.Output($"Ошибка ввода: {ex.Message}");
            WaitForContinue();
        }
        catch (FormatException ex)
        {
            _outputService.Output($"Ошибка формата данных: {ex.Message}");
            WaitForContinue();
        }
        catch (InvalidCastException ex)
        {
            _outputService.Output($"Ошибка преобразования типа: {ex.Message}");
            WaitForContinue();
        }
        catch (IndexOutOfRangeException ex)
        {
            _outputService.Output($"Ошибка индекса: неверный индекс массива или коллекции");
            WaitForContinue();
        }
        catch (KeyNotFoundException ex)
        {
            _outputService.Output($"Ошибка ключа: элемент не найден в словаре");
            WaitForContinue();
        }
        catch (NullReferenceException ex)
        {
            _outputService.Output($"Ошибка ссылки: объект не инициализирован");
            WaitForContinue();
        }
        catch (InvalidOperationException ex)
        {
            _outputService.Output($"Ошибка операции: {ex.Message}");
            WaitForContinue();
        }
        catch (NotSupportedException ex)
        {
            _outputService.Output($"Ошибка: операция не поддерживается");
            WaitForContinue();
        }
        catch (OverflowException ex)
        {
            _outputService.Output($"Ошибка переполнения: число слишком большое или маленькое");
            WaitForContinue();
        }
        catch (IOException ex) when (ex.GetType().Name == "ConsoleIOException")
        {
            _outputService.Output($"Ошибка ввода/вывода консоли: {ex.Message}");
            WaitForContinue();
        }
    }
}

    private void RunMenu()
    {
        int choice = _menu.ReadingMenu(menu);
        switch (choice)
        {
            case 0:
                AddAnimal();
                break;
            case 1:
                AddThing();
                break;
            case 2:
                int id = _inputService.InputInt("Введите номер животного которого хотите отпустить на волю");
                _outputService.Output(_animalService.Remove(id));
                break;
            case 3:
                int idThing = _inputService.InputInt("Введите номер вещи которую хотите отдать нуждающимся");
                _outputService.Output(_thingService.Remove(idThing));
                break;
            case 4:
                _outputService.Output(_animalService.Report());
                break;
            case 5:
                _outputService.Output(_thingService.Report());
                break;
            case 6:
                _outputService.Output(_animalService.ReportFood());
                break;
            case 7:
                _outputService.Output(_animalService.ContactZoo());
                break;
            case 8:
                Environment.Exit(0);
                break;
        }
    }

    private void AddAnimal()
    {
        string type = _inputService.Input("Введите животного которого хотите добавить:\nобезьяна\nкролик\nтигр\nволк");
        type = type.ToLower();
        Dictionary<string, string> mes = _informAnimalField.ReadField(type);
        Dictionary<string, string> args = new();
        List<string> parameters = new();
            
        foreach (KeyValuePair<string, string> pair in mes)
        {
            string key = _inputService.Input(pair.Value);
            args.Add(pair.Key, key);
            parameters.Add(key);
        }
            
        string[] param = parameters.ToArray();
        _animalFieldValidator.ValidateAnimalFields(args);
        _outputService.Output(_animalService.Add(type, param));
    }

    private void AddThing()
    {
        string type = _inputService.Input("Введите вещь которую хотите добавить:\nстол\nкомпьютер");
        Dictionary<string, string> mes = _informThingField.ReadField(type);
        Dictionary<string, string> args = new();
        List<string> parameters = new();
            
        foreach (KeyValuePair<string, string> pair in mes)
        {
            string key = _inputService.Input(pair.Value);
            args.Add(pair.Key, key);
            parameters.Add(key);
        }
            
        string[] param = parameters.ToArray();
        _thingFieldValidator.ValidateThingFields(args);
        _outputService.Output(_thingService.Add(type, param));
    }


    private void WaitForContinue()
    {
        _inputService.Wait("Нажмите любую клавишу для продолжения...");
    }
}


namespace MoscowZoo.reading_fields;


/// <summary>
/// Хранит информацию обо всех полях у классов, и какие для них нужны сообщения. Выдаёт их для определённого класса.
/// </summary>
public class InformAnimalField: IInformAnimalField
{
    private Dictionary<string, List<string>> animalFields = new Dictionary<string, List<string>>
    {
        { "обезьяна", new List<string> { "Food", "Gender", "Age", "Name", "LevelKind", "LevelIntelligence" } },
        { "кролик", new List<string> { "Food", "Gender", "Age", "Name", "LevelKind" } },
        { "тигр", new List<string> { "Food", "Gender", "Age", "Name", "BiteForce", "NumberPolo" } },
        { "волк", new List<string> { "Food", "Gender", "Age", "Name", "BiteForce" } },
    };

    private Dictionary<string, string> messageFields = new Dictionary<string, string>
    {
        {"Food", "Введите какое количество килограммов еды оно потребляют в сутки (>0)"},
        {"Gender", "Введите пол мужской или женский"},
        {"Age", "Введите возраст - положительное число"},
        {"Name", "Введите имя"},
        {"LevelKind", "Введите уровень доброты (от 1 до 10)"},
        {"LevelIntelligence", "Введите уровень интеллекта (от 1 до 100)"},
        {"BiteForce", "Введите силу укуса (в кг/см²)"},
        {"NumberPolo", "Введите количество полосок"},
    };

    public Dictionary<string, string> ReadField(string type)
    {
        if (!animalFields.ContainsKey(type))
        {
            throw new ArgumentException("Введите корректное имя животного");
        }

        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (var i in animalFields[type])
        {
            if (!messageFields.ContainsKey(i))
            {
                throw new ArgumentException();
            }
            result.Add(i, messageFields[i]);
        }
        return result;
    }
}
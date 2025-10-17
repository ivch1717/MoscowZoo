namespace MoscowZoo.reading_fields;


/// <summary>
/// Проверяет на корректность поля
/// </summary>
public class AnimalFieldValidator: IAnimalFieldValidator
{
    private Dictionary<string, string> _defaultValues = new Dictionary<string, string>
    {
        {"Food", "1"},
        {"Gender", "Мужской"},
        {"Age", "1"},
        {"Name", "Дружок"},
        {"LevelKind", "1"},
        {"LevelIntelligence", "1"},
        {"BiteForce", "1"},
        {"NumberPolo", "1"},
    };

    public void ValidateAnimalFields(Dictionary<string, string> inputFields)
    {
        Dictionary<string, string> result = new Dictionary<string, string>(_defaultValues);
        foreach (var inputField in inputFields)
        {
            if (result.ContainsKey(inputField.Key))
            {
                result[inputField.Key] = inputField.Value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        Check(result);
    }

    private void Check(Dictionary<string, string> cur)
    {
        foreach (var field in cur)
        {
            string error = ValidateField(field.Key, field.Value);
            if (!string.IsNullOrEmpty(error))
            {
                throw new ArgumentException($"Ошибка в поле '{field.Key}': {error}");
            }
        }
    }

    private string ValidateField(string fieldName, string value)
    {
        return fieldName switch
        {
            "Food" => ValidateFood(value),
            "Gender" => ValidateGender(value),
            "Age" => ValidateAge(value),
            "Name" => ValidateName(value),
            "LevelKind" => ValidateLevelKind(value),
            "LevelIntelligence" => ValidateLevelIntelligence(value),
            "BiteForce" => ValidateBiteForce(value),
            "NumberPolo" => ValidateNumberPolo(value),
            _ => $"Неизвестное поле для валидации: {fieldName}"
        };
    }

    private string ValidateFood(string value)
    {
        if (!double.TryParse(value, out double food) || food <= 0)
            return "количество килограммов еды должно быть положительным числом";
        return null;
    }

    private string ValidateGender(string value)
    {
        // Приводим к нижнему регистру для сравнения с enum
        string lowerValue = value.ToLower();
        if (lowerValue != "мужской" && lowerValue != "женский")
            return "пол должен быть Мужской или Женский";
        return null;
    }

    private string ValidateAge(string value)
    {
        if (!int.TryParse(value, out int age) || age <= 0)
            return "возраст должен быть положительным числом";
        return null;
    }

    private string ValidateName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "имя не может быть пустым";
        return null;
    }

    private string ValidateLevelKind(string value)
    {
        if (!int.TryParse(value, out int level) || level < 1 || level > 10)
            return "уровень доброты должен быть от 1 до 10";
        return null;
    }

    private string ValidateLevelIntelligence(string value)
    {
        if (!int.TryParse(value, out int level) || level < 1 || level > 100)
            return "уровень интеллекта должен быть от 1 до 100";
        return null;
    }

    private string ValidateBiteForce(string value)
    {
        if (!double.TryParse(value, out double force) || force <= 0)
            return "сила укуса должна быть положительным числом";
        return null;
    }

    private string ValidateNumberPolo(string value)
    {
        if (!int.TryParse(value, out int number) || number <= 0)
            return "количество полосок должно быть положительным числом";
        return null;
    }

}
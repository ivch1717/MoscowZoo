namespace MoscowZoo.reading_fields;

/// <summary>
/// Хранит информацию обо всех полях у классов, и какие для них нужны сообщения. Выдаёт их для определённого класса.
/// </summary>
public class ThingFieldValidator: IThingFieldValidator
{
    private Dictionary<string, string> _defaultValues = new Dictionary<string, string>
    {
        {"InventorNumber", "1"},
        {"Height", "1"},
        {"Width", "1"}, 
        {"AmountAvailableMemory", "1"}
    };

    public void ValidateThingFields(Dictionary<string, string> inputFields)
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
            "InventorNumber" => ValidateInventorNumber(value),
            "Height" => ValidateHeight(value),
            "Width" => ValidateWidth(value),
            "AmountAvailableMemory" => ValidateAmountAvailableMemory(value),
            _ => $"Неизвестное поле для валидации: {fieldName}"
        };
    }

    private string ValidateInventorNumber(string value)
    {
        if (!int.TryParse(value, out int number) || number <= 0)
            return "инвентарный номер должен быть положительным числом";
        return null;
    }

    private string ValidateHeight(string value)
    {
        if (!int.TryParse(value, out int height) || height <= 0)
            return "длина стола должна быть положительным числом";
        return null;
    }

    private string ValidateWidth(string value)
    {
        if (!int.TryParse(value, out int width) || width <= 0)
            return "ширина должна быть положительным числом";
        return null;
    }

    private string ValidateAmountAvailableMemory(string value)
    {
        if (!int.TryParse(value, out int memory) || memory <= 0)
            return "доступная память на компьютере должна быть положительным числом в гб";
        return null;
    }
}
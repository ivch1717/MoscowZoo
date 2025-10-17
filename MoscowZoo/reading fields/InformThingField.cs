namespace MoscowZoo.reading_fields;

public class InformThingField: IInformThingField
{
    private Dictionary<string, List<string>> thingFields = new Dictionary<string, List<string>>
    {
        { "стол", new List<string> { "InventorNumber", "Height", "Width"} },
        { "компьютер", new List<string> { "InventorNumber", "AmountAvailableMemory" } },
    };

    private Dictionary<string, string> messageFields = new Dictionary<string, string>
    {
        {"InventorNumber", "Введите инвентарный номер - положительное число"},
        {"Height", "Введите длину стола - положительное число"},
        {"Width", "Введите ширину - положительное число"}, 
        {"AmountAvailableMemory", "Введите доступную память на компьютере в гб - положительное число"}
    };
    
    public Dictionary<string, string> ReadField(string type)
    {
        if (!thingFields.ContainsKey(type))
        {
            throw new ArgumentException("Введите корректную вещь");
        }

        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (var i in thingFields[type])
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
namespace MoscowZoo.fabrics;

/// <summary>
/// Возвращает фабрику по типу
/// </summary>
public class FactoryAnimalResolver: IFactoryAnimalResolver
{
    private Dictionary<string, IAnimalFabric> _fabrics = new Dictionary<string, IAnimalFabric>
    {
        {"обезьяна", new MonkeyFabric()},
        {"кролик", new RabbitFabric()},
        {"тигр", new TigerFabric()},
        {"волк", new WolfFabric()},
    };

    public IAnimalFabric GetFabric(string type)
    {
        type = type.ToLower();
        return _fabrics[type];
    }
}
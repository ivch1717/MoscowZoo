namespace MoscowZoo.fabrics;

public class FactoryThingResolver: IFactoryThingResolver
{
    
    private Dictionary<string, IThingFabric> _fabrics = new Dictionary<string, IThingFabric>
    {
        {"компьютер", new ComputerFabric()},
        {"стол", new TableFabric()},
    };

    public IThingFabric GetFabric(string type)
    {
        type = type.ToLower();
        return _fabrics[type];
    }
}
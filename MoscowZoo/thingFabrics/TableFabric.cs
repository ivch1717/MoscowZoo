namespace MoscowZoo.fabrics;

public class TableFabric:IThingFabric
{
    public IInventory CreateThing(params object[] parameters)
    {
        return new Table(int.Parse((string)parameters[0]), int.Parse((string)parameters[1]), int.Parse((string)parameters[2]));
    }
}
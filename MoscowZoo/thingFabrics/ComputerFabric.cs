namespace MoscowZoo.fabrics;

public class ComputerFabric:IThingFabric
{
    public IInventory CreateThing(params object[] parameters)
    {
        return new Computer(int.Parse((string)parameters[0]), int.Parse((string)parameters[1]));
    }
}
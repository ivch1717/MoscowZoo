namespace MoscowZoo.fabrics;

public interface IThingFabric
{
    public IInventory CreateThing(params object[] parameters);
}
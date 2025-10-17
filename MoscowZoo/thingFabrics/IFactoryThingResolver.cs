namespace MoscowZoo.fabrics;

public interface IFactoryThingResolver
{
    public IThingFabric GetFabric(string type);
}
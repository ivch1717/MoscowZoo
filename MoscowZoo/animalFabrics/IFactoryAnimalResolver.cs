namespace MoscowZoo.fabrics;

public interface IFactoryAnimalResolver
{
    public IAnimalFabric GetFabric(string type);
}
namespace MoscowZoo.fabrics;

public interface IAnimalFabric
{
    public IAlive CreateAnimal(params object[] parameters);
}

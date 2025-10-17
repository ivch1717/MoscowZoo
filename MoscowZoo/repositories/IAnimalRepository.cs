namespace MoscowZoo.repositories;

public interface IAnimalRepository
{
    void RemoveAnimal(int id);
    void AddAnimal(IAlive animal);
    public List<IAlive> GetAnimals();
    public bool IsEmpty();
}
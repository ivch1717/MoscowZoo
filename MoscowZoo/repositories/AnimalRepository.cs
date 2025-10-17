namespace MoscowZoo.repositories;

public class AnimalRepository: IAnimalRepository
{
    private List<IAlive> Animals = new List<IAlive>();
    public void AddAnimal(IAlive animal)
    {
        Animals.Add(animal);
    }

    public void RemoveAnimal(int id)
    {
        if (id < 0 || id >= this.Animals.Count)
        {
            throw new ArgumentException("Неправильный id");
        }
        Animals.RemoveAt(id);
    }
    
    public List<IAlive> GetAnimals()
    {
        return Animals;
    }
    
    public bool IsEmpty()
    {
        return Animals.Count == 0;
    }
}
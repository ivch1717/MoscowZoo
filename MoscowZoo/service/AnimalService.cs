using MoscowZoo.fabrics;
using MoscowZoo.repositories;
using MoscowZoo.vet_clinic;

namespace MoscowZoo.service;

public class AnimalService:IAnimalService
{
    IAnimalRepository _repository;
    IFactoryAnimalResolver _animalResolver;
    IVeterinaryClinic _veterinaryClinic;
    public AnimalService(IAnimalRepository repository, IFactoryAnimalResolver animalResolver,  IVeterinaryClinic veterinaryClinic)
    {
        _repository = repository;
        _animalResolver = animalResolver;
        _veterinaryClinic = veterinaryClinic;
    }

    public string Add(string type, params object[] parameters)
    {
        IAlive animal = _animalResolver.GetFabric(type).CreateAnimal(parameters);
        if (!_veterinaryClinic.IsHealthy(animal))
        {
            return "К сожалению животное больное, мы пока не можем добавить его в зоопарк";
        }
        _repository.AddAnimal(animal);
        return "Животное успешно добавлено";
    }
    
    public string Remove(int id)
    {
        if (_repository.IsEmpty())
        {
            return "В нашем ззопарке нет животных";
        }
        _repository.RemoveAnimal(id);
        return "Животное выпущено на волю";
    }
    
    public string Report()
    {
        if (_repository.IsEmpty())
        {
            return "В нашем зоопарке пока ещё нет животных(\nПриходите завтра!";
        }
        int id = 0;
        string rep = "Спиок животных:\n";
        foreach (var i in _repository.GetAnimals())
        {
            rep += id.ToString() + ": " + i.ToString();
            rep += "\n";
            id++;
        }
        
        return rep;
    }
    
    public string ReportFood()
    {
        if (_repository.IsEmpty())
        {
            return "В нашем зоопарке пока ещё нет животных(\nПриходите завтра!";
        }
        List<IAlive> d = new List<IAlive>(_repository.GetAnimals());
        int sum = 0;
        foreach (var i in d)
        {
            sum += i.Food;
        }
        return "Количество килограм корма нужное животынм в день: " + sum.ToString();
    }

    public string ContactZoo()
    {
        if (_repository.IsEmpty())
        {
            return "В нашем зоопарке пока ещё нет животных(\nПриходите завтра!";
        }
        List<IAlive> d = new List<IAlive>(_repository.GetAnimals());
        string rep = "Контактный ззопарк:\n";
        bool empty = true;
        foreach (var i in d)
        {
            if (i is Herbo herbo)
            {
                if (herbo.LevelKind > 5)
                {
                    rep += herbo.ToString();
                    rep += "\n";
                    empty = false;
                }
            }
        }

        if (empty)
        {
            return "В нашем ззопарке пока нет контактных животных";
        }
        return rep;
    }
}
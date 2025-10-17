using MoscowZoo.fabrics;
using MoscowZoo.repositories;

namespace MoscowZoo.service;

public class ThingService: IThingService
{
    IThingRepository _repository;
    IFactoryThingResolver _factoryThingResolver;
    public ThingService(IThingRepository repository, IFactoryThingResolver factoryThingResolver)
    {
        _repository = repository;
        
       _factoryThingResolver = factoryThingResolver;
    }

    public string Add(string type, params object[] parameters)
    {
        _repository.AddThing(_factoryThingResolver.GetFabric(type).CreateThing(parameters));
        return "Инвернтарь успешно добавлен";
    }

    public string Remove(int id)
    {
        if (_repository.IsEmpty())
        {
            return "Список вещей пустой";
        }
        _repository.RemoveInventory(id);
        return "инвентарь успешно удалён";
    }

    public string Report()
    {
        if (_repository.IsEmpty())
        {
            return "Список вещей пустой";
        }
        int id = 0;
        string rep = "Список вещей:\n";
        foreach (var i in _repository.GetInventory())
        {
            rep += id.ToString() + ": " + i.ToString();
            rep += "\n";
            id++;
        }
 
        return rep;
    }
}

namespace MoscowZoo.repositories;

public interface IThingRepository
{
    public void RemoveInventory(int id);
    public void AddThing(IInventory  inventory);
    public List<IInventory> GetInventory();
    public bool IsEmpty();
}
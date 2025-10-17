namespace MoscowZoo.repositories;

public class ThingRepository:IThingRepository
{
    private List<IInventory> Inventories = new List<IInventory>(); 
    public void RemoveInventory(int id)
    {
        if (id < 0 || id >= this.Inventories.Count)
        {
            throw new ArgumentException("Неправильный id");
        }
        Inventories.RemoveAt(id);
    }

    public void AddThing(IInventory  inventory)
    {
        Inventories.Add(inventory);
    }

    public List<IInventory>  GetInventory()
    {
        return Inventories;
    }

    public bool IsEmpty()
    {
        return Inventories.Count == 0;
    }
}
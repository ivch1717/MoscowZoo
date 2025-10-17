namespace MoscowZoo;

public abstract class Thing: IInventory
{
    public int InventorNumber { get; init; }

    public Thing(int inventorNumber)
    {
        InventorNumber = inventorNumber;
    }

    public override string ToString()
    {
        return $"Инвентарный номер: {InventorNumber}";
    }
}
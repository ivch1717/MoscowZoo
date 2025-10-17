namespace MoscowZoo;

public class Computer: Thing
{
    public int AmountAvailableMemory{get; init; }

    public Computer(int inventorNumber, int amountAvailableMemory) : base(inventorNumber)
    {
        AmountAvailableMemory = amountAvailableMemory;
    }
    
    public override string ToString()
    {
        return "Компьютер: " + base.ToString() + $", Количество свободной памяти: {AmountAvailableMemory}";
    }
}
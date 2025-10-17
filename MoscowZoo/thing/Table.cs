namespace MoscowZoo;

public class Table: Thing
{
    public int Height { get; init; }
    public int Width { get; init; }

    public Table(int inventorNumber, int height, int width) : base(inventorNumber)
    {
        Height = height;
        Width = width;
    }
    
    public override string ToString()
    {
        return "Стол: " + base.ToString() + $", Длина стола: {Height}, Ширина стола: {Width}";
    }
}
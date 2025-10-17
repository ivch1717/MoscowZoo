namespace MoscowZoo;

public class Rabbit: Herbo
{
    public Rabbit(int food, Gender gender, int age, string name,  int levelKind) : 
        base(food,  gender, age, name, levelKind) {}
    
    public override string ToString()
    {
        return "Кролик: " + base.ToString();
    }
}
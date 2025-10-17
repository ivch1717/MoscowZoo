namespace MoscowZoo;

public abstract class Predator: Animal
{
    
    public int BiteForce{get; init;}

    public Predator(int food, Gender gender, int age, string name, int biteForce) : base(food, gender, age, name)
    {
        BiteForce = biteForce;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Сила укуса: {BiteForce}";
    }
}
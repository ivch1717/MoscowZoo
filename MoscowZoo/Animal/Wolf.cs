namespace MoscowZoo;

public class Wolf: Predator
{
    public Wolf(int food, Gender gender, int age, string name, int biteForce) : base(food, gender, age, name, biteForce) 
    {}
    
    public override string ToString()
    {
        return "Волк: " + base.ToString();
    }
}
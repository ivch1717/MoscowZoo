namespace MoscowZoo;

public class Tiger: Predator
{
    public int NumberPolo {get; init;}

    public Tiger(int food, Gender gender, int age, string name, int biteForce, int numberPolo) :
        base(food, gender, age, name, biteForce)
    {
        NumberPolo = numberPolo;
    }
    
    public override string ToString()
    {
        return "Тигр: " + base.ToString() + $", Количество полосок: {NumberPolo}";
    }
}
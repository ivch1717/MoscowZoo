namespace MoscowZoo;

public class Monkey: Herbo
{
    public int LevelIntelligence  {get; init;}

    public Monkey(int food, Gender gender, int age, string name, int levelKind, int levelIntelligence) : 
        base(food, gender, age, name, levelKind) 
    {
        LevelIntelligence = levelIntelligence;
    }
    
    public override string ToString()
    {
        return "Обезьяна: " + base.ToString() + $", Уровень интеллекта: {LevelIntelligence}";
    }
}
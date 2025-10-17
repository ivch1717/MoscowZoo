namespace MoscowZoo;

public abstract class Herbo: Animal
{
    public int LevelKind {get; init;}
    public Herbo(int food, Gender gender, int age, string name,  int levelKind) : base(food,  gender, age, name)
    {
        LevelKind = levelKind;
    }

    public override string ToString()
    {
        return base.ToString() + $", Уровень доброты: {LevelKind}";
    }
}
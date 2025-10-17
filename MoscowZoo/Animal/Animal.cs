namespace MoscowZoo;

public abstract class Animal: IAlive
{
    public int Food {get; init;}
    public Gender Gender {get; init;}
    public int Age {get; init;}
    public string Name {get; init;}

    public Animal(int food, Gender gender, int age, string name)
    {
        Food = food;
        Gender =  gender;
        Age = age;
        Name = name;
    }

    public override string ToString()
    {
        return $"Количество килограммов еды/сутки: {Food}, Имя: {Name}, Возраст: {Age}, Пол: {Gender}";
    }
}
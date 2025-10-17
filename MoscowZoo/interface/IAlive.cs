namespace MoscowZoo;

public interface IAlive
{
    public int Food {get; init;}
    public Gender Gender {get; init;}
    public int Age {get; init;}
    public string Name {get; init;}
}
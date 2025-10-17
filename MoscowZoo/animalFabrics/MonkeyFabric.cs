namespace MoscowZoo.fabrics;

public class MonkeyFabric: IAnimalFabric
{
    public IAlive CreateAnimal(params object[] parameters)
    {
        return new Monkey(
            int.Parse((string)parameters[0]), 
            (Gender)Enum.Parse(typeof(Gender), (string)parameters[1], true), 
            int.Parse((string)parameters[2]), 
            (string)parameters[3],
            int.Parse((string)parameters[4]), 
            int.Parse((string)parameters[5])
        );
    }
}
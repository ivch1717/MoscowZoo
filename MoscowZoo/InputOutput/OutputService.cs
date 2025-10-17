namespace MoscowZoo.InputOutput;

public class OutputService: IOutputService
{
    public void Output(string message)
    {
        Console.WriteLine(message);
    }

    public void Clear()
    {
        Console.Clear();
    }
}
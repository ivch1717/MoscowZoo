namespace MoscowZoo.service;

public interface IAnimalService
{
    public string Add(string type, params object[] parameters);
    public string Remove(int id);
    public string Report();
    public string ReportFood();
    public string ContactZoo();
    

}
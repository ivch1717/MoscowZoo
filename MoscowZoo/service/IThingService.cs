namespace MoscowZoo.service;

public interface IThingService
{
    public string Add(string type, params object[] parameters);
    public string Remove(int id);
    public string Report();
}
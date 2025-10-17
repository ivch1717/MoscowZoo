namespace MoscowZoo.vet_clinic;

public class VeterinaryClinic: IVeterinaryClinic
{
    public bool IsHealthy(IAlive animal)
    {
        Random random = new Random();
        int healthPercent = random.Next(0, 21);

        if (animal.Food <= 2 || animal.Food >= 15)
        {
            healthPercent += 5;
        }

        if (animal.Gender == Gender.женский)
        {
            healthPercent += 5;
        }

        if (animal.Age <= 2 || animal.Age > 10)
        {
            healthPercent += 5;
        }

        if (animal.Name.Length < 10)
        {
            healthPercent += 3;
        }

        if (animal.Name.Length < 5)
        {
            healthPercent += 3;
        }

        int randomCheck = random.Next(0, 101);
        return randomCheck > healthPercent; 
    }
}
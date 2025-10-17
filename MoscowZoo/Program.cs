using Microsoft.Extensions.DependencyInjection;
using MoscowZoo.InputOutput;
using MoscowZoo.fabrics;
using MoscowZoo.reading_fields;
using MoscowZoo.repositories;
using MoscowZoo.service;
using MoscowZoo.vet_clinic;
namespace MoscowZoo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAnimalRepository, AnimalRepository>();
            services.AddSingleton<IThingRepository, ThingRepository>();
            
            services.AddSingleton<MonkeyFabric>();
            services.AddSingleton<RabbitFabric>();
            services.AddSingleton<TigerFabric>();
            services.AddSingleton<WolfFabric>();
            services.AddSingleton<ComputerFabric>();
            services.AddSingleton<TableFabric>();
            
            services.AddSingleton<IInputService, InputService>();
            services.AddSingleton<IOutputService, OutputService>();
            
            services.AddSingleton<IAnimalFieldValidator, AnimalFieldValidator>();
            services.AddSingleton<IInformAnimalField, InformAnimalField>();
            services.AddSingleton<IThingFieldValidator, ThingFieldValidator>();
            services.AddSingleton<IInformThingField,  InformThingField>();
            
            services.AddSingleton<IAnimalService, AnimalService>();
            services.AddSingleton<IThingService, ThingService>();
            
            services.AddSingleton<IFactoryAnimalResolver, FactoryAnimalResolver>();
            services.AddSingleton<IFactoryThingResolver, FactoryThingResolver>();
            
            services.AddSingleton<IMenu, Menu>();
            services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
            
            services.AddSingleton<MainWork>();
            var serviceProvider = services.BuildServiceProvider();
            
            var app = serviceProvider.GetRequiredService<MainWork>();
            app.Run();
        }
    }
}

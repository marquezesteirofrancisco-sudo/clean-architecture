using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// Tus namespaces
using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;
using CA_InterfaceAdapters_Repository;
using CL_EnterpriseLayer;

// 1. Configurar el Host (Esto gestiona el DI y la Configuración)
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Obtener cadena de conexión desde appsettings.json de la consola
        var connectionString = "Server=PC_ROQUE\\SQLEXPRESS04;Database=CLEAN_ARCHITECTURE;User ID=sa;Password=marka;Encrypt=false;MultipleActiveResultSets=true";

        // --- REGISTRO DE DEPENDENCIAS ---

        // Base de Datos
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Capa de Adaptadores e Infraestructura
        services.AddScoped<IRepository<Beer>, BeerRepository>();
        services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
        services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();

        // Capa de Aplicación (Casos de Uso)
        services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
        services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
    })
    .Build();

// 2. Ejecutar la lógica de la aplicación
await RunConsoleLogic(host.Services);

static async Task RunConsoleLogic(IServiceProvider services)
{
    // Creamos un Scope para resolver los servicios Scoped (como el DbContext)
    using IServiceScope scope = services.CreateScope();
    var provider = scope.ServiceProvider;

    // Resolvemos los casos de uso
    var getBeerUseCase = provider.GetRequiredService<GetBeerUseCase<Beer, BeerViewModel>>();
    var addBeerUseCase = provider.GetRequiredService<AddBeerUseCase<BeerRequestDTO>>();

    // --- EJEMPLO: GUARDAR ---
    Console.WriteLine("Agregando una nueva cerveza...");
    var newBeer = new BeerRequestDTO { Name = "Consola Beer", Style = "Clean Brand", Alcohol=10 }; // Ajusta a tus campos
    await addBeerUseCase.ExecuteAsync(newBeer);
    Console.WriteLine("¡Cerveza guardada con éxito!");

    // --- EJEMPLO: LISTAR ---
    Console.WriteLine("\nListado de Cervezas:");
    var beers = await getBeerUseCase.ExecuteAsync();
    foreach (var beer in beers)
    {
        Console.WriteLine($"- {beer.Name}"); // Ajusta a las propiedades de tu BeerViewModel
    }
}

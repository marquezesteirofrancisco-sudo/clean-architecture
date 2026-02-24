using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// Tus capas
using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;
using CA_InterfaceAdapters_Repository;
using CL_EnterpriseLayer;


namespace Proyecto_Pruebas
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Configurar el Host
            var host = CreateHostBuilder().Build();

            // 2. Ejecutar la aplicación usando el contenedor para crear el Form
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Resolvemos el formulario principal (que ya tiene las dependencias inyectadas)
                    var mainForm = services.GetRequiredService<Form1>();
                    Application.Run(mainForm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al iniciar: {ex.Message}");
                }
            }
        }

        static IHostBuilder CreateHostBuilder() =>
             Host.CreateDefaultBuilder()
                 .ConfigureServices((context, services) =>
                 {
                     // Base de Datos
                     var connectionString = "Server=PC_ROQUE\\SQLEXPRESS04;Database=CLEAN_ARCHITECTURE;User ID=sa;Password=marka;Encrypt=false;MultipleActiveResultSets=true";
                     services.AddDbContext<AppDbContext>(options =>
                         options.UseSqlServer(connectionString));

                     // Registro de capas (Mappers, Repositorios, Presenters)
                     services.AddScoped<IRepository<Beer>, BeerRepository>();
                     services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
                     services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();

                     // Casos de Uso
                     services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
                     services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();

                     // REGISTRO DEL FORMULARIO (Crucial para que funcione el constructor)
                     services.AddTransient<Form1>();
                 });

    }
}
using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Presenters;
using CA_InterfaceAdapters_Repository;
using CL_EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


IConfiguration configuration = builder.Build();


var container = new ServiceCollection()
        .AddDbContext<AppDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("ConexionSQLServer")))
        .AddScoped<IRepository<Beer>, BeerRepository>()
        .AddScoped<GetBeerUseCase<Beer, BeerViewModel>>()
        .AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>()
        .AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>()
        .AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>()
        .BuildServiceProvider();


var getBeerUseCase = container.GetRequiredService<GetBeerUseCase<Beer, BeerViewModel>>();

var beers = await getBeerUseCase.ExecuteAsync();

foreach (var beer in beers)
{
    Console.WriteLine($"Id: {beer.Id}, Name: {beer.Name}, Type: {beer.Style} , Alcohol: {beer.Alcohol}");
}



var getBeerUseCaseDetail = container.GetRequiredService<GetBeerUseCase<Beer, BeerDetailViewModel>>();

var beersDetails = await getBeerUseCaseDetail.ExecuteAsync();

foreach (var beer in beersDetails)
{
    Console.WriteLine($"Id: {beer.Id}, Name: {beer.Name}, Type: {beer.Style} , Alcohol: {beer.Alcohol} , Color: {beer.Color} , Message: {beer.Message} ");
}
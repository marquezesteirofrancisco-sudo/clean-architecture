using CA_ApplicationLayer;

using CA_FrameworksDrivers_ExtenalService;
using CA_InterfaceAdapters_Adapters;
using CA_InterfaceAdapters_Adapters.Dtos;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;
using CA_InterfaceAdapters_Repository;
using CL_EnterpriseLayer;

public static class DependencyContainer
{
    public static IServiceCollection AddBackendServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. INFRAESTRUCTURA Y FRAMEWORKS (HttpClient)
        services.AddHttpClient<IExternalService<PostServiceDTO>, PostService>(c =>
        {
            var url = configuration["BaseUrlPost"];
            if (string.IsNullOrEmpty(url)) throw new Exception("¡BaseUrlPost no configurado!");
            c.BaseAddress = new Uri(url);
        });

        // 2. REPOSITORIOS, MAPPERS Y PRESENTERS (Beer)
        services.AddScoped<IRepository<Beer>, BeerRepository>();
        services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
        services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
        services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
        services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();

        // 3. REPOSITORIOS, MAPPERS Y PRESENTERS (User)
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IPresenter<User, UserViewModel>, UserPresenter>();
        services.AddScoped<IMapper<UserRequestDTO, User>, UserMapper>();

        // 4. REPOSITORIOS, MAPPERS Y PRESENTERS (Sales)
        services.AddScoped<IRepository<Sale>, SaleRepository>();
        services.AddScoped<IRepositorySearch<SaleModel, Sale>, SaleRepository>();
        services.AddScoped<IMapper<SaleRequestDTO, Sale>, SaleMapper>();
        
        // 5. CASOS DE USO (Application Layer)
        services.AddApplicationUseCases();

        return services;
    }

    private static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        // Beer Use Cases
        services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
        services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
        services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
        services.AddScoped<DeleteBeerUseCase>();
        services.AddScoped<UpdateBeerUseCase<BeerRequestDTO>>();

        // User Use Cases
        services.AddScoped<GetUserUseCase<User, UserViewModel>>();
        services.AddScoped<AddUserUseCase<UserRequestDTO>>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<UpdateUserUseCase<UserRequestDTO>>();

        // Post Use Cases
        services.AddScoped<GetPostUseCase>();

        // Sale Use Cases
        services.AddScoped<GenerateSaleUseCase<SaleRequestDTO>>();
        services.AddScoped<GetSalesUseCase>();
        services.AddScoped<DeleteSaleUseCase>();
        services.AddScoped<UpdateSaleUseCase<SaleRequestDTO>>();
        services.AddScoped<GetSalesSearchUseCase<SaleModel>>();

        return services;
    }
}

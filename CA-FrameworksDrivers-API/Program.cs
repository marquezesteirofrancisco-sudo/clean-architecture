using CA_ApplicationLayer;
using CA_FrameworksDrivers_API.Middelwares;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Mappers;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CA_InterfaceAdapters_Models;
using CA_InterfaceAdapters_Presenters;
using CA_InterfaceAdapters_Repository;
using CL_EnterpriseLayer;
using CA_FrameworksDrivers_API.Validator;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi; // Para AddOpenApi
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using FluentValidation;
using FluentValidation.AspNetCore; // Opcional, para personalizar la UI

var builder = WebApplication.CreateBuilder(args);


// Añadimos los validadores
builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>();
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters(); // para windows forms


// DEPENDENCIAS (El "Material") ---
// Aquí es donde registramos los servicios que vamos a usar en nuestra API, como el repositorio y el contexto de la base de datos.


//// Servicio del contexto de la base de datos
var connectionString = builder.Configuration.GetConnectionString("ConexionSQLServer");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information));


//Servicio de la capa de aplicación
// para la entidad Beer
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();

// casos de uso para la entidad Beer
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<DeleteBeerUseCase>();
builder.Services.AddScoped<UpdateBeerUseCase<BeerRequestDTO>>();


// para la entidad User
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IPresenter<User, UserViewModel>, UserPresenter>();
builder.Services.AddScoped<IMapper<UserRequestDTO, User>, UserMapper>();


// CASOS DE USO PARA USER
builder.Services.AddScoped<GetUserUseCase<User, UserViewModel>>();
builder.Services.AddScoped<AddUserUseCase<UserRequestDTO>>();
builder.Services.AddScoped<DeleteUserUseCase>();
builder.Services.AddScoped<UpdateUserUseCase<UserRequestDTO>>();



// --- SERVICIOS (El "Enchufe") ---
// AddOpenApi es la nueva forma de .NET 9
builder.Services.AddOpenApi();

// ESTOS SON LOS QUE TE FALTABAN para que UseSwagger() no explote:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- MIDDLEWARE (El "Aparato") ---
if (app.Environment.IsDevelopment())
{
    // Genera el documento OpenAPI (JSON) en /openapi/v1.json
    app.MapOpenApi();

    // Activa el generador de Swagger y la Interfaz Gráfica
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // IMPORTANTE: Le decimos a Swagger que lea el JSON generado por .NET 9
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API v1");
    });
}


app.UseHttpsRedirection();


// Añado el middelware para controlar las exceptions
app.UseMiddleware<ExceptionMiddelware>();


#region ENDPOINTS CERVEZA


// Crear el grupo para Cervezas
var beerApi = app.MapGroup("/api/beer")
                 .WithTags("Beers")
                 .WithOpenApi();


// END POINT PARA DEVOLVER TODAS LAS CERVEZAS
beerApi.MapGet("/", async (GetBeerUseCase<Beer, BeerViewModel> getBeerUseCase) =>
{
    return await getBeerUseCase.ExecuteAsync();
})
.WithName("API Get Beers")
.WithTags("Beers");


beerApi.MapGet("/details", async (GetBeerUseCase<Beer, BeerDetailViewModel> getBeerUseCase) =>
{
    return await getBeerUseCase.ExecuteAsync();
})
.WithName("API Get Beers Details")
.WithTags("Beers");


// ENDPOINT PARA AGREGAR UNA CERVEZA
beerApi.MapPost("/", async (AddBeerUseCase<BeerRequestDTO> addBeerUseCase, BeerRequestDTO beerRequest
    , IValidator<BeerRequestDTO> validator) =>
{
    var resultValidator = await validator.ValidateAsync(beerRequest);

    if (!resultValidator.IsValid)
    {
        return Results.ValidationProblem(resultValidator.ToDictionary());
    }

    await addBeerUseCase.ExecuteAsync(beerRequest);
    
    return Results.Created("/api/beer", beerRequest);
})
.WithName("API Add Beer")
.WithTags("Beers");


// ENDPOINT PARA ELIMINAR UNA CERVEZA
beerApi.MapDelete("/{id:int}", async (int id, DeleteBeerUseCase deleteBeerUseCase) =>
{
    try
    {
        await deleteBeerUseCase.ExecuteAsync(id);

        return Results.NoContent();
    }
    catch (KeyNotFoundException ex)
    {
        // Transformamos el error interno en un 404 para el cliente
        return Results.NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        // Cualquier otro error sigue siendo un 500
        return Results.Problem("Ocurrió un error inesperado.");
    }
 })
.WithName("API Delete Beer")
.WithTags("Beers");


// ENDPOINT PARA MODIFICAR UNA CERVEZA
beerApi.MapPut("/", async (UpdateBeerUseCase<BeerRequestDTO> updateBeerUseCase, BeerRequestDTO beerRequest) =>
{
    try
    { 
        await updateBeerUseCase.ExecuteAsync(beerRequest);

        return Results.Created("/api/beer", beerRequest);

    }
    catch (KeyNotFoundException ex)
    {
        // Transformamos el error interno en un 404 para el cliente
        return Results.NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        // Cualquier otro error sigue siendo un 500
        return Results.Problem("Ocurrió un error inesperado.");
    }

})
    .WithName("API Update Beer")
    .WithTags("Beers");


#endregion


#region ENDPOINTS USERS


// Crear el grupo para Cervezas
var userApi = app.MapGroup("/api/user")
                 .WithTags("Users")
                 .WithOpenApi();


// END POINT PARA DEVOLVER TODAS LOS USIOS
userApi.MapGet("/", async (GetUserUseCase<User, UserViewModel> getUserUseCase) =>
{
    return await getUserUseCase.ExecuteAsync();
})
.WithName("API Get Users")
.WithTags("Users");


// ENDPOINT PARA AGREGAR UN USUARIO
userApi.MapPost("/", async (AddUserUseCase<UserRequestDTO> addUserUseCase, UserRequestDTO userRequest) =>
{
    await addUserUseCase.ExecuteAsync(userRequest);

    return Results.Created("/api/user", userRequest);
})
.WithName("API Add Users.")
.WithTags("Users");


// ENDPOINT PARA ELIMINAR UN USUARIO
userApi.MapDelete("/{id:int}", async (int id, DeleteUserUseCase deleteUserUseCase) =>
{
    try
    {
        await deleteUserUseCase.ExecuteAsync(id);

        return Results.NoContent();
    }
    catch (KeyNotFoundException ex)
    {
        // Transformamos el error interno en un 404 para el cliente
        return Results.NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        // Cualquier otro error sigue siendo un 500
        return Results.Problem("Ocurrió un error inesperado.");
    }
})
.WithName("API Delete User")
.WithTags("Users");


// ENDPOINT PARA MODIFICAR UN USUARIO
userApi.MapPut("/", async (UpdateUserUseCase<UserRequestDTO> updateUserUseCase, UserRequestDTO userRequest) =>
{
    try
    {
        await updateUserUseCase.ExecuteAsync(userRequest);

        return Results.Created("/api/user", userRequest);

    }
    catch (KeyNotFoundException ex)
    {
        // Transformamos el error interno en un 404 para el cliente
        return Results.NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        // Cualquier otro error sigue siendo un 500
        return Results.Problem("Ocurrió un error inesperado.");
    }

})
    .WithName("API Update User")
    .WithTags("Users");



#endregion

app.Run();
//dotnet new web (nuevo proyecto)
// https://www.nuget.org/

/*
 instalar entity framework
 dotnet add package Microsoft.EntityFrameworkCore --version 7.0.5

 libreria para tener los datos de la base de datos en memoria
 dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 7.0.5

librería para manejar sql server 
 dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.5
  */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

//Data Source=(local); Initial Catalog= TareasDb;Trusted_Connection=True; Integrated Security=True

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext)=>
{
    return Results.Ok(dbContext.Tareas.Include(p=> p.Categoria).Where(p=> p.PrioridadTarea == proyectoef.Models.Prioridad.Baja));
});

app.Run();
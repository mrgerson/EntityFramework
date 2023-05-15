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

builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
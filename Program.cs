using Microsoft.EntityFrameworkCore;
using ModeloParcial1PII.Datos.Models;
using ModeloParcial1PII.Repositorios;
using ModeloParcial1PII.Servicios;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EnviosDBContext>(options =>
        options.UseSqlServer(connectionString));
builder.Services.AddScoped<EFEnvioRepository>();
builder.Services.AddScoped<IEnvioRepository, EFEnvioRepository>();
builder.Services.AddScoped<IEnvioServicio, EnvioServicio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

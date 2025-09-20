using Microsoft.EntityFrameworkCore;
using proyectoPratica01.Datos.Implementaciones;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio.Contexto;
using proyectoPratica01.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProyectoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString
("DefaultConnection")));

builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
builder.Services.AddScoped<ArticuloServicio>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<FacturaServicio>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<PagoServicio>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

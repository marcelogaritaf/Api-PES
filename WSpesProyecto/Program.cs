using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WSpesProyecto.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//para cargar la configuracion desde el appsettings
builder.Configuration.AddJsonFile("appsettings.json");
//para configurar los controladores por medio de la configuracion 
builder.Services.AddDbContext<PesproyectoDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString
        ("SQLchain"));
    });
builder.Services.AddControllers().AddJsonOptions(opt =>// para evitar las referencias ciclicas
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DB configuration
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection"); // Ensure the connection string is not null or empty
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true));
#endregion 

#region Dependency Injection

builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Repository.Interfaces.IDeviceRepository, Fiap.Api.EnvironmentalAlert.Repository.DeviceRepository>();
builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Services.Interfaces.IDeviceService, Fiap.Api.EnvironmentalAlert.Services.DeviceService>();

builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Repository.Interfaces.IDeviceConsumptionRepository, Fiap.Api.EnvironmentalAlert.Repository.DeviceConsumptionRepository>();
builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Services.Interfaces.IDeviceConsumptionService, Fiap.Api.EnvironmentalAlert.Services.DeviceConsumptionService>();

builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Repository.Interfaces.IConsumptionAlertRepository, Fiap.Api.EnvironmentalAlert.Repository.ConsumptionAlertRepository>();
builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Services.Interfaces.IConsumptionAlertService, Fiap.Api.EnvironmentalAlert.Services.ConsumptionAlertService>();

#endregion

#region AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion
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

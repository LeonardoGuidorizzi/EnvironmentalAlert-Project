using Fiap.Api.EnvironmentalAlert.Data.Contexts;
using Fiap.Api.EnvironmentalAlert.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Repository.Interfaces.IUserRepository, Fiap.Api.EnvironmentalAlert.Repository.UserRepository>();
builder.Services.AddScoped<Fiap.Api.EnvironmentalAlert.Services.Interfaces.IUserService, Fiap.Api.EnvironmentalAlert.Services.UserService>();

#endregion

#region AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion
#region Configurações JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
#endregion
#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false, // você pode ajustar isso conforme necessário
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = key
    };
});

builder.Services.AddAuthorization();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

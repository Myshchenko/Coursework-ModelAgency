using ModelAgency_Api.Repositories;
using ModelAgency_Api.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json") // Replace with the path to your configuration file
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IModelService, ModelService>();

builder.Services.AddScoped<IModelRepository, ModelRepository>();

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().
     AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

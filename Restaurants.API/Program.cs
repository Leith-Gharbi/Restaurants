
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Add Serilog 
builder.Host.UseSerilog((context, configuration) => configuration
.MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
.MinimumLevel.Override("Microsoft.EntityFrameworkCore",LogEventLevel.Information)
.WriteTo.Console(outputTemplate : "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}"));

var app = builder.Build();

var scope=app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.


app.UseSerilogRequestLogging(); // middleware serilog

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

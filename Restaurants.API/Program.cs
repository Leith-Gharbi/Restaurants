
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

// Add Swagger Service
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "My API Description",
    });
});


//Register the  Error Handling middleware  as dependency 
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddApplication();

// Add Serilog 
builder.Host.UseSerilog((context, configuration) => 
 configuration
 .ReadFrom.Configuration(context.Configuration)  // this to indicate that serilog parameters are from appseting.json 

 // this when you config serilog in program.cs
        //.MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
        //.MinimumLevel.Override("Microsoft.EntityFrameworkCore",LogEventLevel.Information)
        //.WriteTo.File("Logs/Restaurant-API-.log",rollingInterval:RollingInterval.Day,rollOnFileSizeLimit: true) // create a  Log folder and generate a log file every day 
        //.WriteTo.Console(outputTemplate : "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
 );

var app = builder.Build();

var scope=app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.


app.UseMiddleware<ErrorHandlingMiddleware>(); // add ErrorHandlingMiddle  (1st middleware in the http request pipeline)

app.UseSerilogRequestLogging(); // middleware serilog




if (app.Environment.IsDevelopment())
{

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI(); // Add middleware Swagger 

}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

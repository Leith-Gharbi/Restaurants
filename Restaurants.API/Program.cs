

using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddInfrastructure(builder.Configuration);

builder.AddPresentation();

builder.Services.AddApplication();

var app = builder.Build();

var scope=app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.


app.UseMiddleware<ErrorHandlingMiddleware>(); // add ErrorHandlingMiddle  (1st middleware in the http request pipeline)
app.UseMiddleware<RequestTimeLoggingMiddleware>();// add RequestTimeLoggingMiddleware  
app.UseSerilogRequestLogging(); // middleware serilog




if (app.Environment.IsDevelopment())
{

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI(); // Add middleware Swagger 

}


app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();

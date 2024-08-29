using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder )
        {

            builder.Services.AddAuthentication(); // to accept token passed on header
            builder.Services.AddControllers();
            // Add Swagger Service
            builder.Services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My API Description",
                });

                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type =ReferenceType.SecurityScheme,
                    Id= "bearerAuth"
                }
            },[]
        }

    });
            });


            //Register the  Error Handling middleware  as dependency 
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            //Register the  Request TimeLogging middleware  as dependency 
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

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

        }

    }
}

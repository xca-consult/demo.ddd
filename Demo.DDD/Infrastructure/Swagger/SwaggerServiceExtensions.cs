using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Demo.DDD.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DDD Sample Dotnet", Version = "v1"});

                c.ExampleFilters();
                // c.OperationFilter<AddHeaderOperationFilter>(CorrelationHeader.Name, "Correlation Id for the request", false); // adds any string you like to the request headers - in this case, a correlation id


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                                    Enter 'Bearer' [space] and then your token in the text input below. 
                                    Example: 'Bearer 12345abcdef'. 
                                    Acquire the tokens from here https://security-sales-fsdc-syst.paas-dblan.danskenet.net/Home/Login",
                    Name = "Authorization",
                    
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }
    }
}

  

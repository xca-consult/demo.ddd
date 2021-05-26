using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Demo.DDD.ApplicationServices;
using Demo.DDD.ApplicationServices.QueryServices;
using Demo.DDD.Health;
using Demo.DDD.Integration.Database;
using Demo.DDD.Middleware;
using Demo.DDD.Security;
using Demo.DDD.Swagger;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Demo.DDD
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseSettings = Configuration.GetSection("Database").Get<DatabaseSettings>();
            var connectionString = databaseSettings.GetConnectionString();

            services.AddHttpContextAccessor();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            //todo all correlationid middleware
            
            services.AddHealthChecks()
                .AddCheck("DDD Sample health check", new HealthCheck(connectionString));

            services.AddRepositoryServices(connectionString);
            services.AddDomainServices();

            services.AddScoped<IUserDetailsQueryService, UserDetailsQueryService>();

            services.ConfigureSwagger();

            services.ConfigureSecurity();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                        {new OpenApiServer {Url = $"https://{httpReq.Host.Value}"}};
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD Sample Dotnet");
            });

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();
 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapMetrics();
            });

            var databaseSettings = Configuration.GetSection("Migration").Get<DatabaseSettings>();
            DbMigrator.Migrate(databaseSettings.GetConnectionString());
        }
    }
}
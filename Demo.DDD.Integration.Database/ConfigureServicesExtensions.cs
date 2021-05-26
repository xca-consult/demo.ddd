using Integration.Database;
using Microsoft.Extensions.DependencyInjection;
using Demo.DDD.Domain;

namespace Demo.DDD.Integration.Database
{
    public static class ConfigureServicesExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(new UserRepository(connectionString));
            services.AddSingleton<IReaderRepository<User>>(x => x.GetRequiredService<UserRepository>()); // Forwarding
            services.AddSingleton<IWriteRepository<User>>(x => x.GetRequiredService<UserRepository>()); // Forwarding
            services.AddSingleton<IReaderWriterRepository<User>>(x => x.GetRequiredService<UserRepository>()); // Forwarding
        }
    }
}
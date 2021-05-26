using System;
using Microsoft.Extensions.Configuration;

namespace Demo.DDD.Integration.Database
{
    public static class Program
    {
        public static int Main(string[] args)
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var databaseSettings = config.GetSection("Migration").Get<DatabaseSettings>();
            var connectionString = databaseSettings.GetConnectionString();

            var result = DbMigrator.Migrate(connectionString);

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;

        }

       
    }
}

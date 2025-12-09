using Bejibe.Kosmos.Domain.Data;
using Kosmos.Common.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var databasesOptions = configurationManager.Get<DatabasesOptions>();

            var dbOption = databasesOptions?.Databases.FirstOrDefault(x => x.Key == "Default");
            var dbOptionValue = dbOption.GetValueOrDefault().Value;

            if (dbOption is null || dbOptionValue is null)
                return; // throw ex ?

            // building the connection string
            string connectionString = $"Server={dbOptionValue.Host};;Database={dbOptionValue.Database};User Id={dbOptionValue.User};Password={dbOptionValue.Password};";
            services.AddDbContext<DatabaseContext>((provider, options) =>
            {
                options.UseSqlServer(connectionString);
            });

        }
    }
}

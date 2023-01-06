using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.App.Configurations
{
    public static class RepositoryConfig
    {
        public static IServiceCollection AddRepositoryConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}

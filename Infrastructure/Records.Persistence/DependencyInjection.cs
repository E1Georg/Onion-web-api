using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Records.Application.Interfaces;


namespace Records.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<RecordsDbContext>(options => {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IValuesDbContext>(provider => provider.GetService<RecordsDbContext>());
            services.AddScoped<IResultsDbContext>(provider => provider.GetService<RecordsDbContext>());        

            return services;
        }
    }
}

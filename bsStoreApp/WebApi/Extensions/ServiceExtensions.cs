using Microsoft.EntityFrameworkCore;
using Repositories.Contacts;
using Repositories.EfCore;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        
            services.AddDbContext<RepositoryContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("Sqlite"));
            });
        
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}

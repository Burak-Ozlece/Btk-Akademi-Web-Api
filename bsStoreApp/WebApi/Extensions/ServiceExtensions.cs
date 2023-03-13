using Microsoft.EntityFrameworkCore;
using Repositories.EfCore;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }
    }
}

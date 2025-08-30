using AnimeHub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AnimeHub.Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static void AddDbContextConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AnimeHubContext>(options => options.UseSqlServer(connectionString));
        }
    }
}

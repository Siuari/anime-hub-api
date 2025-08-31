using AnimeHub.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace AnimeHub.Infra.Data.Context
{
    public class AnimeHubContext : DbContext
    {
        public AnimeHubContext(DbContextOptions<AnimeHubContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnimeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

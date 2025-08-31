using AnimeHub.Domain.Interfaces;
using AnimeHub.Infra.Data.Context;
using AnimeHub.Infra.Data.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeHub.Infra.IoC
{
    public static class Provider
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AnimeHubContext>();

            services.AddScoped<IAnimeRepositorio, AnimeRepositorio>();
        }
    }
}

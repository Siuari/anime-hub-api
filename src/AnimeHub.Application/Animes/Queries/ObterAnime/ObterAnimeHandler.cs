using AnimeHub.Domain.Interfaces;
using MediatR;

namespace AnimeHub.Application.Animes.Queries.ObterAnime
{
    public class ObterAnimeHandler : IRequestHandler<ObterAnimeQuery, ObterAnimeResponse>
    {
        private readonly IAnimeRepositorio _animeRepositorio;

        public ObterAnimeHandler(IAnimeRepositorio animeRepositorio)
        {
            _animeRepositorio = animeRepositorio;
        }

        public async Task<ObterAnimeResponse> Handle(ObterAnimeQuery request, CancellationToken cancellationToken)
        {
            var anime = await _animeRepositorio.ObterPorIdAsync(request.Id, cancellationToken);

            if (anime is null)
                return default;

            return new(anime.Id, anime.Nome, anime.Diretor, anime.Resumo);
        }
    }
}

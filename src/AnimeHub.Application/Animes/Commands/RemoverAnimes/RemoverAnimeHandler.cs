using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Interfaces;
using MediatR;

namespace AnimeHub.Application.Animes.Commands.RemoverAnimes
{
    public class RemoverAnimeHandler : IRequestHandler<RemoverAnimeCommand, RemoverAnimeResponse>
    {
        private readonly IAnimeRepositorio _animeRepositorio;

        public RemoverAnimeHandler(IAnimeRepositorio animeRepositorio)
        {
            _animeRepositorio = animeRepositorio;
        }

        public async Task<RemoverAnimeResponse> Handle(RemoverAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = await _animeRepositorio.ObterPorIdAsync(request.Id, cancellationToken);

            if (anime is null)
                throw new AnimeHubValidationException("Anime não encontrado.");

            _animeRepositorio.Remover(anime);

            await _animeRepositorio.SaveChangesAsync(cancellationToken);

            return new(anime.Id);
        }
    }
}

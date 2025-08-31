using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Interfaces;
using MediatR;

namespace AnimeHub.Application.Animes.Commands.AtualizarAnimes
{
    public class AtualizarAnimeHandler : IRequestHandler<AtualizarAnimeCommand, AtualizarAnimeResponse>
    {
        private readonly IAnimeRepositorio _animeRepositorio;

        public AtualizarAnimeHandler(IAnimeRepositorio animeRepositorio)
        {
            _animeRepositorio = animeRepositorio;
        }

        public async Task<AtualizarAnimeResponse> Handle(AtualizarAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime = await _animeRepositorio.ObterPorIdAsync(request.Id, cancellationToken);

            if (anime is null)
                throw new AnimeHubValidationException($"Anime com ID {request.Id} não encontrado.");

            anime.Atualizar(request.Nome, request.Diretor, request.Resumo);

            await _animeRepositorio.SaveChangesAsync(cancellationToken);

            return new(anime.Id, anime.Nome, anime.Diretor, anime.Resumo);
        }
    }
}

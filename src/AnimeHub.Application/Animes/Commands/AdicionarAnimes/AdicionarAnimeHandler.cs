using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Interfaces;
using MediatR;

namespace AnimeHub.Application.Animes.Commands.AdicionarAnimes
{
    public class AdicionarAnimeHandler : IRequestHandler<AdicionarAnimeCommand, AdicionarAnimeResponse>
    {
        public readonly IAnimeRepositorio _repositorio;

        public AdicionarAnimeHandler(IAnimeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<AdicionarAnimeResponse> Handle(AdicionarAnimeCommand request, CancellationToken cancellationToken)
        {
            if (!RequisicaoValida(request))
                throw new AnimeHubValidationException("Requisição inválida. Deve ser informado Nome, Diretor e Resumo.");

            var anime = new Anime(request.Nome, request.Diretor, request.Resumo);

            await _repositorio.AdicionarAsync(anime, cancellationToken);

            await _repositorio.SaveChangesAsync(cancellationToken);

            return new(anime.Id);
        }

        private bool RequisicaoValida(AdicionarAnimeCommand request)
        {
            return !string.IsNullOrWhiteSpace(request.Nome)
                && !string.IsNullOrWhiteSpace(request.Diretor)
                && !string.IsNullOrWhiteSpace(request.Resumo);
        }
    }
}

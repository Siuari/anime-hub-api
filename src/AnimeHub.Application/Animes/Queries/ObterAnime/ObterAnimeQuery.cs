using MediatR;

namespace AnimeHub.Application.Animes.Queries.ObterAnime
{
    public class ObterAnimeQuery : IRequest<ObterAnimeResponse>
    {
        public Guid Id { get; set; }
    }

    public sealed record ObterAnimeResponse(Guid id, string nome, string diretor, string resumo);
}

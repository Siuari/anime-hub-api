using MediatR;

namespace AnimeHub.Application.Animes.Commands.AdicionarAnimes
{
    public class AdicionarAnimeCommand : IRequest<AdicionarAnimeResponse>
    {
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Resumo { get; set; }
    }

    public sealed record AdicionarAnimeResponse(Guid id);
}

using MediatR;

namespace AnimeHub.Application.Animes.Commands.AtualizarAnimes
{
    public class AtualizarAnimeCommand : IRequest<AtualizarAnimeResponse>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string Resumo { get; set; }
    }

    public sealed record AtualizarAnimeResponse(Guid id, string nome, string diretor, string resumo);
}

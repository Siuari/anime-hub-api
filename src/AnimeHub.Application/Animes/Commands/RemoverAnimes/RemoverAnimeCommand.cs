using MediatR;

namespace AnimeHub.Application.Animes.Commands.RemoverAnimes
{
    public class RemoverAnimeCommand : IRequest<RemoverAnimeResponse>
    {
        public Guid Id { get; set; }
    }

    public sealed record RemoverAnimeResponse(Guid id);
}

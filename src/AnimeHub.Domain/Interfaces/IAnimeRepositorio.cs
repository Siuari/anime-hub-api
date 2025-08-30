using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Specifications;

namespace AnimeHub.Domain.Interfaces
{
    public interface IAnimeRepositorio
    {
        Task AdicionarAsync(Anime anime, CancellationToken cancellarionToken);
        Task<Anime> ObterPorIdAsync(Guid id, CancellationToken cancellarionToken);
        Task<(int? totalItens, IReadOnlyList<Anime> animes)> ListarAsync(
            ISpecification<Anime> specification,
            CancellationToken cancellationToken);

        void Remover(Anime anime);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}

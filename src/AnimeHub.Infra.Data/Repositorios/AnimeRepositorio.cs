using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Interfaces;
using AnimeHub.Domain.Specifications;
using AnimeHub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AnimeHub.Infra.Data.Repositorios
{
    public class AnimeRepositorio : IAnimeRepositorio
    {
        private readonly AnimeHubContext _context;

        public AnimeRepositorio(AnimeHubContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Anime anime, CancellationToken cancellationToken)
            => await _context.AddAsync(anime, cancellationToken);

        public async Task<Anime> ObterPorIdAsync(Guid id, CancellationToken cancellarionToken)
            => await _context.Set<Anime>()
                .FindAsync([id], cancellarionToken);

        public async Task<(int? totalItens, IReadOnlyList<Anime> animes)> ListarAsync(
            ISpecification<Anime> specification, 
            CancellationToken cancellationToken)
        {
            var query = _context.Set<Anime>().AsNoTracking();
            var totalItens = null as int?;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.Skip.HasValue && specification.Take.HasValue)
            {
                totalItens = await query.CountAsync(cancellationToken);
                
                query = query.OrderBy(x => x.Nome)
                    .Skip(specification.Skip.Value)
                    .Take(specification.Take.Value);
            }

            var animes = await query.ToListAsync(cancellationToken);

            return (totalItens, animes);
        }

        public void Remover(Anime anime)
            => _context.Remove(anime);

        public async Task SaveChangesAsync(CancellationToken cancellationToken) 
            => await _context.SaveChangesAsync(cancellationToken);
    }
}

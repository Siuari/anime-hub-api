using AnimeHub.Application.Animes.Dtos;
using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Specifications;
using System.Linq.Expressions;

namespace AnimeHub.Application.Animes.Queries.ListarAnimes
{
    internal class ListagemAnimesSpecification : ISpecification<Anime>
    {
        public ListagemAnimesSpecification(FiltroAnimesDto filtro)
        {
            Skip = (filtro.Pagina - 1) * filtro.TamanhoPagina;
            Take = filtro.TamanhoPagina;
            Criteria = anime =>
                (!filtro.Id.HasValue || anime.Id == filtro.Id.Value)
                && (string.IsNullOrWhiteSpace(filtro.Nome) || anime.Nome.ToUpper().Contains(filtro.Nome.ToUpper()))
                && (string.IsNullOrWhiteSpace(filtro.Diretor) || anime.Diretor.ToUpper().Contains(filtro.Diretor.ToUpper()));
        }

        public Expression<Func<Anime, bool>> Criteria { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

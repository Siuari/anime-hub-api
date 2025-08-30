using AnimeHub.Application.Common;
using AnimeHub.Domain.Interfaces;
using MediatR;

namespace AnimeHub.Application.Animes.Queries.ListarAnimes
{
    public class ListagemAnimesHandler : IRequestHandler<ListagemAnimesQuery, ResultadoPaginado<ListagemAnimesResponse>>
    {
        public readonly IAnimeRepositorio _animeRepositorio;

        public ListagemAnimesHandler(IAnimeRepositorio animeRepositorio)
        {
            _animeRepositorio = animeRepositorio;
        }

        public async Task<ResultadoPaginado<ListagemAnimesResponse>> Handle(ListagemAnimesQuery request, CancellationToken cancellationToken)
        {
            var specification = new ListagemAnimesSpecification(request.Filtro);

            var (totalItens, animes) = await _animeRepositorio.ListarAsync(specification, cancellationToken);

            return new ResultadoPaginado<ListagemAnimesResponse>
            {
                TotalItens = totalItens.Value,
                Pagina = request.Filtro.Pagina.Value,
                TamanhoPagina = request.Filtro.TamanhoPagina.Value,
                Itens = animes.Select(a => new ListagemAnimesResponse(a.Id, a.Nome, a.Diretor, a.Resumo))
                    .ToList()
            };
        }
    }
}

using AnimeHub.Application.Animes.Dtos;
using AnimeHub.Application.Common;
using MediatR;

namespace AnimeHub.Application.Animes.Queries.ListarAnimes
{
    public class ListagemAnimesQuery : IRequest<ResultadoPaginado<ListagemAnimesResponse>>
    {
        public FiltroAnimesDto Filtro { get; set; }
    }

    public sealed record ListagemAnimesResponse(Guid id, string nome, string diretor, string resumo);
}

using AnimeHub.Application.Animes.Commands.AdicionarAnimes;
using AnimeHub.Application.Animes.Commands.AtualizarAnimes;
using AnimeHub.Application.Animes.Commands.RemoverAnimes;
using AnimeHub.Application.Animes.Dtos;
using AnimeHub.Application.Animes.Queries.ListarAnimes;
using AnimeHub.Application.Animes.Queries.ObterAnime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnimesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = "ObterAnime")]
        public async Task<IActionResult> ObterAsync(
            [FromRoute] Guid id, CancellationToken cancellationToken) 
            => Ok(await _mediator.Send(new ObterAnimeQuery { Id = id }, cancellationToken));

        [HttpGet]
        public async Task<IActionResult> GetAllAnimes(
            [FromQuery] FiltroAnimesDto filtro,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new ListagemAnimesQuery { Filtro = filtro }, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync(
            [FromBody] AdicionarAnimeCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtRoute("ObterAnime", new { id = result.id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarAsync(
            [FromBody] AtualizarAnimeCommand command,
            CancellationToken cancellationToken) 
            => Ok(await _mediator.Send(command, cancellationToken));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoverAnimeCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}

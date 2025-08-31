namespace AnimeHub.Application.Animes.Dtos
{
    public class FiltroAnimesDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Diretor { get; set; }
        public int? Pagina { get; set; } = 1;
        public int? TamanhoPagina { get; set; } = 10;
    }
}

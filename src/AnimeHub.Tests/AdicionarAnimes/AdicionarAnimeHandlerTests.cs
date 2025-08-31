using AnimeHub.Application.Animes.Commands.AdicionarAnimes;
using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Interfaces;
using AutoFixture;
using Moq;

namespace AnimeHub.Tests.AdicionarAnimes
{
    public class AdicionarAnimeHandlerTests
    {
        private Mock<IAnimeRepositorio> AnimeRepositorioMock { get; } = new();
        private Fixture Fixture { get; } = new();

        [Fact]
        public async Task QuandoRequisicaoValida_DeveAdicionarAnimeComSucesso()
        {
            // Arrange
            var handler = new AdicionarAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Create<AdicionarAnimeCommand>();

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            AnimeRepositorioMock.Verify(r => 
                r.AdicionarAsync(
                    It.Is<Anime>(x => x.Nome == command.Nome
                        && x.Diretor == command.Diretor
                        && x.Resumo == command.Resumo)
                    , It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task QuandoRequisicaoInvalida_DeveLancarException()
        {
            // Arrange
            var handler = new AdicionarAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Build<AdicionarAnimeCommand>()
                .With(c => c.Nome, string.Empty)
                .Create();

            // Act & Assert
            await Assert.ThrowsAsync<AnimeHubValidationException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

    }
}

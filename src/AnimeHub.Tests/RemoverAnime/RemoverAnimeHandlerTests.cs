using AnimeHub.Application.Animes.Commands.RemoverAnimes;
using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Interfaces;
using AutoFixture;
using Moq;

namespace AnimeHub.Tests.RemoverAnime
{
    public class RemoverAnimeHandlerTests
    {
        private Mock<IAnimeRepositorio> AnimeRepositorioMock { get; } = new();
        private Fixture Fixture { get; } = new();

        [Fact]
        public async Task QuandoAnimeExiste_DeveRemoverAnimeComSucesso()
        {
            // Arrange
            var handler = new RemoverAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Create<RemoverAnimeCommand>();
            var animeExistente = Fixture.Create<Anime>();

            AnimeRepositorioMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(animeExistente);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            AnimeRepositorioMock.Verify(r => r.Remover(animeExistente));
            AnimeRepositorioMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task QuandoAnimeNaoExiste_DeveLancarException()
        {
            // Arrange
            var handler = new RemoverAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Create<RemoverAnimeCommand>();

            // Act & Assert
            await Assert.ThrowsAsync<AnimeHubValidationException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}

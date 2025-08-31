using AnimeHub.Application.Animes.Commands.AtualizarAnimes;
using AnimeHub.Domain.DomainExceptions;
using AnimeHub.Domain.Entidades;
using AnimeHub.Domain.Interfaces;
using AutoFixture;
using FluentAssertions;
using Moq;

namespace AnimeHub.Tests.AtualizarAnimes
{
    public class AtualizarAnimeHandlerTests
    {
        private Mock<IAnimeRepositorio> AnimeRepositorioMock { get; } = new();
        private Fixture Fixture { get; } = new();

        [Fact]
        public async Task QuandoRequisicaoValida_DeveAtualizarAnimeComSucesso()
        {
            // Arrange
            var handler = new AtualizarAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Create<AtualizarAnimeCommand>();
            var animeExistente = new Anime("Nome", "Diretor", "Resumo");

            AnimeRepositorioMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(animeExistente);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            AnimeRepositorioMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));

            animeExistente.Nome.Should().Be(command.Nome);
            animeExistente.Diretor.Should().Be(command.Diretor);
            animeExistente.Resumo.Should().Be(command.Resumo);
        }

        [Fact]
        public async Task QuandoAnimeNaoExiste_DeveLancarException()
        {
            // Arrange
            var handler = new AtualizarAnimeHandler(AnimeRepositorioMock.Object);
            var command = Fixture.Create<AtualizarAnimeCommand>();

            // Act & Assert
            await Assert.ThrowsAsync<AnimeHubValidationException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}

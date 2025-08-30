using AnimeHub.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeHub.Infra.Data.Maps
{
    internal class AnimeMap : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.Nome);
            builder.Property(x => x.Diretor);
            builder.Property(x => x.Resumo);

            builder.ToTable(nameof(Anime));
        }
    }
}

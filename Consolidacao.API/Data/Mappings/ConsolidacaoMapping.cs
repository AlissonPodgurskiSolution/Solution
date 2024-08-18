using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consolidacao.API.Data.Mappings;

public class ConsolidacaoMapping : IEntityTypeConfiguration<Models.Consolidacao>
{
    public void Configure(EntityTypeBuilder<Models.Consolidacao> builder)
    {
        // Configuração da chave primária
        builder.HasKey(c => c.Id);

        // Configuração das propriedades
        builder.Property(c => c.Data)
            .IsRequired();

        builder.Property(c => c.Saldo)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.TotalCreditos)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.TotalDebitos)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.QuantidadeLancamentos)
            .IsRequired();

        // Mapeamento da tabela
        builder.ToTable("Consolidacoes");
    }
}
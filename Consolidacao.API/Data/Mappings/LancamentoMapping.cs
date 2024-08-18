using Consolidacao.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consolidacao.API.Data.Mappings;

public class LancamentoMapping : IEntityTypeConfiguration<LancamentoConsolidacao>
{
    public void Configure(EntityTypeBuilder<LancamentoConsolidacao> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Data)
            .IsRequired();

        builder.Property(c => c.LancamentoId)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(c => c.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.Descricao)
            .HasMaxLength(500);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.UpdatedAt)
            .HasColumnType("datetime");

        builder.Property(c => c.UpdatedBy)
            .HasMaxLength(100);

        builder.ToTable("LancamentoConsolidacao");
    }
}
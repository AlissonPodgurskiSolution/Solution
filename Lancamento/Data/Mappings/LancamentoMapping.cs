using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lancamento.API.Data.Mappings
{
    public class LancamentoMapping : IEntityTypeConfiguration<Models.Lancamento>
    {
        public void Configure(EntityTypeBuilder<Models.Lancamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Descricao)
                .HasMaxLength(500);

            // Campos de Auditoria
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

            builder.ToTable("Lancamentos");
        }
    }
}
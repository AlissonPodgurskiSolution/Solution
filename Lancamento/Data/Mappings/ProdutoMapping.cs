using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lancamento.API.Data.Mappings
{
    public class LancamentoMapping : IEntityTypeConfiguration<Lancamento.API.Models.Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento.API.Models.Lancamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(c => c.Valor)
                .IsRequired();

            builder.Property(c => c.Descricao);

        }
    }
}
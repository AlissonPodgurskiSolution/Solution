using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;

namespace Lancamento.API.Data
{
    public class LancamentoContext : DbContext, IUnitOfWork
    {
        public LancamentoContext(DbContextOptions<LancamentoContext> options)
            : base(options) { }

        public DbSet<Models.Lancamento> Lancamentos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LancamentoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
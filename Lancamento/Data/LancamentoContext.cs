using FluentValidation.Results;
using Lancamento.API.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;

namespace Lancamento.API.Data;

public class LancamentoContext : DbContext, IUnitOfWork
{
    public LancamentoContext(DbContextOptions<LancamentoContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Lancamento> Lancamentos { get; set; }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LancamentoContext).Assembly);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedInfo(DateTime.UtcNow, "System"); 
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdatedInfo(DateTime.UtcNow, "System");
            }
        }

        return base.SaveChanges();
    }
}
using Consolidacao.API.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;

namespace Consolidacao.API.Data;

public sealed class ConsolidacaoContext : DbContext, IUnitOfWork
{
    public ConsolidacaoContext(DbContextOptions<ConsolidacaoContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Models.Consolidacao> Consolidacoes { get; set; }
    public DbSet<LancamentoConsolidacao> LancamentosConsolidacao { get; set; }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsolidacaoContext).Assembly);
    }
}
using Consolidacao.API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Consolidacao.API.Data.Repository;

public class ConsolidacaoRepository : IConsolidacaoRepository
{
    private readonly ConsolidacaoContext _context;

    public ConsolidacaoRepository(ConsolidacaoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Models.Consolidacao> ObterPorData(DateTime data)
    {
        return await _context.Consolidacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Data.Date == data.Date);
    }

    public void Adicionar(Models.Consolidacao consolidacao)
    {
        _context.Consolidacoes.Add(consolidacao);
    }

    public void Atualizar(Models.Consolidacao consolidacao)
    {
        _context.Consolidacoes.Update(consolidacao);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<bool> Commit()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
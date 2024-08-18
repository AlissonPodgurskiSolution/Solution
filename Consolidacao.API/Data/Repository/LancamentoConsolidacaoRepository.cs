using Consolidacao.API.Models;
using Consolidacao.API.Models.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Consolidacao.API.Data.Repository;

public class LancamentoConsolidacaoRepository : ILancamentoConsolidacaoRepository
{
    private readonly ConsolidacaoContext _context;

    public LancamentoConsolidacaoRepository(ConsolidacaoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<LancamentoConsolidacao> ObterPorId(Guid id)
    {
        return await _context.LancamentosConsolidacao.FindAsync(id);
    }

    public async Task<List<LancamentoConsolidacao>> ObterLancamentosPorId(string ids)
    {
        var idsGuid = ids.Split(',')
            .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

        if (!idsGuid.All(nid => nid.Ok)) return new List<LancamentoConsolidacao>();

        var idsValue = idsGuid.Select(id => id.Value);

        return await _context.LancamentosConsolidacao.AsNoTracking()
            .Where(l => idsValue.Contains(l.Id)).ToListAsync();
    }

    public void Adicionar(LancamentoConsolidacao lancamento)
    {
        _context.LancamentosConsolidacao.Add(lancamento);
        _context.Commit();
    }

    public void Atualizar(LancamentoConsolidacao lancamento)
    {
        _context.LancamentosConsolidacao.Update(lancamento);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<List<LancamentoConsolidacao>> ObterLancamentosPorData(DateTime data)
    {
        return await _context.LancamentosConsolidacao
            .AsNoTracking()
            .Where(l => l.Data.Date == data.Date)
            .OrderBy(l => l.Data)
            .ToListAsync();
    }

    public async Task<PagedResult<LancamentoConsolidacao>> ObterTodos(int pageSize, int pageIndex, string query = null)
    {
        var queryable = _context.LancamentosConsolidacao.AsQueryable();

        if (!string.IsNullOrEmpty(query)) queryable = queryable.Where(l => l.Descricao.Contains(query));

        var total = await queryable.CountAsync();

        var lancamentos = await queryable
            .OrderBy(l => l.Data)
            .Skip(pageSize * (pageIndex - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<LancamentoConsolidacao>
        {
            List = lancamentos,
            TotalResults = total,
            PageIndex = pageIndex,
            PageSize = pageSize,
            Query = query
        };
    }
}
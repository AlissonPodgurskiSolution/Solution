using Lancamento.API.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Lancamento.API.Data.Repository;

public class LancamentoRepository : ILancamentoRepository
{
    private readonly LancamentoContext _context;

    public LancamentoRepository(LancamentoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<PagedResult<Models.Lancamento>> ObterTodos(int pageSize, int pageIndex, string query = null)
    {
        var queryable = _context.Lancamentos.AsQueryable();

        if (!string.IsNullOrEmpty(query)) queryable = queryable.Where(l => l.Descricao.Contains(query));

        var total = await queryable.CountAsync();

        var lancamentos = await queryable
            .OrderBy(l => l.Data)
            .Skip(pageSize * (pageIndex - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Models.Lancamento>
        {
            List = lancamentos,
            TotalResults = total,
            PageIndex = pageIndex,
            PageSize = pageSize,
            Query = query
        };
    }

    public async Task<Models.Lancamento> ObterPorId(Guid id)
    {
        return await _context.Lancamentos.FindAsync(id);
    }

    public async Task<List<Models.Lancamento>> ObterLancamentosPorId(string ids)
    {
        var idsGuid = ids.Split(',')
            .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

        if (!idsGuid.All(nid => nid.Ok)) return new List<Models.Lancamento>();

        var idsValue = idsGuid.Select(id => id.Value);

        return await _context.Lancamentos.AsNoTracking()
            .Where(l => idsValue.Contains(l.Id)).ToListAsync();
    }

    public void Adicionar(Models.Lancamento lancamento)
    {
        _context.Lancamentos.Add(lancamento);
        _context.Commit();
    }

    public void Atualizar(Models.Lancamento lancamento)
    {
        _context.Lancamentos.Update(lancamento);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
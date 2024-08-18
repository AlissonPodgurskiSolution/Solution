using NetDevPack.Data;

namespace Lancamento.API.Models;

public interface ILancamentoRepository : IRepository<Lancamento>
{
    Task<PagedResult<Lancamento>> ObterTodos(int pageSize, int pageIndex, string query = null);
    Task<Lancamento> ObterPorId(Guid id);
    Task<List<Lancamento>> ObterLancamentosPorId(string ids);
    void Adicionar(Lancamento lancamento);
    void Atualizar(Lancamento lancamento);
}
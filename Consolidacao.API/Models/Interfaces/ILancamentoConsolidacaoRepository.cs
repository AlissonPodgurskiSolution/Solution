using NetDevPack.Data;

namespace Consolidacao.API.Models.Interfaces;

public interface ILancamentoConsolidacaoRepository : IRepository<LancamentoConsolidacao>
{
    Task<LancamentoConsolidacao> ObterPorId(Guid id);
    Task<List<LancamentoConsolidacao>> ObterLancamentosPorId(string ids);
    void Adicionar(LancamentoConsolidacao lancamentoConsolidacao);
    void Atualizar(LancamentoConsolidacao lancamentoConsolidacao);
    Task<List<LancamentoConsolidacao>> ObterLancamentosPorData(DateTime data);
}
using NetDevPack.Data;

namespace Consolidacao.API.Models.Interfaces;

public interface IConsolidacaoRepository : IRepository<Consolidacao>
{
    Task<Consolidacao> ObterPorData(DateTime data);
    void Adicionar(Consolidacao consolidacao);
    void Atualizar(Consolidacao consolidacao);
}
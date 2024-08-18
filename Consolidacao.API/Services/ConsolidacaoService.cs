using Consolidacao.API.Models.Interfaces;
using Consolidacao.API.Services.Interfaces;
using Core.Enumerators;

namespace Consolidacao.API.Services;

public class ConsolidacaoService : IConsolidacaoService
{
    private readonly IConsolidacaoRepository _consolidacaoRepository;
    private readonly ILancamentoConsolidacaoRepository _lancamentoRepository;

    public ConsolidacaoService(ILancamentoConsolidacaoRepository lancamentoRepository,
        IConsolidacaoRepository consolidacaoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
        _consolidacaoRepository = consolidacaoRepository;
    }

    public async Task ConsolidarDia(DateTime data)
    {
        // Obtém todos os lançamentos do dia
        var lancamentos = await _lancamentoRepository.ObterLancamentosPorData(data);

        // Calcula os totais de créditos e débitos
        var totalCreditos = lancamentos.Where(l => l.Tipo == TipoLancamento.Credito).Sum(l => l.Valor);
        var totalDebitos = lancamentos.Where(l => l.Tipo == TipoLancamento.Debito).Sum(l => l.Valor);
        var saldo = totalCreditos - totalDebitos;
        var quantidadeLancamentos = lancamentos.Count();

        // Verifica se já existe uma consolidação para o dia
        var consolidacaoExistente = await _consolidacaoRepository.ObterPorData(data);

        if (consolidacaoExistente == null)
        {
            // Cria uma nova consolidação se não existir
            var consolidacao =
                new Models.Consolidacao(data, saldo, totalCreditos, totalDebitos,
                    quantidadeLancamentos);
            _consolidacaoRepository.Adicionar(consolidacao);
        }
        else
        {
            // Atualiza a consolidação existente
            consolidacaoExistente.Atualizar(saldo, totalCreditos, totalDebitos, quantidadeLancamentos);
            _consolidacaoRepository.Atualizar(consolidacaoExistente);
        }

        // Persiste as mudanças
        await _consolidacaoRepository.UnitOfWork.Commit();
    }
}
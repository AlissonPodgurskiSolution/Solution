using Core.Enumerators;
using NetDevPack.Messaging;

namespace Consolidacao.API.Application.Comands;

public class LancamentoConsolidacaoCommand : Command
{
    public LancamentoConsolidacaoCommand(DateTime data, TipoLancamento tipo, decimal valor, string descricao,
        Guid lancamentoId)
    {
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
        LancamentoId = lancamentoId;
    }

    public DateTime Data { get; set; }
    public TipoLancamento Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public Guid LancamentoId { get; set; }
}
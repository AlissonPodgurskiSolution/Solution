using Core.Enumerators;
using Lancamento.API.Models;

namespace Gateway.API.Models;

public record LancamentoRequest
{
    public DateTime Data { get; set; }
    public TipoLancamento Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
}
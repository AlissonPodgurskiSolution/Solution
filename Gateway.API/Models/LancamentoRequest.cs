using Lancamento.API.Models;

public record LancamentoRequest
{
    public Guid Id { get;  set; }
    public DateTime Data { get;  set; }
    public TipoLancamento Tipo { get;  set; }
    public decimal Valor { get;  set; }
    public string Descricao { get;  set; }
}
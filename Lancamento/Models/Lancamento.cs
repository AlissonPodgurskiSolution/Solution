using NetDevPack.Domain;

namespace Lancamento.API.Models;

public class Lancamento : Entity, IAggregateRoot
{
    public Lancamento(DateTime data, TipoLancamento tipo, decimal valor, string descricao = null)
    {
        Id = Guid.NewGuid();
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
    }

    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }
    public TipoLancamento Tipo { get; private set; }
    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }

    public void AtualizarLancamento(DateTime data, TipoLancamento tipo, decimal valor, string descricao = null)
    {
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
    }

    public bool EhValido()
    {
        return Valor > 0 && Data != default;
    }
}
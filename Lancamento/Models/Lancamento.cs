using System.Text.Json.Serialization;
using NetDevPack.Domain;

namespace Lancamento.API.Models;

public class Lancamento : AuditableEntity, IAggregateRoot
{
    public Lancamento(DateTime data, TipoLancamento tipo, decimal valor, string descricao = null)
    {
        Id = Guid.NewGuid();
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;

        SetCreatedInfo(DateTime.UtcNow, "System");
    }

    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoLancamento Tipo { get; private set; }

    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }

    public void AtualizarLancamento(DateTime data, TipoLancamento tipo, decimal valor, string descricao = null)
    {
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;

        SetUpdatedInfo(DateTime.UtcNow, "System");
    }

    public bool EhValido()
    {
        return Valor > 0 && Data != default;
    }
}
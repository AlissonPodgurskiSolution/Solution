using System.Text.Json.Serialization;
using Core.Enumerators;
using Core.Models;
using NetDevPack.Domain;

namespace Consolidacao.API.Models;

public class LancamentoConsolidacao : AuditableEntity, IAggregateRoot
{
    public LancamentoConsolidacao(DateTime data, TipoLancamento tipo, decimal valor, string descricao = null,
        Guid lancamentoId = default)
    {
        Id = Guid.NewGuid();
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;

        SetCreatedInfo(DateTime.UtcNow, "System");
        LancamentoId = lancamentoId;
    }

    public Guid Id { get; private set; }
    public Guid LancamentoId { get; private set; }
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
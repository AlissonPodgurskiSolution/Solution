using System.Text.Json.Serialization;
using Core.Enumerators;

namespace Core.Messages.Integration;

public class AdicionarParaConsolidacaoEvent : IntegrationEvent
{
    public AdicionarParaConsolidacaoEvent(Guid id, DateTime data, TipoLancamento tipo, decimal valor, string descricao)
    {
        AggregateId = id;
        Id = id;
        Data = data;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
    }

    public Guid Id { get; private set; }
    public DateTime Data { get; private set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoLancamento Tipo { get; private set; }

    public decimal Valor { get; private set; }
    public string Descricao { get; private set; }
}
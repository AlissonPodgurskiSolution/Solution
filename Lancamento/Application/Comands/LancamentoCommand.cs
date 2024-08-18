using Core.Enumerators;
using Lancamento.API.Models;
using NetDevPack.Messaging;

namespace Lancamento.API.Application.Comands;

public class LancamentoCommand : Command
{
    public DateTime Data { get; set; }
    public TipoLancamento Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
}
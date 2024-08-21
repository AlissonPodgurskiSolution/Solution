using Core.Communication;
using Gateway.API.Models;

namespace Gateway.API.Services.Interfaces;

public interface IConsolidacaoService
{
    Task<Consolidacao> ObterConsolidacaoDoDia();
}
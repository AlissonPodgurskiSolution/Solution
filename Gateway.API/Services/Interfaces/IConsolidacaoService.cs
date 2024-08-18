using Core.Communication;

namespace Gateway.API.Services.Interfaces;

public interface IConsolidacaoService
{
    Task<ResponseResult> ObterConsolidacaoDoDia();
}
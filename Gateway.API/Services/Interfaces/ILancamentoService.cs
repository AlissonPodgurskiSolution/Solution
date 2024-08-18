using Core.Communication;
using Gateway.API.Models;

namespace Gateway.API.Services.Interfaces;

public interface ILancamentoService
{
    Task<ResponseResult> AdicionarLancamento(LancamentoRequest lancamento);
}
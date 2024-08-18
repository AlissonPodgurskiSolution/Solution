using Core.Communication;
using Gateway.API.Models;
using Gateway.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Controllers;

namespace Gateway.API.Controllers;

[Authorize]
public class FluxoDeCaixaController : MainController
{
    private readonly ILancamentoService _lancamentoService;

    public FluxoDeCaixaController(
        ILancamentoService lancamentoService)
    {
        _lancamentoService = lancamentoService;
    }

    [HttpPost]
    [Route("adicionar-lancamento")]
    public async Task<ResponseResult> AdicionarLancamento(LancamentoRequest lancamento)
    {
        var lancamentoResult = await _lancamentoService.AdicionarLancamento(lancamento);

        return lancamentoResult;
    }
}
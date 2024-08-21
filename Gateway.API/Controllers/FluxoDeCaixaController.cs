using Core.Communication;
using Gateway.API.Models;
using Gateway.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Controllers;

namespace Gateway.API.Controllers;

[Authorize]
public class FluxoDeCaixaController : MainController
{
    private readonly ILancamentoService _lancamentoService;
    private readonly IConsolidacaoService _consolidacaoService;

    public FluxoDeCaixaController(
        ILancamentoService lancamentoService, 
        IConsolidacaoService consolidacaoService)
    {
        _lancamentoService = lancamentoService;
        _consolidacaoService = consolidacaoService;
    }

    [HttpPost]
    [Route("adicionar-lancamento")]
    public async Task<ResponseResult> AdicionarLancamento(LancamentoRequest lancamento)
    {
        var result = await _lancamentoService.AdicionarLancamento(lancamento);

        return result;
    }

    [HttpGet]
    [Route("obter-consolidacao-dia")]
    public async Task<Consolidacao> ObterConsolidacaoDia()
    {
        var result = await _consolidacaoService.ObterConsolidacaoDoDia();

        return result;
    }
}
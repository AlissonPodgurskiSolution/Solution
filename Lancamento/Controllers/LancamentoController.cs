using Lancamento.API.Application.Comands;
using Lancamento.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Mediator;
using WebApi.Core.Controllers;

namespace Lancamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LancamentoController : MainController
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMediatorHandler _mediator;

    public LancamentoController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("lancamentos-mediator")]
    public async Task<IActionResult> CriarLancamento(LancamentoCommand lancamento)
    {
        return CustomResponse(await _mediator.SendCommand(lancamento));
    }
}
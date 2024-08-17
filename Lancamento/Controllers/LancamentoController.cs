using Lancamento.API.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Controllers;

namespace Lancamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LancamentoController : MainController
{
    private readonly ILancamentoRepository _lancamentoRepository;

    public LancamentoController(ILancamentoRepository lancamentoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
    }

    [HttpGet("lancamentos")]
    public async Task<PagedResult<Models.Lancamento>> Index([FromQuery] int ps = 10, [FromQuery] int page = 1,
        [FromQuery] string q = null)
    {
        return await _lancamentoRepository.ObterTodos(ps, page, q);
    }

    [HttpGet("lancamentos/{id}")]
    public async Task<Models.Lancamento> DetalheLancamento(Guid id)
    {
        return await _lancamentoRepository.ObterPorId(id);
    }

    [HttpGet("lancamentos/lista/{ids}")]
    public async Task<IEnumerable<Models.Lancamento>> ObterLancamentosPorId(string ids)
    {
        return await _lancamentoRepository.ObterLancamentosPorId(ids);
    }

    [HttpPost("lancamentos")]
    public ActionResult CriarLancamento([FromBody] Models.Lancamento lancamento)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        _lancamentoRepository.Adicionar(lancamento);
        return CustomResponse(lancamento);
    }

    [HttpPut("lancamentos/{id}")]
    public ActionResult AtualizarLancamento(Guid id, [FromBody] Models.Lancamento lancamento)
    {
        if (id != lancamento.Id) return BadRequest("O ID do lançamento não corresponde.");

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        _lancamentoRepository.Atualizar(lancamento);
        return CustomResponse(lancamento);
    }
}
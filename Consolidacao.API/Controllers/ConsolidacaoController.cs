using Consolidacao.API.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Controllers;

namespace Consolidacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConsolidacaoController : MainController
{
    private readonly IConsolidacaoRepository _consolidacaoRepository;

    public ConsolidacaoController(IConsolidacaoRepository consolidacaoRepository)
    {
        _consolidacaoRepository = consolidacaoRepository;
    }

    [HttpGet("consolidacao-dia")]
    public async Task<Models.Consolidacao> GetConsolidacaoDoDia()
    {
        return await _consolidacaoRepository.ObterPorData(DateTime.UtcNow);
    }
}
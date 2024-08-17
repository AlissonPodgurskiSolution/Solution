using System.Globalization;
using Core.Communication;
using Gateway.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Controllers;

namespace Gateway.API.Controllers
{
    [Authorize]
    public class LancamentoController : MainController
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentoController(
            ILancamentoService lancamentoService){
            _lancamentoService = lancamentoService;
        }

        [HttpPost]
        [Route("lancar")]
        public async Task<ResponseResult> AdicionarPedido(LancamentoRequest lancamento)
        {
            var lancamentoResult = await _lancamentoService.FazerLancamento(lancamento);

            return lancamentoResult;
        }
    }
}

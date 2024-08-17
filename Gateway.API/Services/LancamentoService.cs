using Core.Communication;
using Gateway.API.Extensions;
using Microsoft.Extensions.Options;

namespace Gateway.API.Services
{
    public interface ILancamentoService
    {
        //Task<ItemProdutoDTO> ObterPorId(Guid id);
        //Task<IEnumerable<ItemProdutoDTO>> ObterItens(IEnumerable<Guid> ids);

        Task<ResponseResult> FazerLancamento(LancamentoRequest lancamento);
    }

    public class LancamentoService : Service, ILancamentoService
    {
        private readonly HttpClient _httpClient;

        public LancamentoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.LancamentoUrl);
        }

        //public async Task<ItemProdutoDTO> ObterPorId(Guid id)
        //{
        //    var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

        //    TratarErrosResponse(response);

        //    return await DeserializarObjetoResponse<ItemProdutoDTO>(response);
        //}

        //public async Task<IEnumerable<ItemProdutoDTO>> ObterItens(IEnumerable<Guid> ids)
        //{
        //    var idsRequest = string.Join(",", ids);

        //    var response = await _httpClient.GetAsync($"/catalogo/produtos/lista/{idsRequest}/");

        //    TratarErrosResponse(response);

        //    return await DeserializarObjetoResponse<IEnumerable<ItemProdutoDTO>>(response);
        //}

        public async Task<ResponseResult> FazerLancamento(LancamentoRequest lancamento)
        {
            var lancamentoContent = ObterConteudo(lancamento);

            var response = await _httpClient.PostAsync("/lancamentos/", lancamentoContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
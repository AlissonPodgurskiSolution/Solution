using Core.Communication;
using Gateway.API.Extensions;
using Gateway.API.Models;
using Microsoft.Extensions.Options;

namespace Gateway.API.Services;

public interface ILancamentoService
{
    Task<ResponseResult> AdicionarLancamento(LancamentoRequest lancamento);
}

public class LancamentoService : Service, ILancamentoService
{
    private readonly HttpClient _httpClient;

    public LancamentoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.LancamentoUrl);
    }

    public async Task<ResponseResult> AdicionarLancamento(LancamentoRequest lancamento)
    {
        var itemContent = ObterConteudo(lancamento);

        Console.WriteLine("Preparando para enviar o lançamento.");

        var response = await _httpClient.PostAsync("/api/Lancamento/lancamentos-mediator/", itemContent);

        Console.WriteLine($"Resposta recebida: {response.StatusCode}");

        if (!TratarErrosResponse(response))
        {
            var result = await DeserializarObjetoResponse<ResponseResult>(response);
            Console.WriteLine("Erro na resposta do servidor.");
            return result;
        }

        Console.WriteLine("Lançamento adicionado com sucesso.");
        return RetornoOk();
    }

}
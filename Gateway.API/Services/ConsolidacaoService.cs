using Core.Communication;
using Gateway.API.Extensions;
using Gateway.API.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Gateway.API.Services;

public class ConsolidacaoService : Service, IConsolidacaoService
{
    private readonly HttpClient _httpClient;

    public ConsolidacaoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.ConsolidacaoUrl);
    }

    public async Task<ResponseResult> ObterConsolidacaoDoDia()
    {
        Console.WriteLine("Preparando para enviar o lançamento.");

        var response = await _httpClient.GetAsync("/api/Consolidcao/consolidacao-dia/");

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
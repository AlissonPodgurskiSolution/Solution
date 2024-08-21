using Core.Communication;
using Gateway.API.Extensions;
using Gateway.API.Models;
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

    public async Task<Consolidacao> ObterConsolidacaoDoDia()
    {
        Console.WriteLine("Preparando para enviar o Obter Consolidacao Do Dia.");

        var response = await _httpClient.GetAsync("/api/consolidacao/consolidacao-dia/");

        Console.WriteLine($"Resposta recebida: {response.StatusCode}");

        if (response.IsSuccessStatusCode)
        {
            // Desserializa o conteúdo da resposta para o objeto Consolidacao
            var result = await response.Content.ReadFromJsonAsync<Consolidacao>();

            if (result != null)
            {
                Console.WriteLine("Obter Consolidacao Do Dia com sucesso.");
                return result;
            }
            else
            {
                Console.WriteLine("Falha ao desserializar o objeto Consolidacao.");
                return null;
            }
        }
        else
        {
            Console.WriteLine("Erro na resposta do servidor.");
            return null;
        }
    }


}
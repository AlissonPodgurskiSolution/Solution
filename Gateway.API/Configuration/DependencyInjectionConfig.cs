using Gateway.API.Extensions;
using Gateway.API.Services;
using Gateway.API.Services.Interfaces;
using Polly;
using WebApi.Core.Extensions;
using WebApi.Core.Usuario;

namespace Gateway.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ILancamentoService, LancamentoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AllowSelfSignedCertificate()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}
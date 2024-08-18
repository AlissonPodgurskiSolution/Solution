using Consolidacao.API.Application.Comands;
using Consolidacao.API.Data;
using Consolidacao.API.Data.Repository;
using Consolidacao.API.Models.Interfaces;
using Consolidacao.API.Services;
using Consolidacao.API.Services.Interfaces;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using WebApi.Core.Usuario;

namespace Consolidacao.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Outros serviços
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        // Repositórios
        services.AddScoped<IConsolidacaoRepository, ConsolidacaoRepository>();
        services.AddScoped<ILancamentoConsolidacaoRepository, LancamentoConsolidacaoRepository>();

        // Serviços
        services.AddScoped<IConsolidacaoService, ConsolidacaoService>(); 

        // MediatR
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services
            .AddScoped<IRequestHandler<LancamentoConsolidacaoCommand, ValidationResult>,
                LancamentoConsolidacaoCommandHandler>();

        // HostedService
        services.AddHostedService<ConsolidacaoDiariaHostedService>();

        // Contexto
        services.AddScoped<ConsolidacaoContext>();
    }
}
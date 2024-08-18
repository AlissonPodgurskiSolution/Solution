using FluentValidation.Results;
using Lancamento.API.Application.Comands;
using Lancamento.API.Data;
using Lancamento.API.Data.Repository;
using Lancamento.API.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using WebApi.Core.Usuario;

namespace Lancamento.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<LancamentoCommand, ValidationResult>, LancamentoCommandHandler>();
        services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        services.AddScoped<LancamentoContext>();
    }
}
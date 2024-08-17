using Lancamento.API.Data;
using Lancamento.API.Data.Repository;
using Lancamento.API.Models;

namespace Lancamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            services.AddScoped<LancamentoContext>();
        }
    }
}
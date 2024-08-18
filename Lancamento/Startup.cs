using System.Text.Json.Serialization;
using Lancamento.API.Configuration;
using Lancamento.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Core.Identidade;
using MediatR;

namespace Lancamento.API;

public class Startup
{
    public Startup(IHostEnvironment hostEnvironment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (hostEnvironment.IsDevelopment()) builder.AddUserSecrets<Startup>();

        Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);

        services.AddMessageBusConfiguration(Configuration);

        services.AddJwtConfiguration(Configuration);

        services.AddSwaggerConfiguration();

        services.AddMediatR(typeof(Startup));

        services.RegisterServices();

        services.AddMessageBusConfiguration(Configuration);

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<LancamentoContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
            }
        }

        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(env);
    }
}
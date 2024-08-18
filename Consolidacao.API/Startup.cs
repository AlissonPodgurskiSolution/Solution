using System.Text.Json.Serialization;
using Consolidacao.API.Configuration;
using Consolidacao.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Identidade;

namespace Consolidacao.API;

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
            var context = serviceScope.ServiceProvider.GetRequiredService<ConsolidacaoContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao aplicar migra��es: {ex.Message}");
            }
        }

        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(env);
    }
}
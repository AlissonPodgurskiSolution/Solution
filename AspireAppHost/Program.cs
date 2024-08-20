using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

// Configura a conex�o com o RabbitMQ
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var opentelemetry = builder.Services.AddOpenTelemetry().WithMetrics(m => m.AddMeter("APMeter"));
// Cria e associa os bancos de dados para cada microservi�o

// Configura��o de OpenTelemetry para m�tricas
builder.Services.AddOpenTelemetry()
    .WithMetrics(m =>
    {
        m.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AspireApplication"))
            .AddAspNetCoreInstrumentation()
            .AddMeter("APMeter")
            .AddPrometheusExporter(); // Adiciona o exportador Prometheus para expor as m�tricas
    });

// Banco de dados para o servi�o de Consolida��o
var consolidacaoDb = sql.AddDatabase("ConsolidacaoDb");
var consolidacaoApi = builder.AddProject<Consolidacao_API>("consolidacaoapi")
    .WithReference(consolidacaoDb)
    .WithReference(rabbitmq);

// Banco de dados para o servi�o de Lan�amento
var lancamentoDb = sql.AddDatabase("LancamentoDb");
var lancamentoApi = builder.AddProject<Lancamento_API>("lancamentoapi")
    .WithReference(lancamentoDb)
    .WithReference(rabbitmq);

// Banco de dados para o servi�o de Identidade
var identityDB = sql.AddDatabase("IdentityDB");
var identidadeApi = builder.AddProject<Identidade_API>("identityapi")
    .WithReference(identityDB)
    .WithReference(rabbitmq);

// Configura��o do Gateway API (n�o requer banco de dados separado)
var gatewayApi = builder.AddProject<Gateway_API>("gatewayapi")
    .WithReference(rabbitmq); // Associa RabbitMQ ao Gateway tamb�m, se necess�rio

// Constr�i e executa a aplica��o
builder.Build().Run();
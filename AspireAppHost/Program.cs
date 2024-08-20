using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

// Configura a conexão com o RabbitMQ
var rabbitmq = builder.AddRabbitMQ("rabbitmq");
var opentelemetry = builder.Services.AddOpenTelemetry().WithMetrics(m => m.AddMeter("APMeter"));
// Cria e associa os bancos de dados para cada microserviço

// Configuração de OpenTelemetry para métricas
builder.Services.AddOpenTelemetry()
    .WithMetrics(m =>
    {
        m.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AspireApplication"))
            .AddAspNetCoreInstrumentation()
            .AddMeter("APMeter")
            .AddPrometheusExporter(); // Adiciona o exportador Prometheus para expor as métricas
    });

// Banco de dados para o serviço de Consolidação
var consolidacaoDb = sql.AddDatabase("ConsolidacaoDb");
var consolidacaoApi = builder.AddProject<Consolidacao_API>("consolidacaoapi")
    .WithReference(consolidacaoDb)
    .WithReference(rabbitmq);

// Banco de dados para o serviço de Lançamento
var lancamentoDb = sql.AddDatabase("LancamentoDb");
var lancamentoApi = builder.AddProject<Lancamento_API>("lancamentoapi")
    .WithReference(lancamentoDb)
    .WithReference(rabbitmq);

// Banco de dados para o serviço de Identidade
var identityDB = sql.AddDatabase("IdentityDB");
var identidadeApi = builder.AddProject<Identidade_API>("identityapi")
    .WithReference(identityDB)
    .WithReference(rabbitmq);

// Configuração do Gateway API (não requer banco de dados separado)
var gatewayApi = builder.AddProject<Gateway_API>("gatewayapi")
    .WithReference(rabbitmq); // Associa RabbitMQ ao Gateway também, se necessário

// Constrói e executa a aplicação
builder.Build().Run();
namespace Gateway.API.Models;

public record Consolidacao(
    DateTime Data,
    decimal Saldo,
    decimal TotalCreditos,
    decimal TotalDebitos,
    int QuantidadeLancamentos)
{
}
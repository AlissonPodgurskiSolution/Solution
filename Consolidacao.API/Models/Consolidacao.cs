using NetDevPack.Domain;

namespace Consolidacao.API.Models;

public class Consolidacao : Entity, IAggregateRoot
{
    public Consolidacao(DateTime data, decimal saldo, decimal totalCreditos, decimal totalDebitos,
        int quantidadeLancamentos)
    {
        Data = data;
        Saldo = saldo;
        TotalCreditos = totalCreditos;
        TotalDebitos = totalDebitos;
        QuantidadeLancamentos = quantidadeLancamentos;
    }

    public DateTime Data { get; private set; } // Pode ser o fim do dia, semana ou mês
    public decimal Saldo { get; private set; } // Saldo consolidado do período
    public decimal TotalCreditos { get; private set; } // Total de créditos do período
    public decimal TotalDebitos { get; private set; } // Total de débitos do período
    public int QuantidadeLancamentos { get; private set; } // Número total de lançamentos no período

    public void Atualizar(decimal saldo, decimal totalCreditos, decimal totalDebitos, int quantidadeLancamentos)
    {
        Saldo = saldo;
        TotalCreditos = totalCreditos;
        TotalDebitos = totalDebitos;
        QuantidadeLancamentos = quantidadeLancamentos;
    }
}
using Consolidacao.API.Application.Comands;
using Consolidacao.API.Models;
using Consolidacao.API.Models.Interfaces;
using Core.Enumerators;
using MessageBus;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Consolidacao.API.Tests;

public class LancamentoConsolidacaoCommandHandlerTests
{
    private readonly Mock<ILancamentoConsolidacaoRepository> _mockLancamentoRepository;
    private readonly Mock<IMessageBus> _mockMessageBus;
    private readonly LancamentoConsolidacaoCommandHandler _handler;

    public LancamentoConsolidacaoCommandHandlerTests()
    {
        _mockLancamentoRepository = new Mock<ILancamentoConsolidacaoRepository>();
        _mockMessageBus = new Mock<IMessageBus>();
        _handler = new LancamentoConsolidacaoCommandHandler(_mockLancamentoRepository.Object, _mockMessageBus.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldAddLancamentoAndCommit()
    {
        // Arrange
        var command = new LancamentoConsolidacaoCommand(
            DateTime.UtcNow.AddDays(-1),
            TipoLancamento.Credito,
            100m,
            "Descri��o do lan�amento",
            Guid.NewGuid()
        );

        _mockLancamentoRepository.Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsValid);
        _mockLancamentoRepository.Verify(r => r.Adicionar(It.IsAny<LancamentoConsolidacao>()), Times.Once);
        _mockLancamentoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldReturnValidationErrors()
    {
        // Arrange
        var command = new LancamentoConsolidacaoCommand(
            DateTime.UtcNow.AddDays(1), // Data no futuro, deve falhar
            TipoLancamento.Credito,
            -100m, // Valor inv�lido, deve falhar
            "Descri��o inv�lida", // Descri��o v�lida
            Guid.NewGuid()
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "A data n�o pode ser no futuro."));
        Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "O valor do lan�amento deve ser maior que zero."));
        _mockLancamentoRepository.Verify(r => r.Adicionar(It.IsAny<LancamentoConsolidacao>()), Times.Never);
        _mockLancamentoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }
}
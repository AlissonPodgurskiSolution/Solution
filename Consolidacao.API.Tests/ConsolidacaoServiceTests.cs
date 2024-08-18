//using Consolidacao.API.Models;
//using Consolidacao.API.Models.Interfaces;
//using Consolidacao.API.Services;
//using Core.Enumerators;
//using Moq;
//using Xunit;

//public class ConsolidacaoServiceTests
//{
//    private readonly Mock<IConsolidacaoRepository> _mockConsolidacaoRepository;
//    private readonly Mock<ILancamentoConsolidacaoRepository> _mockLancamentoRepository;
//    private readonly Mock<NetDevPack.Data.IUnitOfWork> _mockUnitOfWork;
//    private readonly ConsolidacaoService _service;

//    public ConsolidacaoServiceTests()
//    {
//        _mockConsolidacaoRepository = new Mock<IConsolidacaoRepository>();
//        _mockLancamentoRepository = new Mock<ILancamentoConsolidacaoRepository>();
//        _mockUnitOfWork = new Mock<NetDevPack.Data.IUnitOfWork>();
//        _service = new ConsolidacaoService(_mockLancamentoRepository.Object, _mockConsolidacaoRepository.Object);

//        _mockConsolidacaoRepository.Setup(r => r.UnitOfWork).Returns(_mockUnitOfWork.Object);
//    }

//    [Fact]
//    public async Task ConsolidarDia_NovoDiaDeveCriarConsolidacao()
//    {
//        // Arrange
//        var data = new DateTime(2023, 8, 18);
//        var lancamentos = new List<LancamentoConsolidacao>
//        {
//            new(data, TipoLancamento.Credito, 100),
//            new(data, TipoLancamento.Debito, 50)
//        };

//        _mockLancamentoRepository.Setup(r => r.ObterLancamentosPorData(data))
//            .ReturnsAsync(lancamentos);

//        _mockConsolidacaoRepository.Setup(r => r.ObterPorData(data))
//            .ReturnsAsync((Consolidacao.API.Models.Consolidacao)null);

//        _mockUnitOfWork.Setup(u => u.Commit()).Returns((Task<bool>)Task.CompletedTask);

//        // Act
//        await _service.ConsolidarDia(data);

//        // Assert
//        _mockConsolidacaoRepository.Verify(r => r.Adicionar(It.Is<Consolidacao.API.Models.Consolidacao>(c =>
//            c.Data == data &&
//            c.Saldo == 50 &&
//            c.TotalCreditos == 100 &&
//            c.TotalDebitos == 50 &&
//            c.QuantidadeLancamentos == 2
//        )), Times.Once);

//        _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
//    }

//    [Fact]
//    public async Task ConsolidarDia_ConsolidacaoExistenteDeveAtualizar()
//    {
//        // Arrange
//        var data = new DateTime(2023, 8, 18);
//        var lancamentos = new List<LancamentoConsolidacao>
//        {
//            new(data, TipoLancamento.Credito, 200),
//            new(data, TipoLancamento.Debito, 50)
//        };

//        var consolidacaoExistente = new Consolidacao.API.Models.Consolidacao(data, 0, 0, 0, 0);

//        _mockLancamentoRepository.Setup(r => r.ObterLancamentosPorData(data))
//            .ReturnsAsync(lancamentos);

//        _mockConsolidacaoRepository.Setup(r => r.ObterPorData(data))
//            .ReturnsAsync(consolidacaoExistente);

//        _mockUnitOfWork.Setup(u => u.Commit()).Returns((Task<bool>)Task.CompletedTask);

//        // Act
//        await _service.ConsolidarDia(data);

//        // Assert
//        _mockConsolidacaoRepository.Verify(r => r.Atualizar(It.Is<Consolidacao.API.Models.Consolidacao>(c =>
//            c.Saldo == 150 &&
//            c.TotalCreditos == 200 &&
//            c.TotalDebitos == 50 &&
//            c.QuantidadeLancamentos == 2
//        )), Times.Once);

//        _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
//    }
//}
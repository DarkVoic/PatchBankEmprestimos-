using API_EmprestimoConsignado.Controllers;
using BankEmprestimoConsignado.Data;
using BankEmprestimoConsignado.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Teste_EmprestimoConsignado
{
    public class ClientesControllerTest
    {
        private readonly Mock<DbSet<Emprestimo>> _mockSet;
        private readonly Mock<BankContext> _mockContext;
        private readonly Emprestimo _emprestimo;
        public ClientesControllerTest()
        {
            _mockSet = new Mock<DbSet<Emprestimo>>();
            _mockContext = new Mock<BankContext>();
            _emprestimo = new Emprestimo { IdEmprestimo = 1, ValorEmprestimo = 5000, QtdParcela = 15, TaxaJuros = 1, TipoEmprest = 1, DataVenc = DateTime.Now };

            _mockContext.Setup(m => m.Emprestimos).Returns(_mockSet.Object);

            _mockContext.Setup(m => m.Emprestimos.FindAsync(1))
                .ReturnsAsync(_emprestimo);

            _mockContext.Setup(m => m.SetModified(_emprestimo));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new EmprestimosController(_mockContext.Object);

            await service.GetEmprestimo(1);
            _mockSet.Verify(expression: m => m.FindAsync(1),
                times: Times.Once());

        }

        //[Fact]
        //public async Task Put_Categoria()
        //{
        //    var service = new CategoriasController(_mockContext.Object);

        //    await service.PutCategoria(1, _categoria);

        //    _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
        //        Times.Once());

        //}

        //[Fact]
        //public async Task Post_Categoria()
        //{
        //    var service = new CategoriasController(_mockContext.Object);
        //    await service.PostCategoria(_categoria);

        //    _mockSet.Verify(x => x.Add(_categoria), Times.Once);
        //    _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
        //        Times.Once());
        //}

        //[Fact]
        //public async Task Delete_Categoria()
        //{
        //    var service = new CategoriasController(_mockContext.Object);
        //    await service.DeleteCategoria(1);

        //    _mockSet.Verify(m => m.FindAsync(1),
        //        Times.Once());
        //    _mockSet.Verify(x => x.Remove(_categoria), Times.Once());
        //    _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
        //        Times.Once());
        //}
    }
}

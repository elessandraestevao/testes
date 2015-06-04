using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using mock.dominio;
using mock.servico;
using mock.infra;
using mock.interfaces;

namespace mock.testes
{
    [TestFixture]
    class GeradorDePagamentosTest
    {
        private Mock<IRepositorioDeLeiloes> leilaoDao;

        [SetUp]
        public void SetUp()
        {
            this.leilaoDao = new Mock<IRepositorioDeLeiloes>();
        }

        [Test]
        public void DeveGerarPagamentoComMaiorValorDeLance()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("João"), 2000.0))
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("Maria"), 2500.0))
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            leilaoDao.Setup(d => d.encerrados()).Returns(leiloes);                       

            Pagamento realizado = null;
            var pagamentoDao = new Mock<PagamentoDao>();
            pagamentoDao.Setup(p => p.Salvar(It.IsAny<Pagamento>())).Callback<Pagamento>(r => realizado = r);

            GeradorDePagamentos gerador = new GeradorDePagamentos(leilaoDao.Object, pagamentoDao.Object, new Avaliador());
            gerador.Gera();

            Assert.AreEqual(2500.0, realizado.Valor);            
        }

        [Test]
        public void DeveMudarDiaAtualParaSegundaFeiraCasoSejaSabado()
        {
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);
            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("João"), 2000.0))
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("Maria"), 2500.0))
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            leilaoDao.Setup(d => d.encerrados()).Returns(leiloes);

            Pagamento pagamentoCapturado = null;
            var pagamentoDao = new Mock<PagamentoDao>();
            pagamentoDao.Setup(p => p.Salvar(It.IsAny<Pagamento>())).Callback<Pagamento>(r => pagamentoCapturado = r);

            //Manipulando a data como SÁBADO
            var relogio = new Mock<IRelogio>();
            relogio.Setup(r => r.Hoje()).Returns(new DateTime(2015, 5, 30));

            GeradorDePagamentos gerador = new GeradorDePagamentos(leilaoDao.Object, pagamentoDao.Object, new Avaliador(), relogio.Object);
            gerador.Gera();

            Assert.AreEqual(DayOfWeek.Monday, pagamentoCapturado.Data.DayOfWeek);
        }

        [Test]
        public void DeveMudarDiaAtualParaSegundaFeiraCasoSejaDomingo()
        {
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);
            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("João"), 2000.0))
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("Maria"), 2500.0))
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            leilaoDao.Setup(d => d.encerrados()).Returns(leiloes);

            Pagamento pagamentoCapturado = null;
            var pagamentoDao = new Mock<PagamentoDao>();
            pagamentoDao.Setup(p => p.Salvar(It.IsAny<Pagamento>())).Callback<Pagamento>(r => pagamentoCapturado = r);

            //Manipulando a data como DOMINGO
            var relogio = new Mock<IRelogio>();
            relogio.Setup(r => r.Hoje()).Returns(new DateTime(2015, 5, 31));

            GeradorDePagamentos gerador = new GeradorDePagamentos(leilaoDao.Object, pagamentoDao.Object, new Avaliador(), relogio.Object);
            gerador.Gera();

            Assert.AreEqual(DayOfWeek.Monday, pagamentoCapturado.Data.DayOfWeek);
        }

        [Test]
        public void DeveEntenderCorretamenteDiaDaSemanaQueEhDiaUtil()
        {
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);
            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("João"), 2000.0))
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .Lance(new Lance(new Usuario("Maria"), 2500.0))
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            leilaoDao.Setup(d => d.encerrados()).Returns(leiloes);

            Pagamento pagamentoCapturado = null;
            var pagamentoDao = new Mock<PagamentoDao>();
            pagamentoDao.Setup(p => p.Salvar(It.IsAny<Pagamento>())).Callback<Pagamento>(r => pagamentoCapturado = r);

            //Manipulando a data como DIA ÚTIL - TERÇA-FEIRA
            var relogio = new Mock<IRelogio>();
            relogio.Setup(r => r.Hoje()).Returns(new DateTime(2015, 6, 2));

            GeradorDePagamentos gerador = new GeradorDePagamentos(leilaoDao.Object, pagamentoDao.Object, new Avaliador(), relogio.Object);
            gerador.Gera();

            Assert.AreEqual(DayOfWeek.Tuesday, pagamentoCapturado.Data.DayOfWeek);
        }
    }
}

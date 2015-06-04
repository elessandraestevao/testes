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

            leilaoDao.Setup(d => d.correntes()).Returns(leiloes);

            var avaliador = new Mock<Avaliador>();
            avaliador.Setup(a => a.maiorValor).Returns(2500.0);

            var pagamentoDao = new Mock<PagamentoDao>();
            Pagamento realizado = null;            

            pagamentoDao.Setup(p => p.Salvar(It.IsAny<Pagamento>())).Callback<Pagamento>(r => realizado = r);

            GeradorDePagamentos gerador = new GeradorDePagamentos(leilaoDao.Object, pagamentoDao.Object, avaliador.Object);
            gerador.Gera();

            Assert.AreEqual(2500.0, realizado.Valor);
            
        }
    }
}

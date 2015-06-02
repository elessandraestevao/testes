using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using mock.dominio;
using Moq;
using mock.infra;
using mock.servico;
using mock.interfaces;

namespace mock.testes
{
    [TestFixture]
    class EncerradorDeLeilaoTest
    {
        [Test]
        public void DeveEncerrarLeiloesQueComecaramHaMaisDeUmaSemana()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            Leilao leilao1 = new Leilao("Joias raras");
            leilao1.naData(dataSemanaPassada);

            Leilao leilao2 = new Leilao("Carros antigos");
            leilao2.naData(dataSemanaPassada);

            List<Leilao> leiloes = new List<Leilao>();
            leiloes.Add(leilao1);
            leiloes.Add(leilao2);

            var dao = new Mock<IRepositorioDeLeiloes>();
            dao.Setup(d => d.correntes()).Returns(leiloes);

            //Ação
            EncerradorDeLeilao encerrador = new EncerradorDeLeilao(dao.Object);
            encerrador.encerra();

            //Validação
            Assert.AreEqual(2, encerrador.total);
            Assert.True(leilao1.encerrado);
            Assert.True(leilao2.encerrado);

        }

        [Test]
        public void NaoDeveEncerrarLeilaoQueComecouNaDataDeHoje()
        {
            //Cenário
            DateTime dataHoje = DateTime.Today; ;

            Leilao leilao1 = new Leilao("Joias raras");
            leilao1.naData(dataHoje);

            Leilao leilao2 = new Leilao("Carros antigos");
            leilao2.naData(dataHoje);

            List<Leilao> leiloes = new List<Leilao>();
            leiloes.Add(leilao1);
            leiloes.Add(leilao2);

            var dao = new Mock<IRepositorioDeLeiloes>();
            dao.Setup(d => d.correntes()).Returns(leiloes);

            //Ação
            EncerradorDeLeilao encerrador = new EncerradorDeLeilao(dao.Object);
            encerrador.encerra();

            //Validação
            Assert.AreEqual(0, encerrador.total);
            Assert.False(leilao1.encerrado);
            Assert.False(leilao2.encerrado);
        }

        [Test]
        public void NaoDeveFazerNadaSeNaoTiverLeiloesNaListaDeLeiloes()
        {
            //Cenário
            var dao = new Mock<IRepositorioDeLeiloes>();
            dao.Setup(d => d.correntes()).Returns(new List<Leilao>());

            //Ação
            EncerradorDeLeilao encerrador = new EncerradorDeLeilao(dao.Object);
            encerrador.encerra();

            //Validação
            Assert.AreEqual(0, encerrador.total);
        }
    }
}

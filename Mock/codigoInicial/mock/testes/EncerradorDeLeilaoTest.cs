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
        private Mock<Carteiro> carteiro;
        private Mock<IRepositorioDeLeiloes> dao;
        private EncerradorDeLeilao encerrador;

        [SetUp]
        public void SetUp()
        {
            this.carteiro = new Mock<Carteiro>();
            this.dao = new Mock<IRepositorioDeLeiloes>();
            this.encerrador = new EncerradorDeLeilao(dao.Object, carteiro.Object);
        }

        [Test]
        public void DeveEncerrarLeiloesQueComecaramHaMaisDeUmaSemana()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);            

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();            
            
            dao.Setup(d => d.correntes()).Returns(leiloes);          

            //Ação            
            encerrador.encerra();

            //Validação
            Assert.AreEqual(2, encerrador.total);
            Assert.True(leiloes[0].encerrado);
            Assert.True(leiloes[1].encerrado);

        }

        [Test]
        public void NaoDeveEncerrarLeilaoQueComecouNaDataDeHoje()
        {
            //Cenário
            DateTime dataHoje = DateTime.Today; ;

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataHoje)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataHoje)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();
            
            dao.Setup(d => d.correntes()).Returns(leiloes);

            //Ação            
            encerrador.encerra();

            //Validação
            Assert.AreEqual(0, encerrador.total);
            Assert.False(leiloes[0].encerrado);
            Assert.False(leiloes[1].encerrado);
        }

        [Test]
        public void NaoDeveFazerNadaSeNaoTiverLeiloesNaListaDeLeiloes()
        {
            //Cenário            
            dao.Setup(d => d.correntes()).Returns(new List<Leilao>());

            //Ação            
            encerrador.encerra();

            //Validação
            Assert.AreEqual(0, encerrador.total);
        }

        [Test]
        public void DeveChamarOMetodoAtualizaAoEncerrarUmLeilao()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();
           
            dao.Setup(d => d.correntes()).Returns(leiloes);

            //Ação            
            encerrador.encerra();

            //Validação
            dao.Verify(d => d.atualiza(leiloes[0]), Times.Once());
            dao.Verify(d => d.atualiza(leiloes[1]), Times.Once());
        }

        [Test]
        public void NaoDeveAtualizarOsLeiloesEncerrados()
        {
            //Cenário
            DateTime dataHoje = DateTime.Today; ;

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataHoje)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataHoje)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();
           
            dao.Setup(d => d.correntes()).Returns(leiloes);            

            //Ação            
            encerrador.encerra();

            //Validação
            dao.Verify(d => d.atualiza(leiloes[0]), Times.Never());
            dao.Verify(d => d.atualiza(leiloes[1]), Times.Never());
        }

        [Test]
        public void DeveContinuarAEncerrarLeiloesDaListaMesmoComExcecaoEmUmDeles()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            dao.Setup(d => d.correntes()).Returns(leiloes);
            dao.Setup(d => d.atualiza(leiloes[0])).Throws(new Exception());            

            //Ação            
            encerrador.encerra();

            //Validação              
            dao.Verify(d => d.atualiza(leiloes[1]), Times.Once());
            carteiro.Verify(c => c.envia(leiloes[1]), Times.Once());
            carteiro.Verify(c => c.envia(leiloes[0]), Times.Never());
        }

        [Test]
        public void DeveContinuarAEncerrarLeiloesDaListaMesmoComExcecaoNoEnvioDeEmail()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            dao.Setup(d => d.correntes()).Returns(leiloes);
            carteiro.Setup(c => c.envia(leiloes[0])).Throws(new Exception());

            //Ação            
            encerrador.encerra();

            //Validação              
            dao.Verify(d => d.atualiza(leiloes[0]), Times.Once());
            dao.Verify(d => d.atualiza(leiloes[1]), Times.Once());
            carteiro.Verify(c => c.envia(leiloes[1]), Times.Once());
        }

        [Test]
        public void NaoDeveEnviarEmailSeOcorrerExcecaoAoAtualizarLeiloes()
        {
            //Cenário
            DateTime dataSemanaPassada = new DateTime(2015, 5, 20);

            //cenário utilizando Test Data Builder
            List<Leilao> leiloes = new CriadorDeLeilao().Para("Joias raras")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .Para("Carros antigos")
                .NaData(dataSemanaPassada)
                .AdicionaNaLista()
                .RetornaListaDeLeiloes();

            dao.Setup(d => d.correntes()).Returns(leiloes);
            dao.Setup(d => d.atualiza(leiloes[0])).Throws(new Exception());
            dao.Setup(d => d.atualiza(leiloes[1])).Throws(new Exception());

            //Ação            
            encerrador.encerra();

            //Validação           
            carteiro.Verify(c => c.envia(It.IsAny<Leilao>()), Times.Never());                        
        }
    }
}

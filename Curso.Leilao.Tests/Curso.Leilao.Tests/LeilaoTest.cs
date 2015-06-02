using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Curso.Leilao
{
    [TestFixture]
    class LeilaoTest
    {       
        private Usuario joao;
        private Usuario maria;

        [TestFixtureSetUp]
        public void TestandoBeforeClass()
        {
            Console.WriteLine("text fixture setup");
        }

        [TestFixtureTearDown]
        public void TestandoAfterClass()
        {
            Console.WriteLine("test fixture tear down");
        }

        [SetUp]
        public void SetUp()
        {            
            this.joao = new Usuario("João");
            this.maria = new Usuario("Maria");
            Console.WriteLine("Write line");
        }

        [Test]
        public void NaoDeveAceitarDoisLancesSeguidosDoMesmoUsuario()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Macbook Pro")
                .Lance(joao, 6000.0)
                .Lance(joao, 7000.0)
                .Constroi();

            Assert.AreEqual(1, leilao.Lances.Count);
            Assert.AreEqual(6000, leilao.Lances[0].Valor, 0.00001);
        }

        [Test]
        public void NaoDeveAceitarMaisDoQue5LancesDeUmMesmoUsuario()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Macbook Pro")
                .CriaLancesAlternados(new List<Usuario> { joao, maria }, 5)
                .Lance(joao, 12000.0)
                .Constroi();

            int ultimo = leilao.Lances.Count - 1;
            Assert.AreEqual(10, leilao.Lances.Count);
            Assert.AreEqual(11000, leilao.Lances[ultimo].Valor, 0.00001);
        }

        [Test]
        public void DeveDobrarUltimoLanceDoUsuario()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Macbook Pro")
                .Lance(joao, 2000.0)
                .Lance(maria, 3000.0)
                .Lance(joao, 6000.0)
                .Lance(maria, 7000.0)
                .Constroi();

            leilao.DobraLance(joao);

            Assert.AreEqual(12000, leilao.Lances[4].Valor, 0.00001);
            Assert.AreEqual("João", leilao.Lances[2].Usuario.Nome);
        }

        [Test]
        public void NaoDeveDobrarUltimoLanceDoUsuarioCasoNaoHajaLanceAnterior()
        {
            Leilao leilao = new Leilao("Macbook Pro");
            leilao.DobraLance(joao);
            Assert.AreEqual(0, leilao.Lances.Count);            
        }        
    }
}

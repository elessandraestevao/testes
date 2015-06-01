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
        [Test]
        public void NaoDeveAceitarDoisLancesSeguidosDoMesmoUsuario()
        {
            Usuario joao = new Usuario("João");

            Leilao leilao = new Leilao("Leilão de Macbook Pro");

            leilao.Propoe(new Lance(joao, 6000.0));
            leilao.Propoe(new Lance(joao, 7000.0));

            Assert.AreEqual(1, leilao.Lances.Count);
            Assert.AreEqual(6000, leilao.Lances[0].Valor, 0.00001);
        }

        [Test]
        public void NaoDeveAceitarMaisDoQue5LancesDeUmMesmoUsuario()
        {
            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            Leilao leilao = new Leilao("Leilão de Macbook Pro");

            leilao.Propoe(new Lance(joao, 2000.0));
            leilao.Propoe(new Lance(maria, 3000.0));

            leilao.Propoe(new Lance(joao, 4000.0));
            leilao.Propoe(new Lance(maria, 5000.0));

            leilao.Propoe(new Lance(joao, 6000.0));
            leilao.Propoe(new Lance(maria, 7000.0));

            leilao.Propoe(new Lance(joao, 8000.0));
            leilao.Propoe(new Lance(maria, 9000.0));

            leilao.Propoe(new Lance(joao, 10000.0));
            leilao.Propoe(new Lance(maria, 11000.0));

            leilao.Propoe(new Lance(joao, 12000.0));


            int ultimo = leilao.Lances.Count - 1;
            Assert.AreEqual(10, leilao.Lances.Count);
            Assert.AreEqual(11000, leilao.Lances[ultimo].Valor, 0.00001);
        }

        [Test]
        public void DeveDobrarUltimoLanceDoUsuario()
        {
            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            Leilao leilao = new Leilao("Leilão de Macbook Pro");

            leilao.Propoe(new Lance(joao, 2000.0));
            leilao.Propoe(new Lance(maria, 3000.0));

            leilao.Propoe(new Lance(joao, 6000.0));
            leilao.Propoe(new Lance(maria, 7000.0));

            leilao.DobraLance(joao);

            Assert.AreEqual(12000, leilao.Lances[4].Valor, 0.00001);
            Assert.AreEqual("João", leilao.Lances[2].Usuario.Nome);
        }

        [Test]
        public void NaoDeveDobrarUltimoLanceDoUsuarioCasoNaoHajaLanceAnterior()
        {
            Usuario joao = new Usuario("João");
            Leilao leilao = new Leilao("Leilão de Macbook Pro");           

            leilao.DobraLance(joao);

            Assert.AreEqual(0, leilao.Lances.Count);            
        }
    }
}

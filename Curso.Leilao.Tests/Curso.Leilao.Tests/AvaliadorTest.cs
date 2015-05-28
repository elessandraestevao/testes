using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
//using Curso.Leilao;

namespace Curso.Leilao
{
    [TestFixture]
    public class AvaliadorTest
    {
        [Test]
        public void DeveCalcularCorretamenteAMediaDosLancesDados()
        {
            //cenário
            Leilao leilao = new Leilao("Primeiro Leilão");

            Usuario joao = new Usuario("João");
            Usuario jose = new Usuario("José");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(maria, 200.0));
            leilao.Propoe(new Lance(jose, 300.0));
            leilao.Propoe(new Lance(joao, 400.0));


            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            double mediaEsperada = (200 + 300 + 400) / 3;

            Assert.AreEqual(mediaEsperada, avaliador.MediaLances);
        }

        [Test]
        public void DeveEntenderLancesEmOrdemCrescente()
        {
            //cenário
            Leilao leilao = new Leilao("Primeiro Leilão");

            Usuario joao = new Usuario("João");
            Usuario jose = new Usuario("José");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(maria, 200.0));
            leilao.Propoe(new Lance(jose, 300.0));
            leilao.Propoe(new Lance(joao, 400.0));


            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            double menorValorEsperado = 200;
            double maiorValorEsperado = 400;

            Assert.AreEqual(menorValorEsperado, avaliador.MenorLance);
            Assert.AreEqual(maiorValorEsperado, avaliador.MaiorLance);
        }

        [Test]
        public void DeveEntenderMaiorEMenorLanceIguais()
        {
            //cenário
            Leilao leilao = new Leilao("Segundo Leilão");

            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(maria, 200.0));
            leilao.Propoe(new Lance(joao, 200.0));


            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            double menorValorEsperado = 200;
            double maiorValorEsperado = 200;

            Assert.AreEqual(menorValorEsperado, avaliador.MenorLance);
            Assert.AreEqual(maiorValorEsperado, avaliador.MaiorLance);
        }

        [Test]
        public void DeveEntenderApenasUmLanceDado()
        {
            //cenário
            Leilao leilao = new Leilao("Segundo Leilão");

            Usuario joao = new Usuario("João");

            leilao.Propoe(new Lance(joao, 200.0));


            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            double menorValorEsperado = 200;
            double maiorValorEsperado = 200;

            Assert.AreEqual(menorValorEsperado, avaliador.MenorLance);
            Assert.AreEqual(maiorValorEsperado, avaliador.MaiorLance);
        }
    }
}

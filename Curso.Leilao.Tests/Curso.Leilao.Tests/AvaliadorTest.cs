using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Curso.Leilao
{
    [TestFixture]
    public class AvaliadorTest
    {
        [Test]
        public void DeveEncontrarOsTresMaioresLancesComCincoLances()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");

            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 450.0));
            leilao.Propoe(new Lance(joao, 120.0));
            leilao.Propoe(new Lance(maria, 700.0));
            leilao.Propoe(new Lance(joao, 630.0));            

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(3, avaliador.TresMaiores.Count);
            Assert.AreEqual(700, avaliador.TresMaiores[0].Valor, 0.0001);
            Assert.AreEqual(630, avaliador.TresMaiores[1].Valor, 0.0001);
            Assert.AreEqual(450, avaliador.TresMaiores[2].Valor, 0.0001);
        }

        [Test]
        public void DeveEncontrarOsDoisMaioresLancesComDoisLances()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");

            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 450.0));            

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(2, avaliador.TresMaiores.Count);
            Assert.AreEqual(450, avaliador.TresMaiores[0].Valor, 0.0001);
            Assert.AreEqual(200, avaliador.TresMaiores[1].Valor, 0.0001);            
        }

        [Test]
        public void DeveRetornarListaVaziaComZeroLances()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");            

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(0, avaliador.TresMaiores.Count);            
        }

        [Test]
        public void DeveEncontrarOMaiorLanceApenasUmLance()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");

            Usuario joao = new Usuario("João");            

            leilao.Propoe(new Lance(joao, 200.0));            

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(1, avaliador.TresMaiores.Count);            
            Assert.AreEqual(200, avaliador.TresMaiores[0].Valor, 0.0001);
        }

        [Test]
        public void DeveEntenderMaiorEMenorLancesEmOrdemAleatoria()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");

            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");

            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 450.0));
            leilao.Propoe(new Lance(joao, 120.0));
            leilao.Propoe(new Lance(maria, 700.0));
            leilao.Propoe(new Lance(joao, 630.0));
            leilao.Propoe(new Lance(maria, 230.0));

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(120, avaliador.MenorLance, 0.0001);
            Assert.AreEqual(700, avaliador.MaiorLance, 0.0001);
        }

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
        public void DeveEntenderLanceEmOrdemDescrescente()
        {
            //Cenário
            Usuario joao = new Usuario("João");
            Usuario maria = new Usuario("Maria");
            Leilao leilao = new Leilao("Leilão de joias masculinas");
            leilao.Propoe(new Lance(joao, 400.0));
            leilao.Propoe(new Lance(maria, 300.0));
            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 100.0));

            //Ação
            Avaliador avaliador = new Avaliador();
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(400, avaliador.MaiorLance, 0.0001);
            Assert.AreEqual(100, avaliador.MenorLance, 0.0001);

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
            Assert.AreEqual(200, avaliador.MenorLance, 0.0001);
            Assert.AreEqual(200, avaliador.MaiorLance, 0.0001);
        }
    }
}

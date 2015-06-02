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
        private Avaliador avaliador;
        private Usuario joao;
        private Usuario jose;
        private Usuario maria;

        [SetUp]
        public void SetUp()
        {
            this.avaliador = new Avaliador();
            this.joao = new Usuario("João");
            this.jose = new Usuario("José");
            this.maria = new Usuario("Maria");
        }

        [TearDown]
        public void Finaliza()
        {
            Console.WriteLine("fim");
        }

        [Test]
        public void DeveEncontrarOsTresMaioresLancesComCincoLances()
        {
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Leilao de joias femininas")
                .Lance(joao, 200.0)
                .Lance(maria, 450.0)
                .Lance(joao, 120.0)
                .Lance(maria, 700.0)
                .Lance(joao, 630.0)
                .Constroi();

            //Ação          
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
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Leilao de joias femininas")
                .Lance(joao, 200.0)
                .Lance(maria, 450.0)
                .Constroi();

            //Ação            
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(2, avaliador.TresMaiores.Count);
            Assert.AreEqual(450, avaliador.TresMaiores[0].Valor, 0.0001);
            Assert.AreEqual(200, avaliador.TresMaiores[1].Valor, 0.0001);            
        }

        /*[Test]
        public void DeveRetornarListaVaziaComZeroLances()
        {
            //cenário
            Leilao leilao = new Leilao("Leilao de joias femininas");             

            //Ação
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(0, avaliador.TresMaiores.Count);            
        }*/

        [Test]
        public void DeveEncontrarOMaiorLanceApenasUmLance()
        {
            //cenário
            Leilao leilao = new CriadorDeLeilao().Para("Leilao de joias femininas")
                .Lance(joao, 200.0)
                .Constroi();

            //Ação
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(1, avaliador.TresMaiores.Count);            
            Assert.AreEqual(200, avaliador.TresMaiores[0].Valor, 0.0001);
        }

        [Test]
        public void DeveEntenderMaiorEMenorLancesEmOrdemAleatoria()
        {
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Leilao de joias femininas")
                .Lance(joao, 200.0)
                .Lance(maria, 450.0)
                .Lance(joao, 120.0)
                .Lance(maria, 700.0)
                .Lance(joao, 630.0)
                .Lance(maria, 230.0)
                .Constroi();

            //Ação
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(120, avaliador.MenorLance, 0.0001);
            Assert.AreEqual(700, avaliador.MaiorLance, 0.0001);
        }

        [Test]
        public void DeveCalcularCorretamenteAMediaDosLancesDados()
        {
            //cenário
            Leilao leilao = new CriadorDeLeilao().Para("Primeiro Leilão")
                .Lance(maria, 200.0)
                .Lance(jose, 300.0)
                .Lance(joao, 400.0)
                .Constroi();
            //Ação
            avaliador.Avalia(leilao);

            //Validação
            double mediaEsperada = (200 + 300 + 400) / 3;

            Assert.AreEqual(mediaEsperada, avaliador.MediaLances);
        }

        [Test]
        public void DeveEntenderLancesEmOrdemCrescente()
        {
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Primeiro Leilão")
                .Lance(maria, 200.0)
                .Lance(jose, 300.0)
                .Lance(joao, 400.0)
                .Constroi();


            //Ação
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
            Leilao leilao = new CriadorDeLeilao().Para("Leilão de joias masculinas")
                .Lance(joao, 400.0)
                .Lance(maria, 300.0)
                .Lance(joao, 200.0)
                .Lance(maria, 100.0)
                .Constroi();

            //Ação
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(400, avaliador.MaiorLance, 0.0001);
            Assert.AreEqual(100, avaliador.MenorLance, 0.0001);

        }

        [Test]
        public void DeveEntenderMaiorEMenorLanceIguais()
        {
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Segundo Leilão")
                .Lance(maria, 200.0)
                .Lance(joao, 200.0)
                .Constroi();


            //Ação
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
            //cenário utilizando Test Data Builder
            Leilao leilao = new CriadorDeLeilao().Para("Segundo Leilão")
                .Lance(joao, 200.0)
                .Constroi();


            //Ação
            avaliador.Avalia(leilao);

            //Validação
            Assert.AreEqual(200, avaliador.MenorLance, 0.0001);
            Assert.AreEqual(200, avaliador.MaiorLance, 0.0001);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void NaoDeveAvaliarLeilaoSemLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("PlayStation 5").Constroi();
            avaliador.Avalia(leilao);
        }
    }
}

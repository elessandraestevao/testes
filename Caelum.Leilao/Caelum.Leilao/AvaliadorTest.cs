using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
namespace Caelum.Leilao
{
    [TestFixture]
    public class AvaliadorTest
    {
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
        public void DeveAgirComNumerosNegativos()
        {

        }
    }
}

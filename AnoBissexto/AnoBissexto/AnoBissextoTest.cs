using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AnoBissexto
{
    [TestFixture]
    class AnoBissextoTest
    {
        [Test]
        public void DeveReconhecerAnoBissexto()
        {
            AnoBissexto anoBissexto = new AnoBissexto();
            bool verificacao = anoBissexto.EhBissexto(4);

            Assert.True(verificacao);
        }

        [Test]
        public void DeveReconhecerAnoNaoBissexto()
        {
            AnoBissexto anoBissexto = new AnoBissexto();
            bool verificacao = anoBissexto.EhBissexto(5);

            Assert.False(verificacao);
        }

        [Test]
        public void DeveReconhecerAno100ComoNaoBissexto()
        {
            AnoBissexto anoBissexto = new AnoBissexto();
            bool verificacao = anoBissexto.EhBissexto(100);

            Assert.False(verificacao);
        }

        [Test]
        public void DeveReconhecerAno400ComoBissexto()
        {
            AnoBissexto anoBissexto = new AnoBissexto();
            bool verificacao = anoBissexto.EhBissexto(400);

            Assert.True(verificacao);
        }

        [Test]
        public void NaoDeveReconhecerAnoZeroComoBissexto()
        {
            AnoBissexto anoBissexto = new AnoBissexto();
            bool verificacao = anoBissexto.EhBissexto(0);

            Assert.False(verificacao);
        }
    }
}

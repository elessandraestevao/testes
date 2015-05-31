using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MatematicaMaluca
{
    [TestFixture]
    class MatematicaMalucaTest
    {
        [Test]
        public void DeveMultiplicarComValoresMaioresQueTrinta()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(31);

            Assert.AreEqual(31 * 4, resultado, 0.0001);
        }

        [Test]
        public void DeveMultiplicarComValoresMenoresQueTrintaEMaioresQueDez()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(11);

            Assert.AreEqual(11 * 3, resultado, 0.0001);
        }

        [Test]
        public void DeveMultiplicarComValoresMenoresQueDez()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(9);

            Assert.AreEqual(9 * 2, resultado, 0.0001);
        }

        [Test]
        public void DeveMultiplicarComValorIgualATrinta()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(30);

            Assert.AreEqual(30 * 3, resultado, 0.0001);
        }

        [Test]
        public void DeveMultiplicarComValorIgualADez()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(10);

            Assert.AreEqual(10 * 2, resultado, 0.0001);
        }

        [Test]
        public void DeveMultiplicarComValorNegativo()
        {
            MatematicaMaluca m = new MatematicaMaluca();
            int resultado = m.ContaMaluca(-1);

            Assert.AreEqual(-1 * 2, resultado, 0.0001);
        }
    }
}

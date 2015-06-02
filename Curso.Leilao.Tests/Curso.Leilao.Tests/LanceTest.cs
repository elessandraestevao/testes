using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Curso.Leilao
{
    [TestFixture]
    class LanceTest
    {
        private Usuario joao;

        [SetUp]
        public void SetUp()
        {
            this.joao = new Usuario("João");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DeveReconhecerLanceComValorIgualAZero()
        {
            Lance lance = new Lance(joao, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DeveReconhecerLanceComValorNegativo()
        {
            Lance lance = new Lance(joao, -100.0);
        }
    }
}

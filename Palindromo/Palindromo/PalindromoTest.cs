using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Palindromo
{
    [TestFixture]
    class PalindromoTest
    {
        [Test]
        public void DeveReconhecerUmPalindromo()
        {
            Palindromo p = new Palindromo();
            string palindromo = "Socorram-me subi no onibus em Marrocos";
            bool verifica = p.EhPalindromo(palindromo);

            Assert.AreEqual(true, verifica);
        }

        [Test]
        public void DeveReconhecerQueNaoEhUmPalindromo()
        {
            Palindromo p = new Palindromo();
            string palindromo = "Essa frase nao e palindromo";
            bool verifica = p.EhPalindromo(palindromo);

            Assert.AreEqual(false, verifica);
        }
    }
}

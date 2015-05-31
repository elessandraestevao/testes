using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Curso.Leilao
{
    [TestFixture]
    class FiltroDeLancesTest
    {
        [Test]
        public void DeveSelecionarLancesEntre1000E3000()
        {
            Usuario joao = new Usuario("Joao");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>() { 
            new Lance(joao,2000), 
            new Lance(joao,1000), 
            new Lance(joao,3000), 
            new Lance(joao, 800)});

            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual(2000, resultado[0].Valor, 0.00001);
        }

        [Test]
        public void DeveSelecionarLancesEntre500E700()
        {
            Usuario joao = new Usuario("Joao");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>() {
            new Lance(joao,600), 
            new Lance(joao,500), 
            new Lance(joao,700), 
            new Lance(joao, 800)});

            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual(600, resultado[0].Valor, 0.00001);
        }

        [Test]
        public void DeveSelecionarLancesMaioresQue5000()
        {
            Usuario joao = new Usuario("João");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>(){
                new Lance(joao, 5001.0),
                new Lance(joao, 4999.0),
                new Lance(joao, 7000.0),
                new Lance(joao, 5000.0)
            });

            Assert.AreEqual(2, resultado.Count);
            Assert.AreEqual(5001, resultado[0].Valor, 0.0001);
            Assert.AreEqual(7000, resultado[1].Valor, 0.0001);
        }

        [Test]
        public void NaoDeveSelecionarLancesMenoresQue500()
        {
            Usuario joao = new Usuario("João");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>(){
                new Lance(joao, 50.0),
                new Lance(joao, 49.0),
                new Lance(joao, 70.0),
                new Lance(joao, 54.0)
            });

            Assert.AreEqual(0, resultado.Count);            
        }

        [Test]
        public void NaoDeveSelecionarLancesEntre700E1000()
        {
            Usuario joao = new Usuario("João");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>(){
                new Lance(joao, 701.0),
                new Lance(joao, 1000.0)                
            });

            Assert.AreEqual(0, resultado.Count);
        }

        [Test]
        public void NaoDeveSelecionarLancesEntre3000E5000()
        {
            Usuario joao = new Usuario("João");

            FiltroDeLances filtro = new FiltroDeLances();
            IList<Lance> resultado = filtro.Filtra(new List<Lance>(){
                new Lance(joao, 3000.0),
                new Lance(joao, 4000.0),
                new Lance(joao, 5000.0)                
            });

            Assert.AreEqual(0, resultado.Count);
        }
    }
}

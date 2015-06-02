using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Leilao
{
    public class Avaliador
    {
        private double maiorValor = double.MinValue;
        private double menorValor = double.MaxValue;
        private double mediaValor;
        private IList<Lance> maiores;

        public void Avalia(Leilao leilao)
        {
            if (leilao.Lances.Count == 0)
            {
                throw new Exception("Não é possível avaliar leilão sem lances");
            }

            foreach (var l in leilao.Lances)
            {
                if (l.Valor > maiorValor)
                {
                    maiorValor = l.Valor;
                }
                if (l.Valor < menorValor)
                {
                    menorValor = l.Valor;
                }
                mediaValor += l.Valor;
            }
            mediaValor = mediaValor / leilao.Lances.Count;
            pegaOsMaioresNo(leilao);
        }

        private void pegaOsMaioresNo(Leilao leilao)
        {
            var filtro = leilao.Lances.OrderByDescending(p => p.Valor).Take(3);
            maiores = new List<Lance>(filtro);
        }

        public IList<Lance> TresMaiores
        {
            get { return this.maiores; }
        }

        public double MaiorLance
        {
            get { return maiorValor; }
        }

        public double MenorLance
        {
            get { return menorValor; }
        }

        public double MediaLances
        {
            get { return mediaValor; }
        }
    }
}

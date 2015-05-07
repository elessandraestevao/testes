using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.Leilao
{
    public class Avaliador
    {
        private double maiorValor = double.MinValue;
        private double menorValor = double.MaxValue;
        private double mediaValor;

        public void Avalia(Leilao leilao)
        {
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
        }

        public double MaiorLance
        {
            get { return maiorValor; }
        }

        public double MenorLance
        {
            get {return menorValor;}
        }

        public double MediaLances
        {
            get { return MediaLances; }
        }
    }
}

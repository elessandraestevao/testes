using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Leilao
{
    public class Leilao
    {

        public string Descricao { get; set; }
        public IList<Lance> Lances { get; set; }

        public Leilao(string descricao)
        {
            this.Descricao = descricao;
            this.Lances = new List<Lance>();
        }

        public void Propoe(Lance lance)
        {    
            if (Lances.Count == 0 || PodeAdicionarLance(lance.Usuario))
            {
                Lances.Add(lance);
            }            
        }

        private bool PodeAdicionarLance(Usuario usuario)
        {
            return !UltimoLanceDado().Usuario.Equals(usuario) 
                && QtdeDeLancesDeUmUsuario(usuario) < 5;
        }

        private int QtdeDeLancesDeUmUsuario(Usuario usuario)
        {
            int total = 0;
            foreach (Lance l in Lances)
            {
                if (l.Usuario.Equals(usuario))
                {
                    total++;
                }
            }
            return total;
        }

        private Lance UltimoLanceDado()
        {
            return Lances[Lances.Count - 1];
        }       
    }
}

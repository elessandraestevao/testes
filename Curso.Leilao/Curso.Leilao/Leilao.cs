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

        public void DobraLance(Usuario usuario)
        {  
            Lance ultimoLance = ultimoLanceDo(usuario);
            if (ultimoLance != null)
            {
                Propoe(new Lance(usuario, ultimoLance.Valor * 2));
            }
        }

        private Lance ultimoLanceDo(Usuario usuario)
        {
            Lance ultimoLance = null;
            foreach (var l in Lances)
            {
                if (l.Usuario.Equals(usuario))
                {
                    ultimoLance = l;
                }
            }
            return ultimoLance;
        }

        public void Propoe(Lance lance)
        {    
            if (Lances.Count == 0 || podeAdicionarLance(lance.Usuario))
            {
                Lances.Add(lance);
            }            
        }

        private bool podeAdicionarLance(Usuario usuario)
        {
            return !ultimoLanceDado().Usuario.Equals(usuario) 
                && qtdeDeLancesDeUmUsuario(usuario) < 5;
        }

        private int qtdeDeLancesDeUmUsuario(Usuario usuario)
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

        private Lance ultimoLanceDado()
        {
            return Lances[Lances.Count - 1];
        }       
    }
}

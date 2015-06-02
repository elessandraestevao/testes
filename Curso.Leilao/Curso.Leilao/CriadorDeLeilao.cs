using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Leilao
{
    public class CriadorDeLeilao
    {
        private Leilao leilao;
        public CriadorDeLeilao Para(string descricao)
        {
            this.leilao = new Leilao(descricao);
            return this;
        }

        public CriadorDeLeilao Lance(Usuario usuario, double valor)
        {
            this.leilao.Propoe(new Lance(usuario, valor));
            return this;
        }

        public Leilao Constroi()
        {
            return this.leilao;
        }

        public CriadorDeLeilao CriaLancesAlternados(List<Usuario> listaUsuarios, int quantidade)
        {
            double valor = 2000.0;
            for (int i = 1; i <= quantidade; i++)
            {
                foreach (var usuario in listaUsuarios)
                {
                    this.leilao.Propoe(new Lance(usuario, valor));                                        
                    valor = valor + 1000.0;                    
                }                
            }
            return this;
        }
    }
}

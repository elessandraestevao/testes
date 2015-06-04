using mock.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.servico
{
    public class CriadorDeLeilao
    {
        private List<Leilao> leiloes;
        private Leilao leilao;

        public CriadorDeLeilao()
        {
            this.leiloes = new List<Leilao>();
        }

        public CriadorDeLeilao Para(string descricao)
        {
            this.leilao = new Leilao(descricao);
            return this;
        }

        public CriadorDeLeilao NaData(DateTime data)
        {
            this.leilao.naData(data);
            return this;
        }

        public CriadorDeLeilao Lance(Lance lance)
        {
            leilao.propoe(lance);
            return this;
        }

        public CriadorDeLeilao AdicionaNaLista()
        {
            this.leiloes.Add(leilao);
            return this;
        }

        public List<Leilao> RetornaListaDeLeiloes()
        {
            return leiloes;
        }
    }
}

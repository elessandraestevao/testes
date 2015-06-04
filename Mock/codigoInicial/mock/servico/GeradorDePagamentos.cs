using mock.dominio;
using mock.infra;
using mock.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.servico
{
    public class GeradorDePagamentos
    {
        private IRepositorioDeLeiloes leilaoDao;
        private PagamentoDao pagamentoDao;
        private Avaliador avaliador;

        public GeradorDePagamentos(IRepositorioDeLeiloes leilaoDao, PagamentoDao pagamentoDao, Avaliador avaliador)
        {
            this.leilaoDao = leilaoDao;
            this.pagamentoDao = pagamentoDao;
            this.avaliador = avaliador;
        }

        public virtual void Gera()
        {
            List<Leilao> encerrados = new List<Leilao>();
            encerrados = leilaoDao.encerrados();
            foreach (var l in encerrados)
            {
                this.avaliador.avalia(l);                
            }
            Pagamento pagamento = new Pagamento(this.avaliador.maiorValor, DateTime.Today);
            this.pagamentoDao.Salvar(pagamento);
        }
    }
}

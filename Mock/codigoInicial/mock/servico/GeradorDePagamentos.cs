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
        private IRelogio relogio;

        public GeradorDePagamentos(IRepositorioDeLeiloes leilaoDao, PagamentoDao pagamentoDao, Avaliador avaliador)
        {
            this.leilaoDao = leilaoDao;
            this.pagamentoDao = pagamentoDao;
            this.avaliador = avaliador;
            this.relogio = new RelogioDoSistema();
        }

        public GeradorDePagamentos(IRepositorioDeLeiloes leilaoDao, PagamentoDao pagamentoDao, Avaliador avaliador, IRelogio relogio)
        {
            this.leilaoDao = leilaoDao;
            this.pagamentoDao = pagamentoDao;
            this.avaliador = avaliador;
            this.relogio = relogio;
        }

        public void Gera()
        {
            List<Leilao> encerrados = new List<Leilao>();
            encerrados = this.leilaoDao.encerrados();
            foreach (var l in encerrados)
            {
                this.avaliador.avalia(l);                
            }
            Pagamento pagamento = new Pagamento(this.avaliador.maiorValor, proximoDiaUtil());
            this.pagamentoDao.Salvar(pagamento);
        }

        private DateTime proximoDiaUtil()
        {
            DateTime data = this.relogio.Hoje();
            DayOfWeek diaDaSemana = data.DayOfWeek;
            if (diaDaSemana == DayOfWeek.Saturday)
            {
                data = data.AddDays(2);
            }
            else if (diaDaSemana == DayOfWeek.Sunday)
            {
                data = data.AddDays(1);
            }
            return data;
        }
    }
}

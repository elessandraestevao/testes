using mock.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.interfaces
{
    public interface IRepositorioDeLeiloes
    {
        void salva(Leilao leilao);
        List<Leilao> encerrados();
        List<Leilao> correntes();
        void atualiza(Leilao leilao);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.servico
{
    public class Pagamento
    {
        public virtual double Valor { get; set; }
        public DateTime Data { get; set; }

        public Pagamento(double valor, DateTime data)
        {            
            this.Valor = valor;
            this.Data = data;
        }
        
    }
}

﻿using mock.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mock.servico
{
    public class RelogioDoSistema : IRelogio
    {
        public DateTime Hoje()
        {
            return DateTime.Today;
        }
    }
}

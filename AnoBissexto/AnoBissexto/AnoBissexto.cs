﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnoBissexto
{
    class AnoBissexto
    {
        public bool EhBissexto(int ano)
        {
            if (ano > 0)
            {
                if (ano % 4 == 0)
                {
                    if (ano % 100 != 0 || ano % 400 == 0)
                    {
                        return true;
                    }
                }
            }       
            return false;           
        }
    }
}

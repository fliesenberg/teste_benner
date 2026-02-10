using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models.Gravacao
{
    public class Linha09
    {
        public Linha09(string tipoLinha)
        {
            TipoLinha = tipoLinha;
        }

        public string TipoLinha { get; set; }

        public int Total { get; set; }
    }
}

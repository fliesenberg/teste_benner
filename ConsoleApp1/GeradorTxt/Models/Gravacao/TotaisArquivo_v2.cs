using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models.Gravacao
{
    public class TotaisArquivo_v2 : TotaisArquivo
    {
        private readonly Linha09 totalLinha03;

        public TotaisArquivo_v2()
        {
            totalLinha03 = new Linha09("03");
            totaisPorLinha.Add(totalLinha03);
        }

        public override void SomarTotais(string tipoLinha)
        {
            switch (tipoLinha)
            {
                case "00":
                case "01":
                case "02":
                    base.SomarTotais(tipoLinha);
                    break;
                case "03":
                    base.SomarTotalArquivo();
                    ++totalLinha03.Total;
                    break;
            }
        }
    }
}

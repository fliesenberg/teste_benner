using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models.Gravacao
{
    public class TotaisArquivo
    {
        protected readonly Linha99 totalArquivo;
        protected readonly List<Linha09> totaisPorLinha;

        protected readonly Linha09 totalLinha00;
        protected readonly Linha09 totalLinha01;
        protected readonly Linha09 totalLinha02;

        public TotaisArquivo()
        {
            totalArquivo = new Linha99();

            totaisPorLinha = new List<Linha09>();

            totalLinha00 = new Linha09("00");
            totaisPorLinha.Add(totalLinha00);

            totalLinha01 = new Linha09("01");
            totaisPorLinha.Add(totalLinha01);

            totalLinha02 = new Linha09("02");
            totaisPorLinha.Add(totalLinha02);
        }

        public IEnumerable<Linha09> GetTotaisPorLinha()
        {
            return totaisPorLinha;
        }

        public Linha99 GetTotalArquivo()
        {
            return totalArquivo;
        }

        public virtual void SomarTotais(string tipoLinha)
        {
            switch (tipoLinha)
            {
                case "00":
                    SomarTotalArquivo();
                    ++totalLinha00.Total;
                    break;

                case "01":
                    SomarTotalArquivo();
                    ++totalLinha01.Total;
                    break;

                case "02":
                    SomarTotalArquivo();
                    ++totalLinha02.Total;
                    break;
            }
        }

        public void SomarTotalArquivo()
        {
            ++totalArquivo.TotalLinhas;
        }
    }
}

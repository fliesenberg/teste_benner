using ConsoleApp1.GeradorTxt.Gravacao.Leiaute_v1;
using ConsoleApp1.GeradorTxt.Models;
using ConsoleApp1.GeradorTxt.Models.Gravacao;
using ConsoleApp1.GeradorTxt.Models.Leitura;
using GeradorTxt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Gravacao.Leiaute_v2
{
    public class GeradorArquivo_v2 : GeradorArquivoBase
    {
        public override bool Gerar(IEnumerable<Empresa> empresas, string outputPath, out string msgValidacao)
        {
            List<Empresa_v2> empresas_v2 = empresas as List<Empresa_v2>;

            if (!ValidarDocumentos(empresas, out msgValidacao))
            {
                return false;
            }

            var totaisArquivo = new TotaisArquivo_v2();

            var sb = new StringBuilder();

            foreach (var emp in empresas_v2)
            {
                GerarTipo00(sb, emp, totaisArquivo);

                foreach (var doc in emp.Documentos)
                {
                    GerarTipo01(sb, doc, totaisArquivo);

                    foreach (var item in doc.Itens)
                    {
                        GerarTipo02(sb, item, totaisArquivo);

                        foreach (var categoria in item.Categorias)
                        {
                            GerarTipo03(sb, categoria, totaisArquivo);
                        }
                    }
                }
            }

            // Totalizadores
            foreach (var tipoLinha in totaisArquivo.GetTotaisPorLinha())
            {
                GerarTipo09(sb, tipoLinha, totaisArquivo);
            }

            GerarTipo99(sb, totaisArquivo.GetTotalArquivo());

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);

            return true;
        }

        protected override void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            ItemDocumento_v2 item_v2 = item as ItemDocumento_v2;

            // 02|NUMEROITEM|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item_v2.NumeroItem).Append("|")
              .Append(item_v2.Descricao).Append("|")
              .Append(ToMoney(item_v2.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo03(StringBuilder sb, Categoria categoria)
        {
            // 03|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            sb.Append("03").Append("|")
              .Append(categoria.NumeroCategoria).Append("|")
              .Append(categoria.DescricaoCategoria).AppendLine();
        }

        protected void GerarTipo03(StringBuilder sb, Categoria categoria, TotaisArquivo_v2 totaisArquivo)
        {
            EscreverTipo03(sb, categoria);
            totaisArquivo.SomarTotais("03");
        }
    }
}

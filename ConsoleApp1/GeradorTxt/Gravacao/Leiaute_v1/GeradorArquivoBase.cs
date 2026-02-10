using ConsoleApp1.GeradorTxt.Models;
using ConsoleApp1.GeradorTxt.Models.Gravacao;
using ConsoleApp1.GeradorTxt.Models.Leitura;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ConsoleApp1.GeradorTxt.Gravacao.Leiaute_v1
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase
    {
        public virtual bool Gerar(IEnumerable<Empresa> empresas, string outputPath, out string msgValidacao)
        {
            if (!ValidarDocumentos(empresas, out msgValidacao))
            {
                return false;
            }

            var totaisArquivo = new TotaisArquivo();

            var sb = new StringBuilder();

            foreach (var emp in empresas)
            {
                GerarTipo00(sb, emp, totaisArquivo);

                foreach (var doc in emp.Documentos)
                {
                    GerarTipo01(sb, doc, totaisArquivo);

                    foreach (var item in doc.Itens)
                    {
                        GerarTipo02(sb, item, totaisArquivo);
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

        public bool ValidarDocumentos(IEnumerable<Empresa> empresas, out string msgValidacao)
        {
            msgValidacao = string.Empty;

            foreach (var emp in empresas)
            {
                foreach (var doc in emp.GetDocumentos())
                {
                    decimal total = 0M;

                    foreach (var item in doc.GetItens())
                        total += item.Valor;

                    if (total != doc.Valor)
                    {
                        msgValidacao = string.Format("Valor do documento não fecha com o valor dos itens! Empresa: {0} - Documento: {1} - Valor Documento: {2} - Valor Itens: {3}", emp.Nome, doc.Numero, doc.Valor, total);
                        return false;
                    }
                }
            }

            return true;
        }

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void GerarTipo00(StringBuilder sb, Empresa emp, TotaisArquivo totaisArquivo)
        {
            EscreverTipo00(sb, emp);
            totaisArquivo.SomarTotais("00");
        }

        protected void GerarTipo01(StringBuilder sb, Documento doc, TotaisArquivo totaisArquivo)
        {
            EscreverTipo01(sb, doc);
            totaisArquivo.SomarTotais("01");
        }

        protected void GerarTipo02(StringBuilder sb, ItemDocumento item, TotaisArquivo totaisArquivo)
        {
            EscreverTipo02(sb, item);
            totaisArquivo.SomarTotais("02");
        }

        protected void GerarTipo09(StringBuilder sb, Linha09 tipoLinha, TotaisArquivo totaisArquivo)
        {
            EscreverTipo09(sb, tipoLinha);
            totaisArquivo.SomarTotalArquivo();
        }

        protected void GerarTipo99(StringBuilder sb, Linha99 totalLinhas)
        {
            EscreverTipo99(sb, totalLinhas);
        }

        protected virtual void EscreverTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00").Append("|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected virtual void EscreverTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01").Append("|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
        }

        protected virtual void EscreverTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02").Append("|")
              .Append(item.Descricao).Append("|")
              .Append(ToMoney(item.Valor)).AppendLine();
        }

        protected void EscreverTipo09(StringBuilder sb, Linha09 totalPorLinha)
        {
            // 09|TIPOLINHA|QTDLINHAS
            sb.Append("09").Append("|")
              .Append(totalPorLinha.TipoLinha).Append("|")
              .Append(totalPorLinha.Total).AppendLine();
        }

        protected void EscreverTipo99(StringBuilder sb, Linha99 totalLinhas)
        {
            // 99|TOTALLINHAS
            sb.Append("99").Append("|")
              .Append(totalLinhas.TotalLinhas).AppendLine();
        }
    }
}

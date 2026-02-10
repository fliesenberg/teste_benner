using System.Collections.Generic;

namespace ConsoleApp1.GeradorTxt.Models.Leitura
{
    public class Empresa
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public List<Documento> Documentos { get; set; }

        public virtual IEnumerable<Documento> GetDocumentos()
        {
            return Documentos;
        }
    }

    public class Documento
    {
        public string Modelo { get; set; }
        public string Numero { get; set; }
        public decimal Valor { get; set; }
        public List<ItemDocumento> Itens { get; set; }

        public virtual IEnumerable<ItemDocumento> GetItens()
        {
            return Itens;
        }
    }

    public class ItemDocumento
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}

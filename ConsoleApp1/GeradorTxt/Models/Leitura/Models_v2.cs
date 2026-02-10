using GeradorTxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models.Leitura
{
    public class Empresa_v2 : Empresa
    {
        public new List<Documento_v2> Documentos { get; set; }

        public override IEnumerable<Documento> GetDocumentos()
        {
            return Documentos;
        }
    }

    public class Documento_v2 : Documento
    {
        public new List<ItemDocumento_v2> Itens { get; set; }

        public override IEnumerable<ItemDocumento> GetItens()
        {
            return Itens;
        }
    }

    public class ItemDocumento_v2 : ItemDocumento
    {
        public string NumeroItem { get; set; }
        public List<Categoria> Categorias { get; set; }
    }

    public class Categoria
    {
        public string NumeroCategoria { get; set; }
        public string DescricaoCategoria { get; set; }
    }
}

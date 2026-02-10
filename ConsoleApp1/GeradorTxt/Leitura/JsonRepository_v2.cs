using ConsoleApp1.GeradorTxt.Models.Leitura;
using GeradorTxt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Leitura
{
    public static class JsonRepository_v2
    {
        public static List<Empresa_v2> LoadEmpresas(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("Arquivo JSON não encontrado.", jsonPath);

            try
            {
                var json = File.ReadAllText(jsonPath);

                // Desserializa diretamente para a lista de empresas
                var empresas = JsonConvert.DeserializeObject<List<Empresa_v2>>(json);

                if (empresas == null)
                    throw new Exception("O JSON não contém dados de empresas válidos.");

                return empresas;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao desserializar JSON. " +
                    "Garanta que o arquivo está no formato esperado. Detalhes: " + ex.Message, ex);
            }
        }
    }
}

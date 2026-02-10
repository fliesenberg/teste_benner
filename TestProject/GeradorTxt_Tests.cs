using ConsoleApp1.GeradorTxt.Gravacao.Leiaute_v1;
using ConsoleApp1.GeradorTxt.Gravacao.Leiaute_v2;
using ConsoleApp1.GeradorTxt.Leitura;
using ConsoleApp1.GeradorTxt.Models.Leitura;
using GeradorTxt;
using System.Collections.Generic;

namespace TestProject
{
    [TestFixture]
    public class GeradorTxt_Tests
    {
        private string jsonPath_v1 = string.Empty;
        private string jsonPath_v2 = string.Empty;

        private string outputPath = string.Empty;

        [SetUp]
        public void Setup()
        {
            jsonPath_v1 = "C:\\Benner\\teste_benner\\ConsoleApp1\\data\\base-dados.json";
            jsonPath_v2 = "C:\\Benner\\teste_benner\\ConsoleApp1\\data\\base-dados-v2.json";

            outputPath = "C:\\Benner\\teste_benner\\ConsoleApp1\\out";
        }

        [Test]
        public void LerJson_v1()
        {
            List<Empresa> empresas = JsonRepository.LoadEmpresas(jsonPath_v1);

            Assert.That(empresas, Is.Not.Empty, "Não conseguiu ler o arquivo json versão 1!");
        }

        [Test]
        public void LerJson_v2()
        {
            List<Empresa_v2> empresas = JsonRepository_v2.LoadEmpresas(jsonPath_v2);

            Assert.That(empresas, Is.Not.Empty, "Não conseguiu ler o arquivo json versão 2!");
        }

        [Test]
        public void ValidarDocumentos_v1()
        {
            List<Empresa> empresas = JsonRepository.LoadEmpresas(jsonPath_v1);

            var gerador = new GeradorArquivoBase();

            string msgValidacao;

            bool result = gerador.ValidarDocumentos(empresas, out msgValidacao);

            Assert.That(result, Is.True, "Falhou na validação de documentos do leiaute versão 1!");
        }

        [Test]
        public void ValidarDocumentos_v2()
        {
            List<Empresa_v2> empresas = JsonRepository_v2.LoadEmpresas(jsonPath_v2);

            var gerador = new GeradorArquivo_v2();

            string msgValidacao;

            bool result = gerador.ValidarDocumentos(empresas, out msgValidacao);

            Assert.That(result, Is.True, "Falhou na validação de documentos do leiaute versão 2!");
        }

        [Test]
        public void Gerar_v1()
        {
            List<Empresa> empresas = JsonRepository.LoadEmpresas(jsonPath_v1);

            var gerador = new GeradorArquivoBase();

            var fileName = $"saida_leiaute_versão 01_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            var fullPath = Path.Combine(outputPath, fileName);

            string msgValidacao;

            bool result = gerador.Gerar(empresas, fullPath, out msgValidacao);

            Assert.That(result, Is.True, "Falhou na geração do arquivo do leiaute versão 1!");
        }

        [Test]
        public void Gerar_v2()
        {
            List<Empresa_v2> empresas = JsonRepository_v2.LoadEmpresas(jsonPath_v2);

            var gerador = new GeradorArquivo_v2();

            var fileName = $"saida_leiaute_versão 02_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            var fullPath = Path.Combine(outputPath, fileName);

            string msgValidacao;

            bool result = gerador.Gerar(empresas, fullPath, out msgValidacao);

            Assert.That(result, Is.True, "Falhou na geração do arquivo do leiaute versão 2!");
        }
    }
}
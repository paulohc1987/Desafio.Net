using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Graph;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;

namespace AceleraDev
{

    class Program
    {
        const string path = @"C:\Users\Paulo\Desktop\Curso\answer.json";
        JsonCrud jscrud = new JsonCrud();

        static void Main(string[] args)
        {
            SalvarArquivo();

            JsonCrud jscrud = new JsonCrud();
            string decifrado = jscrud.DecifrandoArquivo(path);
            string resumoCriptografado = Util.CalculateSHA1(decifrado);

            string conArquivo = AtualizarArquivo(decifrado, resumoCriptografado);

            EnviarArquivo(conArquivo);
        }

        private static string  AtualizarArquivo(string decifrado, string resumo_criptografico)
        {
            string arquivoJson = path;
            JsonCrud jscrud = new JsonCrud();
            return jscrud.AtualizarRespostaJason(arquivoJson, decifrado, resumo_criptografico);
        }

        private static void SalvarArquivo()
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=8e00bb742d0df6b0bc674808a739a6d4e97c1a0b");
            requisicaoWeb.Method = "GET";
            //requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();

                // Grava o arquivo com o retorno da Api
                System.IO.File.WriteAllText(path, objResponse);

                streamDados.Close();
                resposta.Close();
            }
        }

        private static void EnviarArquivo(string conteudoArquivo)
        {
            string fileName = path;
           
            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            using (FileStream fileStream = System.IO.File.OpenRead(fileName))
            using (StreamContent fileContent = new StreamContent(fileStream))
            {
                content.Add(fileContent, "answer", "answer.json");
                var result = client.PostAsync("https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=8e00bb742d0df6b0bc674808a739a6d4e97c1a0b", content).Result;
                result.EnsureSuccessStatusCode();
            }
        }

    }
}

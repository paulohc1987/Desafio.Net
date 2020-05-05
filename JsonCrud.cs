using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceleraDev
{
    public  class JsonCrud
    {
        private string encrypt;

        public string AtualizarRespostaJason(string arquivoJson, string valorDecifrado, string ValorResumoCriptografado)
        {
            string json = File.ReadAllText(arquivoJson);

            JObject obj = JObject.Parse(json);
            obj["decifrado"] = valorDecifrado;
            obj["resumo_criptografico"] = ValorResumoCriptografado;

            string saida = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(arquivoJson, saida);
            return saida;

        }

        public string DecifrandoArquivo(string arquivoJson)
        {
            string json = File.ReadAllText(arquivoJson);

            JObject obj = JObject.Parse(json);

            string palavra = obj["cifrado"].ToString().ToLower();

            for (int i = 0; i < palavra.Length; i++)

            {
                int ASCII = (int)palavra[i];

                // mantendo tudo que é diferente de letra minuscula 
                if ((ASCII < 97 || ASCII > 122))
                {
                    encrypt += Char.ConvertFromUtf32(ASCII);
                    continue;
                }

                //Coloca a chave fixa retirando 10 posições no numero da tabela ASCII
                int ASCIIC = ASCII - 10;

                // Considerar o Alfabeto de A até Z
                if (ASCIIC < 97)
                    ASCIIC = ASCIIC += 26;

                encrypt += Char.ConvertFromUtf32(ASCIIC);
            }

            return encrypt;
        }
    }
}


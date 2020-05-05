using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceleraDev
{
    static class Util
    {
        // ou se você não quiser usar a encoding como parâmetro.
        public static string CalculateSHA1(string text)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash.ToLower();
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
        
    }
}

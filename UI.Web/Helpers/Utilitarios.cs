using Aplicacao.Core.Dominio;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace UI.Web.Helpers
{
    public class Utilitarios
    {
        public class Cripitografia
        {
            public static string GetMd5Hash(MD5 md5Hash, string input)
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            // Verify a hash against a string.
            public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(md5Hash, input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void Log(string mensagem, string arquivo)
        {
            StreamWriter writer = new StreamWriter(arquivo);
            writer.WriteLine(mensagem);
            writer.Close();
        }


        public string RetornaMesConvertido(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Janeiro";

                case 2:
                    return "Fevereiro";

                case 3:
                    return "Março";

                case 4:
                    return "Abril";

                case 5:
                    return "Maio";

                case 6:
                    return "Junho";

                case 7:
                    return "Julho";

                case 8:
                    return "Agosto";

                case 9:
                    return "Setembro";

                case 10:
                    return "Outrubro";

                case 11:
                    return "Novembro";

                case 12:
                    return "Dezembro";

                default: return "";
            }
        }

        public static string ImagemStatus(bool Bloqueado)
        {
            if (!Bloqueado)
                return "<img src=\"/Content/Site/image/icone-ativo.png\" />";
            else
                return "<img src=\"/Content/Site/image/icone-inativo.png\" />";
        }

        public static bool validarData(string data)
        {
            Regex r = new Regex(@"(\d{2}\/\d{2}\/\d{4} \d{2}:\d{2})");
            return r.Match(data).Success;
        }
     

    }
}
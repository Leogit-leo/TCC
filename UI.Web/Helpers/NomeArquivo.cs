using System;

namespace UI.Web.Helpers
{
    public class NomeArquivo
    {
        public static string GerarNomeArquivo(string nome)
        {
            return DateTime.Now.ToLongTimeString().Replace("/", "").Replace(":", "") + nome.Substring(nome.LastIndexOf("."));
        }
    }
}
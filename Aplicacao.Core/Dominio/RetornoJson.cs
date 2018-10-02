

using System.Web.Mvc;

namespace Aplicacao.Core.Dominio
{
    public class RetornoJson
    {

        public RetornoJson()
        {
            Sucesso = false;
            Redirecionar = false;
            LimparForm = false;
            IgnoraScrool = false;
            DispararFuncaoAuxiliar = false;
            Link = "";
            Mensagem = "";
        }

        public bool Sucesso { get; set; }
        public bool Redirecionar { get; set; }
        public bool LimparForm { get; set; }
        public string Link { get; set; }
        public bool IgnoraScrool { get; set; }
        [AllowHtml]
        public string Mensagem { get; set; }
        public bool DispararFuncaoAuxiliar { get; set; }
        public string FuncaoAuxiliar { get; set; }
        public string FuncaoAuxiliarParametro { get; set; }
    }
}
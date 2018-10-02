//using System.Linq;

//namespace UI.Web.Helpers
//{
//    public class GetConfigs
//    {

//        public static string Valor(string Chave)
//        {
//            string Config = "";
//            if (System.Web.HttpContext.Current.Session["Config_" + Chave] == null)
//            {
//                var ctx = new Aplicacao.Core.RepositorioEF.Contexto.BancoContexto();
//                var Configs = ctx.Configs.Where(x => x.Chave == Chave).FirstOrDefault();

//                if (Configs != null)
//                {
//                    System.Web.HttpContext.Current.Session["Config_" + Chave] = Configs.Valor + "" + Configs.Arquivo;
//                    Config = System.Web.HttpContext.Current.Session["Config_" + Chave].ToString();
//                }
//            }
//            else
//                Config = System.Web.HttpContext.Current.Session["Config_" + Chave].ToString();

//            return Config;
//        }

//    }
//}
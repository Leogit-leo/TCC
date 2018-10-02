namespace UI.Web.Helpers
{
    public class Caminho
    {
       
        private static string usuario = "~/Arquivos/Usuario/";
        private static string foto = "~/Arquivos/Foto/";

        public static string Usuario()
        {
            return usuario;
        }
        public static string Foto()
        {
            return foto;
        }

    }
}
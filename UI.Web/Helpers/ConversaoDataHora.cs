using System;

namespace UI.Web.Helpers
{
    public class ConversaoDataHora
    {
        public static DateTime Brasil(DateTime dateTime)
        {
            //TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

        public string NomeMes(int NumeroMes)
        {
            string nomeMes = "";

            switch (NumeroMes)
            {
                case 1:
                    nomeMes = "Janeiro";
                    break;
                case 2:
                    nomeMes = "Fevereiro";
                    break;
                case 3:
                    nomeMes = "Março";
                    break;
                case 4:
                    nomeMes = "Abril";
                    break;
                case 5:
                    nomeMes = "Maio";
                    break;
                case 6:
                    nomeMes = "Junho";
                    break;
                case 7:
                    nomeMes = "Julho";
                    break;
                case 8:
                    nomeMes = "Agosto";
                    break;
                case 9:
                    nomeMes = "Setembro";
                    break;
                case 10:
                    nomeMes = "Outubro";
                    break;
                case 11:
                    nomeMes = "Novembro";
                    break;
                case 12:
                    nomeMes = "Dezembro";
                    break;

                default:
                    nomeMes = "Mês não encontrado!";
                    break;
            }

            return nomeMes;
        }

        public DateTime MenorDataMes(DateTime data)
        {



            return new DateTime(data.Year, data.Month, 1);
        }

        public DateTime MaiorDataMes(DateTime data)
        {



            return new DateTime(data.Year, data.Month + 1, 1).AddDays(-1);
        }
    }
}
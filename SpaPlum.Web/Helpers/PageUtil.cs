using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SpaPlum.Web.Helpers
{
    public class PageUtil
    {
        private static void MostrarAlerta(string nomeFunction, string mensagem)
        {
            var page = (Page)HttpContext.Current.CurrentHandler;
            if (((page != null)))
            {
                var script = string.Format("{0}('{1}');", nomeFunction, mensagem.Replace("'", "\""));
                page.ClientScript.RegisterStartupScript(page.GetType(), "key", script, true);
            }
        }

        public static void MostrarAlertaSucesso(string mensagem)
        {
            MostrarAlerta("MostrarAlertaSucesso", mensagem);
        }

        public static void MostrarAlertaInfo(string mensagem)
        {
            MostrarAlerta("MostrarAlertaInfo", mensagem);
        }

        public static void MostrarAlertaWarning(string mensagem)
        {
            MostrarAlerta("MostrarAlertaWarning", mensagem);
        }

        public static void MostrarAlertaErro(string mensagem)
        {
            MostrarAlerta("MostrarAlertaErro", mensagem);
        }
    }
}
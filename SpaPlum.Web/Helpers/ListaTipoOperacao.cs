using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaTipoOperacao
    {
        public List<ListItem> TipoOperacaoLista { get; set; }

        public ListaTipoOperacao()
        {
            TipoOperacaoLista = GetTipoOperacaoLista();
        }

        private List<ListItem> GetTipoOperacaoLista()
        {
            Array values = Enum.GetValues(typeof(TipoOperacao));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);
                string texto = Enum.GetName(typeof(TipoOperacao), i);

                switch ((int)i)
                {
                    case 0:
                        texto = "Nenhum";
                        break;
                    case 1:
                        texto = "Contas à Receber";
                        break;
                    case 2:
                        texto = "Contas à Pagar";
                        break;
                    case 3:
                        texto = "Transferência";
                        break;
                }

                items.Add(new ListItem
                {
                    Text = texto,
                    Value = valor,
                    Selected = false,
                    Enabled = true
                });
            }

            return items;
        }



    }
}
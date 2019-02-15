using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaGatewayPagamento
    {
        public List<ListItem> GatewayPagamentoListItem { get; set; }

        public ListaGatewayPagamento()
        {            
            GatewayPagamentoListItem = GetGatewayPagamentoListItem();
        }

        private List<ListItem> GetGatewayPagamentoListItem()
        {
            Array values = Enum.GetValues(typeof(GatewayPagamento));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);

                items.Add(new ListItem
                {
                    Text = Enum.GetName(typeof(GatewayPagamento), i),
                    Value = valor,
                    Selected = false,
                    Enabled = valor != "0" ? true : false
                });
            }

            return items;
        }



    }
}
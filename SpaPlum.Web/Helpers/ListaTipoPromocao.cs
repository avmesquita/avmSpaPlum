using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaTipoPromocao
    {
        public List<ListItem> TipoPromocaoLista { get; set; }

        public ListaTipoPromocao()
        {
            TipoPromocaoLista = GetTipoPromocaoLista();
        }

        private List<ListItem> GetTipoPromocaoLista()
        {
            Array values = Enum.GetValues(typeof(TipoPromocao));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);
                string texto = Enum.GetName(typeof(TipoPromocao), i);

                switch ((int)i)
                {
                    case 0:
                        texto = "Nenhum";
                        break;
                    case 1:
                        texto = "Desconto no Pedido";
                        break;
                    case 2:
                        texto = "Desconto no Servico";
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
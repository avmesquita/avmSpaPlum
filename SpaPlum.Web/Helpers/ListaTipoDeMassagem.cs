using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaTipoDeMassagem
    {
        public List<ListItem> Lista { get; set; }

        public ListaTipoDeMassagem()
        {
            Lista = GetListaDeMassagens();
        }

        private List<ListItem> GetListaDeMassagens()
        {
            List<ListItem> items = new List<ListItem>(3);

            items.Add(new ListItem
            {
                Text = "Padrão",
                Value = "0",
                Selected = true,
                Enabled = true
            });

            items.Add(new ListItem
            {
                Text = "4 Mãos",
                Value = "1",
                Selected = false,
                Enabled = true
            });

            items.Add(new ListItem
            {
                Text = "Para Casais",
                Value = "2",
                Selected = false,
                Enabled = true
            });

            return items;
        }

    }
}
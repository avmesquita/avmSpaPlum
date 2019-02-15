using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaDiaDaSemana
    {
        public SelectList DiaDaSemanaSelectList { get; set; }
        public List<ListItem> DiaDaSemanaListItem { get; set; }

        public ListaDiaDaSemana()
        {
            DiaDaSemanaSelectList = GetDiaDaSemanaSelectList();
            DiaDaSemanaListItem = GetDiaDaSemanaListItem();
        }


        private SelectList GetDiaDaSemanaSelectList()
        {
            Array values = Enum.GetValues(typeof(DiaDaSemana));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);
                
                items.Add(new ListItem
                {
                    Text = Enum.GetName(typeof(DiaDaSemana), i),
                    Value = valor,
                    Selected = false,
                    Enabled = valor != "0" ? true : false
                });
            }

            return new SelectList(items);
        }

        private List<ListItem> GetDiaDaSemanaListItem()
        {
            Array values = Enum.GetValues(typeof(DiaDaSemana));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);

                items.Add(new ListItem
                {
                    Text = Enum.GetName(typeof(DiaDaSemana), i),
                    Value = valor,
                    Selected = false,
                    Enabled = valor != "0" ? true : false
                });
            }

            return items;
        }



    }
}
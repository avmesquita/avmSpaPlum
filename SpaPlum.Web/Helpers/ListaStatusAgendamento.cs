using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Helpers
{
    public class ListaStatusAgendamento
    {
        public List<ListItem> StatusAgendamentoListItem { get; set; }

        public ListaStatusAgendamento()
        {
            StatusAgendamentoListItem = GetStatusAgendamentoListItem();
        }

        private List<ListItem> GetStatusAgendamentoListItem()
        {
            Array values = Enum.GetValues(typeof(StatusAgendamento));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                string valor = Convert.ToString((int)i);
                string texto = Enum.GetName(typeof(StatusAgendamento), i);

                switch ((int)i)
                {
                    case 1: texto = "Agendado Pelo Site";
                        break;
                    case 2: texto = "Agendado Pelo Telefone";
                        break;
                    case 3: texto = "Agendado Pessoalmente";
                        break;
                    case 5: texto = "Confirmado";
                        break;
                    case 7: texto = "Pago";
                        break;
                    case 9: texto = "Cancelado";
                        break;
                }

                items.Add(new ListItem
                {
                    Text = texto,
                    Value = valor,
                    Selected = false,
                    Enabled = valor != "0" ? true : false
                });
            }

            return items;
        }



    }
}
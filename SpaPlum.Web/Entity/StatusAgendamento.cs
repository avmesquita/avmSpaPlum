using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Web.Entity
{
    public enum StatusAgendamento
    {
        AgendadoPeloSite = 1,
        AgendadoPeloTelefone = 2,
        AgendadoPessoalmente = 3,

        Confirmado = 5,

        Pago = 7,

        Cancelado = 9
    }    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Entity
{
    public enum GatewayPagamento
    {
        Nenhum = 0,
        PayPalPadrao = 1,
        PayPalExpress = 2,
        MercadoPago = 3,
        Cielo = 4,
        F2b = 5,
        PagSeguro = 6
    }

}
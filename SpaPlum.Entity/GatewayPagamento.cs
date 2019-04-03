using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
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
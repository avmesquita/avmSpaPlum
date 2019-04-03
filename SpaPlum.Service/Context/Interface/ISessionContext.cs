using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Service.Context.Interface
{
    public interface ISessionContext
    {
        string SessionID { get; }

        //Cliente ClienteLogado { get; set; }

        //Carrinho Carrinho { get; }

        //UsuarioFacebook UsuarioFacebook { get; set; }

        /// <summary>
        /// Representa o valor total de pagamento usado para o Paypal
        /// </summary>
        string ValorTotalPaypal { get; set; }

        /// <summary>
        /// IP do Cliente - Para registrar origem da venda
        /// </summary>
        String IPCliente { get; set; }

        /// <summary>
        /// Recria o carrinho na sessao depois que zerar a sacola
        /// </summary>
        void NovoCarrinho();


    }
}
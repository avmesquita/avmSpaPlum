using SpaPlum.Web.Contexto.Interface;
using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Contexto
{
    public class SessionContext : ISessionContext
    {
        private const string IMAGEM_AVATAR = "ImagemAvatar";
        private const string PAYPAL_AMOUNT = "payment_amt";
        private const string IP_DO_CLIENTE = "EnderecoIPdoCliente";
        private const string CARRINHO = "ShoppingCart";

        private static void SetValueInContext<T>(T value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }

        private static T GetValueFromContext<T>(string key)
        {
            var value = HttpContext.Current.Session[key];

            if (value == null)
            {
                return default(T);
            }

            return (T)value;
        }

        public string SessionID
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Session != null))
                {
                    return HttpContext.Current.Session.SessionID.ToString();
                }
                else { return string.Empty; }
            }
        }

        public byte[] ImagemAvatar
        {
            get { return GetValueFromContext<byte[]>(IMAGEM_AVATAR); }
            set { SetValueInContext<byte[]>(value, IMAGEM_AVATAR); }
        }

        public string ValorTotalPaypal
        {
            get { return GetValueFromContext<string>(PAYPAL_AMOUNT); }
            set { SetValueInContext<string>(value, PAYPAL_AMOUNT); }
        }

        public string IPCliente
        {
            get { return GetValueFromContext<String>(IP_DO_CLIENTE); }
            set { SetValueInContext<String>(value, IP_DO_CLIENTE); }
        }

        public Carrinho Carrinho
        {
            get
            {
                Carrinho meuCarrinho = GetValueFromContext<Carrinho>(CARRINHO) ?? new Carrinho(this.SessionID, this.IPCliente);
                SetValueInContext(meuCarrinho, CARRINHO);

                return meuCarrinho;
            }
        }

        public void NovoCarrinho()
        {
            var novoCarrinho = new Carrinho(this.SessionID, this.IPCliente);

            SetValueInContext(novoCarrinho, CARRINHO);
        }
    }
}
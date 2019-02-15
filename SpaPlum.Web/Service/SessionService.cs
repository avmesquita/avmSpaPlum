using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Service
{
    public sealed class SessionService
    {
        private const string PAYPAL_AMOUNT = "payment_amt";
        private const string CARRINHO = "ShoppingCart";

        #region Singleton Pattern
        private static volatile SessionService instance;
        private static object syncRoot = new Object();

        private SessionService()
        {
            // pré-instancia o carrinho de compras
            _carrinho = new Carrinho();
            HttpContext.Current.Session.Add(CARRINHO, _carrinho);
            HttpContext.Current.Session.Add(PAYPAL_AMOUNT, 0);
        }

        public static SessionService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SessionService();
                    }
                }
                return instance;
            }
        }
        #endregion

        private Carrinho _carrinho;

        public Carrinho Carrinho
        {
            get { return _carrinho; }
            set { _carrinho = value; }
        }

        public void Limpar()
        {
            _carrinho = null;
            _carrinho = new Carrinho();            
        }

    }
}
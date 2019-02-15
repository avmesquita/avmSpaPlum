using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SpaPlum.Web.Service
{
    public class WebSessionContextCreationService : SessionContextCreationService
    {
        public override void Create(string SessionID, Boolean redirecionar = true)
        {
            base.Create(SessionID, redirecionar);

            if (redirecionar)
            {
                /*
                if (cliente != null)
                    FormsAuthentication.RedirectFromLoginPage(cliente.Email, false);
                else if (usuarioFacebook != null)
                {
                    if (usuarioFacebook.Cliente != null)
                        FormsAuthentication.RedirectFromLoginPage(usuarioFacebook.Cliente.Email, false);
                    else
                        FormsAuthentication.RedirectFromLoginPage(usuarioFacebook.Email, false);
                }*/
            }
        }


    }
}
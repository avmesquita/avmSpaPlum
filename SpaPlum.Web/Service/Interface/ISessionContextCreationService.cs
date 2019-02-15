using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Service.Interface
{
    public interface ISessionContextCreationService
    {
        void Create(string SessionID, Boolean redirecionar = true);
    }
}
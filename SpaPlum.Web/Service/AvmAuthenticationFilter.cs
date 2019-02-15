using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SpaPlum.Web.Service
{
    public class AvmAuthenticationFilter //: ActionFilterAttribute, IAuthenticationFilter
    {

        public Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            /*
            if (context.Principal != null && context.Principal.Identity.IsAuthenticated)
            {
                CustomPrincipal myPrincipal = new CustomPrincipal();

                // Do work to setup custom principal

                context.Principal = myPrincipal;
            }
            */
            return Task.FromResult(0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SpaPlum.Web.Models
{
    public static class UserExtendedModel
    {
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim == null ? null : claim.Value;
        }
        public static string GetAddress(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.StreetAddress);
            return claim == null ? null : claim.Value;
        }
        public static string GetNome(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Nome");
            return claim == null ? null : claim.Value;
        }
        public static string GetSobrenome(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("Sobrenome");
            return claim == null ? null : claim.Value;
        }
        public static string GetCodigoCliente(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("CodigoCliente");
            return claim == null ? null : claim.Value;
        }
        public static string GetCodigoPerfil(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("CodigoPerfil");
            return claim == null ? null : claim.Value;
        }
		public static string GetCodigoTerapeuta(this IPrincipal user)
		{
			var claim = ((ClaimsIdentity)user.Identity).FindFirst("CodigoTerapeuta");
			return claim == null ? null : claim.Value;
		}
	}

}
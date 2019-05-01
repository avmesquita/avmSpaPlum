using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpaPlum.Web.Contexto;

namespace SpaPlum.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoPerfil { get; set; }
		public int CodigoTerapeuta { get; set; }
		public int CodigoEmpresa { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// Add custom user claims here
			userIdentity.AddClaim(new Claim("Nome", this.Nome));
            userIdentity.AddClaim(new Claim("Sobrenome", this.Sobrenome));
            userIdentity.AddClaim(new Claim("CodigoCliente", this.CodigoCliente.ToString()));
            userIdentity.AddClaim(new Claim("CodigoPerfil",  this.CodigoPerfil.ToString()));
			userIdentity.AddClaim(new Claim("CodigoTerapeuta", this.CodigoTerapeuta.ToString()));
			userIdentity.AddClaim(new Claim("CodigoEmpresa", this.CodigoEmpresa.ToString()));

			return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SpaPlumEntitiesSQL", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		//public DbSet<ApplicationUser> Usuarios { get; set; }
	}
}
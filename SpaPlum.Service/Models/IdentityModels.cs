using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpaPlum.Service.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public int CodigoCliente { get; set; }
		public int CodigoPerfil { get; set; }
		public int CodigoTerapeuta { get; set; }
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			   : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("localhost\\SQLEXPRESS; Database = dbo_spaplum; User Id = sa; Password = syncmaster;");
		}

		//public DbSet<ApplicationUser> Usuarios { get; set; }
	}
}
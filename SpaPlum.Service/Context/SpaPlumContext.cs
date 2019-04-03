using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;

namespace SpaPlum.Service.Context
{
    public class SpaPlumContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public SpaPlumContext()
			: base()
		{
			
		}

        public int SaveChanges(bool refreshOnConcurrencyException, RefreshMode refreshMode = RefreshMode.ClientWins)
        {
            try
            {
                return SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (refreshMode == RefreshMode.ClientWins)
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    else
                        entry.Reload();
                }
                return SaveChanges();
            }
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("localhost\\SQLEXPRESS;Database=dbo_spaplum;User Id=sa;Password=syncmaster;");
		}


	}
}
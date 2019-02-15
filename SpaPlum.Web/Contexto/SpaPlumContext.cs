using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Contexto
{
    public class SpaPlumContext : DbContext
    {
        public SpaPlumContext()
            : base("SpaPlumEntitiesSQL")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public class SpaPlumInitializer : DropCreateDatabaseIfModelChanges<SpaPlumContext>
        {
            public SpaPlumInitializer()
            {
				// PARA NÃO FAZER NADA, COMENTA TUDO

                // RECRIA QQ ALTERACAO
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AgendamentoContexto>());                

                // CRIA SOMENTE SE NAO TIVER NADA
                //Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
            }
        }



        public int SaveChanges(bool refreshOnConcurrencyException, RefreshMode refreshMode = RefreshMode.ClientWins)
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (DbEntityEntry entry in ex.Entries)
                {
                    if (refreshMode == RefreshMode.ClientWins)
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    else
                        entry.Reload();
                }
                return SaveChanges();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            Database.SetInitializer(new SpaPlumInitializer());
            
            // Para realizar a criação das entidades manualmente
            //MyGeneration(modelBuilder);
        }

        private void MyGeneration(DbModelBuilder modelBuilder)
        {            
            /*
            modelBuilder.Entity<Agendamento>()
                        .HasRequired<Filial>(s => s.Filial)
                        .WithMany(s => s.Agendamentos);

            modelBuilder.Entity<Servico>()
                        .HasRequired<Filial>(s => s.Filial)
                        .WithMany(s => s.Servicos);

            modelBuilder.Entity<Terapeuta>()
                        .HasRequired<Filial>(s => s.Filial)
                        .WithMany(s => s.Terapeutas);

            modelBuilder.Entity<Fornecedor>()
                        .HasRequired<Filial>(s => s.Filial)
                        .WithMany(s => s.Fornecedores);
            */

        }
    }
}
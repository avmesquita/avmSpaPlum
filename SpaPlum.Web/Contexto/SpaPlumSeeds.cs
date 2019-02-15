using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using SpaPlum.Web.Entity;

namespace SpaPlum.Web.Contexto
{
    internal sealed class SpaPlumSeeds : DbMigrationsConfiguration<AgendamentoContexto>
    {
        public SpaPlumSeeds()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AgendamentoContexto context)
        {
            context.PerfilAcessoModels.AddOrUpdate(
                        new PerfilAcesso
                        {
                            CodigoPerfilAcesso = 0,
                            Descricao = "Administrador",
                            Ativo = true
                        },
                        new PerfilAcesso
                        {
                            CodigoPerfilAcesso = 1,
                            Descricao = "Cliente",
                            Ativo = true
                        },
                        new PerfilAcesso
                        {
                            CodigoPerfilAcesso = 2,
                            Descricao = "Profissional",
                            Ativo = true
                        }
                );

            /*
            if (context.PerfilAcessoModels.ToList().Count() == 0)
            {
                PerfilAcesso perfilAcessoAdmin = new PerfilAcesso
                {
                    CodigoPerfilAcesso = 0,
                    Descricao = "Administrador",
                    Ativo = true
                };
                PerfilAcesso perfilAcessoCliente = new PerfilAcesso
                {
                    CodigoPerfilAcesso = 1,
                    Descricao = "Cliente",
                    Ativo = true
                };
                PerfilAcesso perfilAcessoProfissional = new PerfilAcesso
                {
                    CodigoPerfilAcesso = 2,
                    Descricao = "Profissional",
                    Ativo = true
                };
                context.PerfilAcessoModels.Add(perfilAcessoAdmin);
                context.PerfilAcessoModels.Add(perfilAcessoCliente);
                context.PerfilAcessoModels.Add(perfilAcessoProfissional);
                context.SaveChanges();
            }
            */
        }
    }
}
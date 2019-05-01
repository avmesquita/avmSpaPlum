using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Contexto
{
	public partial class AgendamentoContexto : SpaPlumContext
	{
		public AgendamentoContexto()
		{
			this.Configuration.LazyLoadingEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//PrepareDatabase();

			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			Database.SetInitializer(new SpaPlumInitializer());
		}

		public DbSet<Agendamento> AgendamentoModels { get; set; }
		public DbSet<Lancamento> LancamentoModels { get; set; }
		public DbSet<Servico> ServicoModels { get; set; }
		public DbSet<TaskEntity> TaskEntityModels { get; set; }
		public DbSet<Terapeuta> TerapeutaModels { get; set; }
		public DbSet<TipoPagamento> TipoPagamentoModels { get; set; }
		public DbSet<Filial> FilialModels { get; set; }
		public DbSet<Fornecedor> FornecedorModels { get; set; }
		public DbSet<PerfilAcesso> PerfilAcessoModels { get; set; }
		public DbSet<Email> EmailModels { get; set; }
		public DbSet<HorarioTerapeuta> HorarioTerapeutaModels { get; set; }
		public DbSet<AgendamentoItemServico> AgendamentoItemServicoModels { get; set; }
		public DbSet<Carrinho> CarrinhoModels { get; set; }
		public DbSet<CarrinhoItem> CarrinhoItemModels { get; set; }
		public DbSet<FilialConfiguracao> FilialConfiguracaoModels { get; set; }
		public DbSet<TerapeutaServico> TerapeutaServicoModels { get; set; }
		public DbSet<Mensagem> MensagemModels { get; set; }
		public DbSet<Cliente> ClienteModels { get; set; }
		public DbSet<Promocao> PromocaoModels { get; set; }
		public DbSet<PromocaoCliente> PromocaoCliente { get; set; }
		public DbSet<PontoTerapeuta> PontoTerapeutaModels { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Plano> PlanoModels { get; set; }
		public DbSet<Empresa> EmpresaModels { get; set; }

		public virtual void PrepareDatabase()
		{
			//this.Database.SqlQuery<string>("msdb.dbo.sp_delete_database_backuphistory @database_name = N'dbo_spaplum'");

			//this.Database.SqlQuery<string>("alter database [dbo_spaplum] set single_user with rollback immediate");

			/* Incializa os dominios */
			/* ATENCAO: AO CRIAR CONTROLADOR COM "scaffolding", comentar as criações. */
			/*          O "scaffolding" do gerador auto-instancia o contexto, chamando OnModelCreating(). */
			if (PlanoModels.ToList().Count() == 0)
			{
				
				if (PerfilAcessoModels.ToList().Count() == 0)
				{
					PerfilAcesso perfilAcessoDev = new PerfilAcesso
					{
						CodigoPerfilAcesso = 0,
						Descricao = "Desenvolvimento",
						Ativo = true
					};
					PerfilAcesso perfilAcessoAdmin = new PerfilAcesso
					{
						CodigoPerfilAcesso = 1,
						Descricao = "Administrador",
						Ativo = true
					};
					PerfilAcesso perfilAcessoCliente = new PerfilAcesso
					{
						CodigoPerfilAcesso = 2,
						Descricao = "Cliente",
						Ativo = true
					};
					PerfilAcesso perfilAcessoProfissional = new PerfilAcesso
					{
						CodigoPerfilAcesso = 3,
						Descricao = "Profissional",
						Ativo = true
					};
					PerfilAcesso perfilAcessoGerente = new PerfilAcesso
					{
						CodigoPerfilAcesso = 4,
						Descricao = "Gerente",
						Ativo = true
					};
					PerfilAcessoModels.Add(perfilAcessoDev);
					PerfilAcessoModels.Add(perfilAcessoAdmin);
					PerfilAcessoModels.Add(perfilAcessoCliente);
					PerfilAcessoModels.Add(perfilAcessoProfissional);
					PerfilAcessoModels.Add(perfilAcessoGerente);
				}
				this.SaveChanges();
			}
		}
	}
}
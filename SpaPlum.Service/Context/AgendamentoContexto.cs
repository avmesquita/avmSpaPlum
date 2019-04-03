using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SpaPlum.Entity;
using System;
using System.Linq;

namespace SpaPlum.Service.Context
{
	public partial class AgendamentoContexto : SpaPlumContext
	{
		public AgendamentoContexto()
	           : base()
		{
			

		}		

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("localhost\\SQLEXPRESS;Database=dbo_spaplum;User Id=sa;Password=syncmaster;");
		}

		public virtual void PrepareDatabase()
		{
			/* Incializa os dominios */
			if (Plano.ToList().Count() == 0)
			{
				Plano planoGratis = new Plano
				{
					Descricao = "Plano gratuito (default)",
					Nome = "Plano gratuito pessoal",
					Preco = 0,
					VigenciaInicio = new DateTime(2018, 09, 13),
					VigenciaFim = null
				};
				Plano.Add(planoGratis);
				this.SaveChanges();

				if (Empresa.ToList().Count() == 0)
				{
					Empresa empresaPrincipal = new Empresa
					{
						Ativo = true,
						CNPJ = "023051676000100",
						CodigoPlano = planoGratis.CodigoPlano,
						DataCadastro = DateTime.Now,
						Email = "contato@avmsistemas.net",
						Nome = "AVM Sistemas",
						Plano = planoGratis,
						RazaoSocial = "ANDRE VELOSO DE MESQUITA MEI",
						Telefone = "21986415221"
					};
					Empresa.Add(empresaPrincipal);
				}
				if (PerfilAcesso.ToList().Count() == 0)
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
					PerfilAcesso.Add(perfilAcessoDev);
					PerfilAcesso.Add(perfilAcessoAdmin);
					PerfilAcesso.Add(perfilAcessoCliente);
					PerfilAcesso.Add(perfilAcessoProfissional);
					PerfilAcesso.Add(perfilAcessoGerente);
				}
				this.SaveChanges();
			}
		}

		public DbSet<SpaPlum.Entity.Agendamento> Agendamento { get; set; }
		public DbSet<SpaPlum.Entity.AgendamentoItemServico> AgendamentoItemServico { get; set; }
		public DbSet<SpaPlum.Entity.Carrinho> Carrinho { get; set; }
		public DbSet<SpaPlum.Entity.CarrinhoItem> CarrinhoItem { get; set; }
		public DbSet<SpaPlum.Entity.Cliente> Cliente { get; set; }
		public DbSet<SpaPlum.Entity.Email> Email { get; set; }
		public DbSet<SpaPlum.Entity.Empresa> Empresa { get; set; }
		public DbSet<SpaPlum.Entity.Filial> Filial { get; set; }
		public DbSet<SpaPlum.Entity.Fornecedor> Fornecedor { get; set; }
		public DbSet<SpaPlum.Entity.HorarioTerapeuta> HorarioTerapeuta { get; set; }
		public DbSet<SpaPlum.Entity.Lancamento> Lancamento { get; set; }
		public DbSet<SpaPlum.Entity.Log> Log { get; set; }
		public DbSet<SpaPlum.Entity.Mensagem> Mensagem { get; set; }
		public DbSet<SpaPlum.Entity.Plano> Plano { get; set; }
		public DbSet<SpaPlum.Entity.PontoTerapeuta> PontoTerapeuta { get; set; }
		public DbSet<SpaPlum.Entity.Promocao> Promocao { get; set; }
		public DbSet<SpaPlum.Entity.PromocaoCliente> PromocaoCliente { get; set; }
		public DbSet<SpaPlum.Entity.Servico> Servico { get; set; }
		public DbSet<SpaPlum.Entity.TerapeutaServico> TerapeutaServico { get; set; }
		public DbSet<SpaPlum.Entity.PerfilAcesso> PerfilAcesso { get; set; }
		public DbSet<SpaPlum.Entity.Terapeuta> Terapeuta { get; set; }
		public DbSet<SpaPlum.Entity.TipoPagamento> TipoPagamento { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpaPlum.Service.Context;
using SpaPlum.Service.Context.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace SpaPlum.Service
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<AgendamentoContexto>();
			//services.AddDbContext<AgendamentoContexto>(x => x.

			//var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";			

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "SpaPlum API",
					Description = "API escalável para acesso do aplicativo",
					TermsOfService = "https://www.avmsistemas.net/politica-de-privacidade",
					Contact = new Contact() { Name = "AVM Sistemas", Email = "contato@avmsistemas.net", Url = "https://www.avmsistemas.net/" }
				});				
				c.DescribeAllEnumsAsStrings();
				c.DescribeAllParametersInCamelCase();				

			});

		}	

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSwagger( x => { x.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value); });

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaPlum API v1");

				// COLOCA O SWAGGER NA HOME
				//c.RoutePrefix = string.Empty;
			});

		}
	}
}

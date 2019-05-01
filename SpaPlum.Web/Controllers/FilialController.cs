using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using PagedList;

namespace SpaPlum.Web.Controllers
{
	public class FilialController : SpaPlumBaseController
	{
		private AgendamentoContexto db = new AgendamentoContexto();

		/*
        // GET: Filial
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.FilialModels.OrderBy(x => x.Nome).ToListAsync());
        }
        */

		[Authorize]
		public ActionResult Index(int? page)
		{
			var perfil = SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User);
			var empresa = SpaPlum.Web.Models.UserExtendedModel.GetCodigoEmpresa(User);

			if ((!perfil.Equals("0")) && (!perfil.Equals("1")) && (!perfil.Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var filialModels = db.FilialModels.Where(x => x.CodigoEmpresa == empresa).OrderBy(x => x.Nome).OrderByDescending(a => a.Nome).ToPagedList(page ?? 1, 10);
			return View(filialModels);
		}



		// GET: Filial/Details/5
		[Authorize]
		public async Task<ActionResult> Details(int? id)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Filial filial = await db.FilialModels.FindAsync(id);
			if (filial == null)
			{
				return HttpNotFound();
			}
			return View(filial);
		}

		// GET: Filial/Create
		public ActionResult Create()
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View();
		}

		// POST: Filial/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "CodigoFilial,Nome,EmailPrincipalFilial,EmailSecundarioFilial,Ativo")] Filial filial)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			if (ModelState.IsValid)
			{
				db.FilialModels.Add(filial);
				try
				{
					new LogController().RegistrarLog(filial.CodigoFilial, Convert.ToInt32(SpaPlum.Web.Models.UserExtendedModel.GetCodigoCliente(User)), "[INFO] Inclusão de filial", "Inclusão de filial de " + filial.Nome.ToString() + ", por: " + SpaPlum.Web.Models.UserExtendedModel.GetFullName(User), null);
					await db.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					new LogController().RegistrarLog(filial.CodigoFilial, Convert.ToInt32(SpaPlum.Web.Models.UserExtendedModel.GetCodigoCliente(User)), "[ERRO] Inclusão de filial", "Por: " + SpaPlum.Web.Models.UserExtendedModel.GetFullName(User), ex);
				}

				return RedirectToAction("Index");
			}

			return View(filial);
		}

		// GET: Filial/Edit/5
		[Authorize]
		public async Task<ActionResult> Edit(int? id)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Filial filial = await db.FilialModels.FindAsync(id);
			if (filial == null)
			{
				return HttpNotFound();
			}
			return View(filial);
		}

		// POST: Filial/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "CodigoFilial,Nome,EmailPrincipalFilial,EmailSecundarioFilial,Ativo")] Filial filial)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			if (ModelState.IsValid)
			{
				db.Entry(filial).State = EntityState.Modified;
				new LogController().RegistrarLog(filial.CodigoFilial, Convert.ToInt32(SpaPlum.Web.Models.UserExtendedModel.GetCodigoCliente(User)), "Alteração de filial", "Alteração de filial de " + filial.Nome.ToString() + ", por: " + SpaPlum.Web.Models.UserExtendedModel.GetFullName(User), null);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(filial);
		}

		// GET: Filial/Delete/5
		[Authorize]
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");
			Filial filial = await db.FilialModels.FindAsync(id);
			if (filial == null)
			{
				return HttpNotFound();
			}
			return View(filial);
		}

		// POST: Filial/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Filial filial = await db.FilialModels.FindAsync(id);
			db.FilialModels.Remove(filial);
			new LogController().RegistrarLog(filial.CodigoFilial, Convert.ToInt32(SpaPlum.Web.Models.UserExtendedModel.GetCodigoCliente(User)), "Exclusao de filial", "Exclusao de filial de " + filial.Nome.ToString() + ", por: " + SpaPlum.Web.Models.UserExtendedModel.GetFullName(User), null);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

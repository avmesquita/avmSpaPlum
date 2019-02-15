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

namespace SpaPlum.Web.Controllers
{
    public class PerfilAcessoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: PerfilAcesso
		[Authorize]
        public async Task<ActionResult> Index()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View(await db.PerfilAcessoModels.ToListAsync());
        }

        // GET: PerfilAcesso/Details/5
		[Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			PerfilAcesso perfilAcesso = await db.PerfilAcessoModels.FindAsync(id);
            if (perfilAcesso == null)
            {
                return HttpNotFound();
            }
            return View(perfilAcesso);
        }

        // GET: PerfilAcesso/Create
		[Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View();
        }

        // POST: PerfilAcesso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoPerfilAcesso,Descricao,Ativo")] PerfilAcesso perfilAcesso)
        {
            if (ModelState.IsValid)
            {
                db.PerfilAcessoModels.Add(perfilAcesso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perfilAcesso);
        }

        // GET: PerfilAcesso/Edit/5
		[Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			PerfilAcesso perfilAcesso = await db.PerfilAcessoModels.FindAsync(id);
            if (perfilAcesso == null)
            {
                return HttpNotFound();
            }
            return View(perfilAcesso);
        }

        // POST: PerfilAcesso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoPerfilAcesso,Descricao,Ativo")] PerfilAcesso perfilAcesso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilAcesso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perfilAcesso);
        }

        // GET: PerfilAcesso/Delete/5
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

			PerfilAcesso perfilAcesso = await db.PerfilAcessoModels.FindAsync(id);
            if (perfilAcesso == null)
            {
                return HttpNotFound();
            }
            return View(perfilAcesso);
        }

        // POST: PerfilAcesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PerfilAcesso perfilAcesso = await db.PerfilAcessoModels.FindAsync(id);
            db.PerfilAcessoModels.Remove(perfilAcesso);
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

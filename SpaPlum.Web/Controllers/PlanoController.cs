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
    public class PlanoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Plano
        public async Task<ActionResult> Index()
        {
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			return View(await db.PlanoModels.ToListAsync());
        }

        // GET: Plano/Details/5
        public async Task<ActionResult> Details(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			Plano plano = await db.PlanoModels.FindAsync(id);
            if (plano == null)
            {
                return HttpNotFound();
            }
            return View(plano);
        }

        // GET: Plano/Create
        public ActionResult Create()
        {
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			return View();
        }

        // POST: Plano/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoPlano,Nome,Descricao,VigenciaInicio,VigenciaFim,Preco")] Plano plano)
        {
            if (ModelState.IsValid)
            {
                db.PlanoModels.Add(plano);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(plano);
        }

        // GET: Plano/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			Plano plano = await db.PlanoModels.FindAsync(id);
            if (plano == null)
            {
                return HttpNotFound();
            }
            return View(plano);
        }

        // POST: Plano/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoPlano,Nome,Descricao,VigenciaInicio,VigenciaFim,Preco")] Plano plano)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plano).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(plano);
        }

        // GET: Plano/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0"))
				RedirectToAction("NotAuthorized", "Erro");

			Plano plano = await db.PlanoModels.FindAsync(id);
            if (plano == null)
            {
                return HttpNotFound();
            }
            return View(plano);
        }

        // POST: Plano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Plano plano = await db.PlanoModels.FindAsync(id);
            db.PlanoModels.Remove(plano);
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

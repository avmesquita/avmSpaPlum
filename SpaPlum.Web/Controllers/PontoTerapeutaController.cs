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
    public class PontoTerapeutaController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: PontoTerapeuta
		[Authorize]
        public ActionResult Index(int? page)
        {
			var pontoTerapeutas = db.PontoTerapeutaModels.Include(p => p.Cliente)
														 .Include(p => p.Terapeuta)
														 .Include(p => p.Terapeuta.Filial)
														 .OrderBy(p => p.CodigoTerapeuta).ThenBy(p => p.Data);

			var pontoTerapeutas2 = pontoTerapeutas.ToList()
				                                  .Where(p => p.Data >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
															&& p.Data <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
									                    ).ToList();

			var pontoTerapeutas3 = pontoTerapeutas2.ToPagedList(page ?? 1, 10); 

            return View(pontoTerapeutas);
        }

        // GET: PontoTerapeuta/Details/5
		[Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("2"))
				RedirectToAction("NotAuthorized", "Erro");

			PontoTerapeuta pontoTerapeuta = await db.PontoTerapeutaModels.FindAsync(id);
            if (pontoTerapeuta == null)
            {
                return HttpNotFound();
            }
            return View(pontoTerapeuta);
        }

        // GET: PontoTerapeuta/Create
		[Authorize]
        public ActionResult Create()
        {
			if (SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("2"))
				RedirectToAction("NotAuthorized", "Erro");


			ViewBag.CodigoCliente = new SelectList(db.ClienteModels, "CodigoCliente", "Descricao");
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome");

			ViewBag.Data = DateTime.Now.ToString();
            return View();
        }

        // POST: PontoTerapeuta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoPonto,CodigoTerapeuta,CodigoCliente,Data")] PontoTerapeuta pontoTerapeuta)
        {
            if (ModelState.IsValid)
            {
                db.PontoTerapeutaModels.Add(pontoTerapeuta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoCliente = new SelectList(db.ClienteModels, "CodigoCliente", "Descricao", pontoTerapeuta.CodigoCliente);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", pontoTerapeuta.CodigoTerapeuta);
            return View(pontoTerapeuta);
        }

        // GET: PontoTerapeuta/Edit/5
		[Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			if (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("2"))
				RedirectToAction("NotAuthorized", "Erro");

			PontoTerapeuta pontoTerapeuta = await db.PontoTerapeutaModels.FindAsync(id);
            if (pontoTerapeuta == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoCliente = new SelectList(db.ClienteModels, "CodigoCliente", "Descricao", pontoTerapeuta.CodigoCliente);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", pontoTerapeuta.CodigoTerapeuta);
            return View(pontoTerapeuta);
        }

        // POST: PontoTerapeuta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoPonto,CodigoTerapeuta,CodigoCliente,Data")] PontoTerapeuta pontoTerapeuta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pontoTerapeuta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoCliente = new SelectList(db.ClienteModels, "CodigoCliente", "Descricao", pontoTerapeuta.CodigoCliente);
            ViewBag.CodigoTerapeuta = new SelectList(db.TerapeutaModels, "CodigoTerapeuta", "Nome", pontoTerapeuta.CodigoTerapeuta);
            return View(pontoTerapeuta);
        }

        // GET: PontoTerapeuta/Delete/5
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

			PontoTerapeuta pontoTerapeuta = await db.PontoTerapeutaModels.FindAsync(id);
            if (pontoTerapeuta == null)
            {
                return HttpNotFound();
            }
            return View(pontoTerapeuta);
        }

        // POST: PontoTerapeuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            throw new Exception("Não é possível excluir um apontamento.");
            /*
            PontoTerapeuta pontoTerapeuta = await db.PontoTerapeutaModels.FindAsync(id);
            db.PontoTerapeutaModels.Remove(pontoTerapeuta);
            await db.SaveChangesAsync();
            */
            //return RedirectToAction("Index");
            
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

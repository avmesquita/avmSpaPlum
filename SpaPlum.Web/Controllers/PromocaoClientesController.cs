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
    public class PromocaoClientesController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: PromocaoClientes
		[Authorize]
        public async Task<ActionResult> Index()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var promocaoCliente = db.PromocaoCliente.Include(p => p.Promocao);
            return View(await promocaoCliente.ToListAsync());
        }

        // GET: PromocaoClientes/Details/5
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

			PromocaoCliente promocaoCliente = await db.PromocaoCliente.FindAsync(id);
            if (promocaoCliente == null)
            {
                return HttpNotFound();
            }
            return View(promocaoCliente);
        }

        // GET: PromocaoClientes/Create
		[Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.CodigoPromocao = new SelectList(db.PromocaoModels, "CodigoPromocao", "Nome");
            return View();
        }

        // POST: PromocaoClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoPromocaoCliente,CodigoPromocao,Motivacao,ContaBeneficiaria,QuantidadeMaximaUtilizacoes,DataUltimaUtilizacao")] PromocaoCliente promocaoCliente)
        {
            if (ModelState.IsValid)
            {
                db.PromocaoCliente.Add(promocaoCliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoPromocao = new SelectList(db.PromocaoModels, "CodigoPromocao", "Nome", promocaoCliente.CodigoPromocao);
            return View(promocaoCliente);
        }

        // GET: PromocaoClientes/Edit/5
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

			PromocaoCliente promocaoCliente = await db.PromocaoCliente.FindAsync(id);
            if (promocaoCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoPromocao = new SelectList(db.PromocaoModels, "CodigoPromocao", "Nome", promocaoCliente.CodigoPromocao);
            return View(promocaoCliente);
        }

        // POST: PromocaoClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoPromocaoCliente,CodigoPromocao,Motivacao,ContaBeneficiaria,QuantidadeMaximaUtilizacoes,DataUltimaUtilizacao")] PromocaoCliente promocaoCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promocaoCliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoPromocao = new SelectList(db.PromocaoModels, "CodigoPromocao", "Nome", promocaoCliente.CodigoPromocao);
            return View(promocaoCliente);
        }

        // GET: PromocaoClientes/Delete/5
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

			PromocaoCliente promocaoCliente = await db.PromocaoCliente.FindAsync(id);
            if (promocaoCliente == null)
            {
                return HttpNotFound();
            }
            return View(promocaoCliente);
        }

        // POST: PromocaoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PromocaoCliente promocaoCliente = await db.PromocaoCliente.FindAsync(id);
            db.PromocaoCliente.Remove(promocaoCliente);
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

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
using SpaPlum.Web.Helpers;

namespace SpaPlum.Web.Controllers
{
    public class PromocaoController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Promocao
		[Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var promocaoModels = db.PromocaoModels.Include(p => p.Filial).OrderBy(p => p.DataFimVigencia).ToPagedList(page ?? 1, 10);
            
            return View(promocaoModels);
        }

        // GET: Promocao/Details/5
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

			Promocao promocao = await db.PromocaoModels.FindAsync(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            return View(promocao);
        }

        // GET: Promocao/Create
		[Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome");
            ViewBag.ListaTipoPromocao = new ListaTipoPromocao().TipoPromocaoLista;
            return View();
        }

        // POST: Promocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoPromocao,CodigoFilial,Nome,NomeHtml,TipoPromocao,Valor,Token,DataCadastro,DataInicioVigencia,DataFimVigencia,TempoMinimo,ValorMinimo,Ativo")] Promocao promocao)
        {
            if (ModelState.IsValid)
            {
                db.PromocaoModels.Add(promocao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", promocao.CodigoFilial);
            return View(promocao);
        }

        // GET: Promocao/Edit/5
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

			Promocao promocao = await db.PromocaoModels.FindAsync(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", promocao.CodigoFilial);
            return View(promocao);
        }

        // POST: Promocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoPromocao,CodigoFilial,Nome,NomeHtml,TipoPromocao,Valor,Token,DataCadastro,DataInicioVigencia,DataFimVigencia,TempoMinimo,ValorMinimo,Ativo")] Promocao promocao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promocao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoFilial = new SelectList(db.FilialModels, "CodigoFilial", "Nome", promocao.CodigoFilial);
            return View(promocao);
        }

        // GET: Promocao/Delete/5
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

			Promocao promocao = await db.PromocaoModels.FindAsync(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            return View(promocao);
        }

        // POST: Promocao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Promocao promocao = await db.PromocaoModels.FindAsync(id);
            db.PromocaoModels.Remove(promocao);
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

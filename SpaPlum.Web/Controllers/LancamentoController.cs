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
using SpaPlum.Web.Helpers;
using PagedList;

namespace SpaPlum.Web.Controllers
{
    public class LancamentoController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        // GET: Lancamento
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var lancamentoModels = db.LancamentoModels.Include(l => l.TipoPagamento);
            return View(await lancamentoModels.ToListAsync());
        }
        */

        [Authorize]
        public ActionResult Index(int? page)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var lancamentoModels = db.LancamentoModels.Include(t => t.Filial)
                                                      .Include(t => t.TipoPagamento)
                                                      .OrderBy(x => x.DataLancamento)
                                                      .OrderByDescending(a => a.DataPagamento)
                                                      .ToPagedList(page ?? 1, 10);
            return View(lancamentoModels);
        }


        // GET: Lancamento/Details/5
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

			Lancamento lancamento = await db.LancamentoModels.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            return View(lancamento);
        }

        // GET: Lancamento/Create
        [Authorize]
        public ActionResult Create()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			PrepararMochilao();
            return View();
        }

        // POST: Lancamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoLancamento,Titulo,Descricao,DataPagamento,CodigoAgendamento,DataLancamento,Valor,CodigoTipoPagamento,CodigoTipoOperacao,CodigoFilial")] Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                lancamento.DataLancamento = DateTime.Now;
                db.LancamentoModels.Add(lancamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            PrepararMochilao();
            return View(lancamento);
        }

        // GET: Lancamento/Edit/5
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

			Lancamento lancamento = await db.LancamentoModels.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            PrepararMochilao();
            return View(lancamento);
        }

        // POST: Lancamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodigoLancamento,Titulo,Descricao,DataPagamento,CodigoAgendamento,DataLancamento,Valor,CodigoTipoPagamento,CodigoTipoOperacao,CodigoFilial")] Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lancamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PrepararMochilao();
            return View(lancamento);
        }

        // GET: Lancamento/Delete/5
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

			Lancamento lancamento = await db.LancamentoModels.FindAsync(id);
            if (lancamento == null)
            {
                return HttpNotFound();
            }
            return View(lancamento);
        }

        // POST: Lancamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lancamento lancamento = await db.LancamentoModels.FindAsync(id);
            db.LancamentoModels.Remove(lancamento);
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

        internal void PrepararMochilao()
        {
            ViewBag.ListaTipoOperacao = new ListaTipoOperacao().TipoOperacaoLista;
            ViewBag.ListaTipoPagamento = new SelectList(db.TipoPagamentoModels.Where(x => x.Ativo == true).OrderBy(x => x.Descricao), "CodigoTipoPagamento", "Descricao");
            ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");
        }
    }
}
